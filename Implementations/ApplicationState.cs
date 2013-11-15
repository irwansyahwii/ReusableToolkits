using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReusableToolkits.Interfaces;

namespace ReusableToolkits.Implementations
{
  public class ApplicationState : IApplicationState
  {
    private ApplicationStates _currentState;
    protected ISystemLog SystemLog;
    public ApplicationState( ISystemLog systemLog )
    {
      _currentState = ApplicationStates.StartState;
      SystemLog = systemLog;
    }
    #region IApplicationState Members

    protected void ChangeStateTo( ApplicationStates newState )
    {
      SystemLog.LogInfo( "Changing application state from :{0} to {1}", _currentState, newState );
      _currentState = newState;

    }

    public void EventInitializeStart()
    {
      SystemLog.LogInfo( "Received EventInitializeStart()" );

      if( GetCurrentState() == ApplicationStates.StartState )
      {
        ChangeStateTo( ApplicationStates.InitializingState );
        if( FunctionInitialize != null )
        {
          SystemLog.LogInfo( "Executing FunctionInitialize()" );
          FunctionInitialize();
        }
      }
    }

    public void EventException( Exception exception )
    {
      SystemLog.LogInfo( "Received EventException()" );

      if( GetCurrentState() == ApplicationStates.InitializingState
        || GetCurrentState() == ApplicationStates.RunningState
        || GetCurrentState() == ApplicationStates.ShuttingDown
        )
      {
        ChangeStateTo( ApplicationStates.ShowingExceptionDialogState );
        if( FunctionDisplayAndLogException != null )
        {
          SystemLog.LogInfo( "Executing FunctionDisplayAndLogException()" );
          FunctionDisplayAndLogException( exception );
        }
      }
    }

    public void EventCloseDialog()
    {
      SystemLog.LogInfo( "Received EventCloseDialog()" );

      switch( GetCurrentState() )
      {
        case ApplicationStates.ShowingExceptionDialogState:
          {
            ChangeStateTo( ApplicationStates.FinalState );
            if( FunctionExitApp != null )
            {
              SystemLog.LogInfo( "Executing FunctionExitApp()" );
              FunctionExitApp();
            }
          }
          break;
      }
    }

    public void EventInitialized()
    {
      SystemLog.LogInfo( "Received EventInitialized()" );

      if( GetCurrentState() == ApplicationStates.InitializingState )
      {
        ChangeStateTo( ApplicationStates.RunningState );
        if( FunctionRun != null )
        {
          SystemLog.LogInfo( "Executing FunctionRun()" );
          FunctionRun();
        }
      }
    }

    public void EventQuitConfirmed()
    {
      SystemLog.LogInfo( "Received EventQuitConfirmed()" );

      ApplicationStates currentState = GetCurrentState();
      if( currentState == ApplicationStates.ConfirmingQuitState )
      {
        ChangeStateTo( ApplicationStates.ShuttingDown );
        if( FunctionShuttingDown != null )
        {
          SystemLog.LogInfo( "Executing FunctionShuttingDown()" );
          FunctionShuttingDown();
        }
      }
    }

    public void EventQuitCancelled()
    {
      SystemLog.LogInfo( "Received EventQuitCancelled()" );

      ApplicationStates currentState = GetCurrentState();
      if( currentState == ApplicationStates.ConfirmingQuitState )
      {        
        ChangeStateTo( ApplicationStates.RunningState );
      }
    }

    public ApplicationStates GetCurrentState()
    {
      return _currentState;
    }

    public Action FunctionInitialize
    {
      get;
      set;
    }

    public Action<Exception> FunctionDisplayAndLogException
    {
      get;
      set;
    }

    public Action FunctionRun
    {
      get;
      set;
    }

    public Action FunctionExitApp
    {
      get;
      set;
    }

    public Action FunctionShuttingDown
    {
      get;
      set;
    }

    public void EventTryQuit()
    {
      SystemLog.LogInfo( "Received EventTryQuit()" );

      if( GetCurrentState() == ApplicationStates.RunningState )
      {
        ChangeStateTo( ApplicationStates.ConfirmingQuitState );
        if( FunctionShowConfirmQuit != null )
        {
          SystemLog.LogInfo( "Executing FunctionShowConfirmQuit()" );
          FunctionShowConfirmQuit();
        }
      }
    }

    public Action FunctionShowConfirmQuit
    {
      get;
      set;
    }

    public void EventShuttingDownSuccess()
    {
      SystemLog.LogInfo( "Received EventShuttingDownSuccess()" );

      if( GetCurrentState() == ApplicationStates.ShuttingDown )
      {
        ChangeStateTo( ApplicationStates.FinalState );
        if( FunctionExitApp != null )
        {
          SystemLog.LogInfo( "Executing FunctionExitApp()" );
          FunctionExitApp();
        }
      }
    }

    #endregion
  }
}
