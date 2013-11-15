using System;
using ReusableToolkits.Interfaces;

namespace ReusableToolkits.Implementations.SystemLog
{
  public class LogToConsole : ISystemLog
  {
    public ISystemLog Decoratee { get; set; }

    #region ISystemLog Members

    public void Initialize()
    {
      if ( Decoratee != null )
      {
        Decoratee.Initialize();
      }
    }

    public void LogException( Exception ex, string fullMethodName )
    {
      Console.WriteLine( fullMethodName + ", " + ex );

      if ( Decoratee != null )
      {
        Decoratee.LogException( ex, fullMethodName );
      }

    }

    public void LogInfo( string message, params object[] args )
    {
      Console.WriteLine( message, args );

      if ( Decoratee != null )
      {
        Decoratee.LogInfo( message, args );
      }

    }

    public void LogInfo( string message )
    {
      Console.WriteLine( message );

      if ( Decoratee != null )
      {
        Decoratee.LogInfo( message );
      }

    }

    public void LogWarning( string message, params object[] args )
    {
      Console.WriteLine( "WARNING: " + message, args );

      if ( Decoratee != null )
      {
        Decoratee.LogWarning( message, args );
      }

    }

    public void LogWarning( string message )
    {
      Console.WriteLine( "WARNING: " + message );

      if ( Decoratee != null )
      {
        Decoratee.LogWarning( message );
      }

    }

    public void SetLoggerName( string loggerName )
    {
      if ( Decoratee != null )
      {
        Decoratee.SetLoggerName( loggerName );
      }

    }

    public string GetLoggerName()
    {
      return "";
    }

    public void LogMethodStart( IFullMethodName fullMethodName )
    {
      LogMethodStart( fullMethodName.GetFullMethodName() );

      if ( Decoratee != null )
      {
        Decoratee.LogMethodStart( fullMethodName );
      }
    }

    public void LogMethodReturningWithResult( IFullMethodName fullMethodName, string resultName, object resulValue )
    {
      LogMethodReturningWithResult( fullMethodName.GetFullMethodName(), resultName, resulValue );

      if ( Decoratee != null )
      {
        Decoratee.LogMethodReturningWithResult( fullMethodName, resultName, resulValue );
      }
    }

    public void LogMethodEnds( IFullMethodName fullMethodName )
    {
      LogMethodEnds( fullMethodName.GetFullMethodName() );

      if ( Decoratee != null )
      {
        Decoratee.LogMethodEnds( fullMethodName );
      }
    }

    #endregion

    public void LogMethodStart( string methodName )
    {
      Console.WriteLine( "START - {0}", methodName );

    }

    public void LogMethodReturningWithResult( string methodName, string resultName, object resulValue )
    {
      Console.WriteLine( "END - {0}. {1}:{2}", methodName, resultName, resulValue );

    }

    public void LogMethodEnds( string methodName )
    {
      Console.WriteLine( "END - {0}.", methodName );

    }
  }
}