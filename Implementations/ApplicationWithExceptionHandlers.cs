using System;
using System.Net.Mime;
using ReusableToolkits.Interfaces;


namespace ReusableToolkits.Implementations
{
  public class ApplicationWithExceptionHandlers : IApplication
  {
    private GuardUtility guardUtility;

    public IApplication Decoratee { get; set; }
    protected ISystemLog SystemLog;

    public ApplicationWithExceptionHandlers(IApplication decoratee, ISystemLog systemLog)
    {
      guardUtility = new GuardUtility( "ApplicationWithExceptionHandlers" );

      Decoratee = decoratee;
      SystemLog = systemLog;
    }

    #region IApplication Members

    protected virtual void DoInitialize()
    {
      // Create unhandled exception handler.      
      AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler( CurrentDomainUnhandledException );

      Decoratee.Initialize();      
    }

    public void Initialize()
    {
      DoInitialize();
    }

    void CurrentDomainUnhandledException( object sender, UnhandledExceptionEventArgs e )
    {
      const string methodName = "CurrentDomainUnhandledException( object sender, UnhandledExceptionEventArgs e )";
      SystemLog.LogException((Exception) e.ExceptionObject, guardUtility.GetFullMethodName(methodName));
    }

    protected virtual void DoRun()
    {
      Decoratee.Run();
    }

    public void Run()
    {
      DoRun();
    }

    protected virtual void DoStop()
    {
      Decoratee.Stop();
    }

    public void Stop()
    {
      DoStop();
    }

    #endregion
  }
}