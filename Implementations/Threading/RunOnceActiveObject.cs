using System;
using System.Diagnostics;
using System.Threading;
using ReusableToolkits.Interfaces;

namespace ReusableToolkits.Implementations.Threading
{
  public class RunOnceActiveObject : IActiveObject
  {
    #region IActiveObject Members

    public Action<object> WorkerMethod { get; set; }

    public void Run(object param)
    {
      ThreadPool.QueueUserWorkItem(state =>
                                     {
                                       //Debug.Print("Calling worker method...");
                                       WorkerMethod(param);
                                       //Debug.Print("Finish calling worker method.");
                                     });
    }

    #endregion

    #region IActiveObject Members


    public void ForceStop()
    {
      
    }

    #endregion
  }
}