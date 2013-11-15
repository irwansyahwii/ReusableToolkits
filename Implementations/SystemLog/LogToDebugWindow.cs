using System;
using System.Diagnostics;
using ReusableToolkits.Interfaces;

namespace ReusableToolkits.Implementations.SystemLog
{
  public class LogToDebugWindow : ISystemLog
  {
    public ISystemLog Decoratee { get; set; }

    public LogToDebugWindow(ISystemLog decoratee)
    {
      Decoratee = decoratee;
    }

    #region ISystemLog Members

    public void Initialize()
    {
      if( Decoratee != null )
      {
        Decoratee.Initialize();
      }
    }

    public void LogException(Exception ex, string fullMethodName)
    {
      Debug.Print("EXCEPTION in {0}: {1}", ex, fullMethodName);
      if (Decoratee != null)
      {
        Decoratee.LogException(ex, fullMethodName);
      }
    }

    public void LogInfo(string message, params object[] args)
    {
      Debug.Print("INFO: {0}", string.Format(message, args));
      if( Decoratee != null )
      {
        Decoratee.LogInfo(message, args);
      }

    }

    public void LogInfo(string message)
    {
      Debug.Print("INFO:{0}", message);
      if( Decoratee != null )
      {
        Decoratee.LogInfo(message);
      }

    }

    public void LogWarning( string message, params object[] args )
    {
      Debug.Print( "WARNING: {0}", string.Format( message, args ) );
      if ( Decoratee != null )
      {
        Decoratee.LogWarning( message, args );
      }

    }

    public void LogWarning( string message )
    {
      Debug.Print( "WARNING:{0}", message );
      if ( Decoratee != null )
      {
        Decoratee.LogWarning( message );
      }

    }

    public void LogMethodStart( IFullMethodName fullMethodName )
    {
      Debug.Print("START - {0}", fullMethodName);

      if( Decoratee != null )
      {
        Decoratee.LogMethodStart(fullMethodName);
      }

    }

    public void LogMethodReturningWithResult(IFullMethodName fullMethodName, string resultName, object resulValue)
    {
      Debug.Print("END - {2}. Returning with {0}:{1}", resultName, resulValue, fullMethodName);

      if( Decoratee != null )
      {
        Decoratee.LogMethodReturningWithResult(fullMethodName, resultName, resulValue);
      }

    }

    public void LogMethodEnds(IFullMethodName fullMethodName)
    {
      Debug.Print( "END - {0}", fullMethodName );

      if( Decoratee != null )
      {
        Decoratee.LogMethodEnds(fullMethodName);
      }

    }

    public void SetLoggerName(string loggerName)
    {
      lock (WriteLock)
      {
        LoggerName = loggerName;

        if( Decoratee != null )
        {
          Decoratee.SetLoggerName(loggerName);
        }

      }
    }

    protected string LoggerName;
    public string GetLoggerName()
    {
      return LoggerName;
    }

    private readonly object WriteLock = new object();


    #endregion
  }
}