using System;
using System.Diagnostics;
using System.Threading;
using ReusableToolkits.Interfaces;

namespace ReusableToolkits.Implementations.Threading
{
  public class BackgroundThreadActiveObject : IActiveObject
  {
    protected Thread WorkerThread;
    protected Guid ID;

    public BackgroundThreadActiveObject()
    {
      ID = Guid.NewGuid();
    }

    #region IActiveObject Members

    public Action<object> WorkerMethod { get; set; }

    public void Run( object param )
    {
      WorkerThread = new Thread(o =>
                                  {
                                    Debug.Print("START - Calling WorkerMethod...");
                                    WorkerMethod(o);
                                    Debug.Print( "END - Calling WorkerMethod..." );
                                  });
      WorkerThread.Name = "BackgroundThreadActiveObject.ID:" + ID.ToString();
      WorkerThread.IsBackground = true;
      WorkerThread.Start(param);
    }

    #endregion

    #region IActiveObject Members


    public void ForceStop()
    {
      if (WorkerThread.IsAlive)
      {
        WorkerThread.Abort();
      }      
    }

    #endregion
  }
  
}