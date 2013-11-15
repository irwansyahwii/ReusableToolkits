using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using ReusableToolkits.Interfaces;

namespace ReusableToolkits.Implementations.Threading
{
  public class ActiveObjectQueue<T> : IActiveObjectQueue<T> 
  {    
    private readonly object _syncRoot;
    protected Queue<T> Queue;
    protected IContinousActiveObject ContinousActiveObject;
    protected volatile bool IsStopped;

    public ActiveObjectQueue(IContinousActiveObject continousActiveObject)
    {
      
      _syncRoot = new object();
      Queue = new Queue<T>();
      ContinousActiveObject = continousActiveObject;
      IsStopped = false;
    }

    #region IContinousActiveObject Members

    public void Stop()
    {
      IsStopped = true;
      ContinousActiveObject.Stop();
      lock( _syncRoot )
      {        
        Monitor.Pulse( _syncRoot );
      }
    }

    public Action<IContinousActiveObject> OnFinished
    {
      get; set; }

    #endregion

    #region IActiveObject Members

    public Action<object> WorkerMethod
    {
      get; set; }

    protected virtual void DoRun(object param)
    {
      T item = default( T );
      //while( !IsStopped )
      //{
      lock( _syncRoot )
      {
        while( Queue.Count == 0 && !IsStopped)
        {
          //Debug.Print("Waiting...");
          Monitor.Wait( _syncRoot );            
          //Debug.Print("Waking up: IsStopped: {0}", IsStopped);
        }



        if (!IsStopped)
        {
          item = Queue.Dequeue();

          if( WorkerMethod != null )
          {
            WorkerMethod( item );
          }            

        }
      }
      //Debug.Print("DoRun - END");
      //}      
    }

    public void Run( object param )
    {
      IsStopped = false;
      ContinousActiveObject.OnFinished = OnFinished;
      ContinousActiveObject.WorkerMethod = DoRun;
      ContinousActiveObject.Run(param);
    }

    public void ForceStop()
    {
      ContinousActiveObject.ForceStop();
    }

    #endregion

    #region IActiveObjectQueue<T> Members

    public void Enqueue( T item )
    {
      lock (_syncRoot)
      {
        Queue.Enqueue(item);
        Monitor.Pulse(_syncRoot);
      }
    }

    #endregion
  }
}