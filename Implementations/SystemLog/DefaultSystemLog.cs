using System;
using log4net;
using log4net.Config;
using ReusableToolkits.Interfaces;

namespace ReusableToolkits.Implementations.SystemLog
{
  public class DefaultSystemLog : ISystemLog
  {
    protected static ILog log;

    //private static readonly ILog log = LogManager.GetLogger(typeof(DefaultSystemLog));

    public DefaultSystemLog()
    {
      log = LogManager.GetLogger(typeof (DefaultSystemLog));
    }

    #region ISystemLog Members

    public void SetLoggerName(string loggerName)
    {
      log = LogManager.GetLogger(loggerName);
    }

    public string GetLoggerName()
    {
      return log.Logger.Name;
    }

    public void Initialize()
    {
      XmlConfigurator.Configure();
    }

    public void LogException(Exception ex, string fullMethodName)
    {
      log.Error(fullMethodName, ex);
    }

    public void LogInfo(string message, params object[] args)
    {
      log.Info(string.Format(message, args));
    }

    public void LogInfo(string message)
    {
      log.Info(message);
    }

    public void LogWarning( string message, params object[] args )
    {
      log.Warn( string.Format( message, args ) );
    }

    public void LogWarning( string message )
    {
      log.Warn( message );
    }

    public void LogMethodStart( IFullMethodName fullMethodName )
    {
      LogMethodStart(fullMethodName.GetFullMethodName());
    }

    public void LogMethodReturningWithResult(IFullMethodName fullMethodName, string resultName, object resulValue)
    {
      LogMethodReturningWithResult(fullMethodName.GetFullMethodName(), resultName, resulValue);
    }

    public void LogMethodEnds(IFullMethodName fullMethodName)
    {
      LogMethodEnds(fullMethodName.GetFullMethodName());
    }

    #endregion

    public void LogMethodStart(string methodName)
    {
      LogInfo("START - {0}", methodName);
    }

    public void LogMethodReturningWithResult(string methodName, string resultName, object resulValue)
    {
      LogInfo("END - {0}. Returning with {1}:{2}", methodName, resultName, resulValue);
    }

    public void LogMethodEnds(string methodName)
    {
      LogInfo("END - {0}", methodName);
    }
  }
}