using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReusableToolkits.Interfaces;

namespace ReusableToolkits.Implementations
{
  public abstract class ApplicationRunningWithTimerEverySecond : IApplication
  {
    private readonly GuardUtility guardUtility;
    protected bool IsInitialized;
    protected ITimer RunTimer;
    protected ISystemLog SystemLog;
    protected int TimerIntervalInSeconds;

    public ApplicationRunningWithTimerEverySecond( ISystemLog systemLog, ITimer runTimer )
    {
      guardUtility = new GuardUtility( "ApplicationRunningWithTimerEverySecond" );

      SystemLog = systemLog;
      RunTimer = runTimer;
      IsInitialized = false;
      TimerIntervalInSeconds = 1;
    }

    #region IApplication Members

    public void Initialize()
    {
      const string methodName = "Initialize()";

      SystemLog.LogMethodStart( new FullMethodName( guardUtility, methodName ) );

      IsInitialized = true;
      DoInitialization();

      SystemLog.LogMethodEnds( new FullMethodName( guardUtility, methodName ) );
    }

    public void Run()
    {
      if( !IsInitialized )
      {
        SystemLog.LogInfo( "Application is not initialized, initializing application..." );
        Initialize();
      }
      SystemLog.LogInfo( "Stopping timer" );
      RunTimer.Stop();
      SystemLog.LogInfo( "Calling application core function" );
      DoRun();
      SystemLog.LogInfo( "Starting timer" );
      RunTimer.Start();
    }

    public void Stop()
    {
      RunTimer.Stop();
    }

    #endregion

    protected virtual void InitTimer( int timerIntervalInSeconds )
    {
      RunTimer.InitTimer( o =>
      {
        SystemLog.LogInfo( "Timer elapsed" );
        Run();
      }, timerIntervalInSeconds );
    }

    protected virtual void DoInitialization()
    {
      InitTimer( TimerIntervalInSeconds );
    }

    protected abstract void DoRun();
  }
}
