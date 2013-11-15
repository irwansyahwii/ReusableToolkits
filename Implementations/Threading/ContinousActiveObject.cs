using System;
using System.Diagnostics;
using System.Threading;
using ReusableToolkits.Interfaces;

namespace ReusableToolkits.Implementations.Threading
{
  public class ContinousActiveObject : IContinousActiveObject
  {
    protected volatile bool _isRunning;

    public ContinousActiveObject()
    {
      _isRunning = false;
    }

    #region IContinousActiveObject Members

    public Action<object> WorkerMethod { get; set; }

    public void Run(object param)
    {
      DoRun(param);
    }

    protected virtual void DoRun(object param)
    {
      if (WorkerMethod == null)
      {
        throw new ArgumentNullException("WorkerMethod", "ActiveObject.DoRun()");
      }
      if (_isRunning)
      {
        return;
      }
      _isRunning = true;

      ContinousActiveObject instance = this;
      RunThread(param, instance);
    }

    protected virtual void RunThread(object param, ContinousActiveObject instance)
    {
      ThreadPool.QueueUserWorkItem(state =>
                                     {
                                       ThreadMethod(param, instance);
                                     });
    }

    protected void ThreadMethod(object param, ContinousActiveObject instance)
    {
      while (_isRunning)
      {
        //Debug.Print("Calling worker method...");
        WorkerMethod(param);
        //Debug.Print("Finish calling worker method.");
      }
      if (OnFinished != null)
      {
        OnFinished(instance);
      }
    }

    public void Stop()
    {
      _isRunning = false;
    }

    #endregion

    #region IActiveObject Members


    public void ForceStop()
    {
      _isRunning = false;
    }


    public Action<IContinousActiveObject> OnFinished
    {
      get; set; }

    #endregion
  }

  public class ContinousActiveObjectWithBackgroundThread : ContinousActiveObject
  {
    protected Thread WorkerThread;
    public ContinousActiveObjectWithBackgroundThread()
    {
      
    }

    protected override void RunThread( object param, ContinousActiveObject instance )
    {
      WorkerThread = new Thread(Start);
      WorkerThread.IsBackground = true;
      WorkerThread.Start(param);
    }

    private void Start(object o)
    {
      ThreadMethod(o, this);
    }
  }

}