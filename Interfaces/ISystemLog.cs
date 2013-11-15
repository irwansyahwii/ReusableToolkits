using System;

namespace ReusableToolkits.Interfaces
{ 
  public interface ISystemLog
  {
    void Initialize();
    void LogException(Exception ex, string fullMethodName);
    void LogInfo(string message, params object[] args);
    void LogInfo(string message);
    void LogWarning( string message, params object[] args );
    void LogWarning( string message );
    //void LogMethodStart(string methodName);
    void LogMethodStart(IFullMethodName fullMethodName);
    //void LogMethodReturningWithResult(string methodName, string resultName, string resulValue);
    void LogMethodReturningWithResult(IFullMethodName fullMethodName, string resultName, object resulValue);
    //void LogMethodEnds(string methodName);
    void LogMethodEnds(IFullMethodName fullMethodName);
    void SetLoggerName(string loggerName);
    string GetLoggerName();
  }
}