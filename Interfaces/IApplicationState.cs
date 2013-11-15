using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReusableToolkits.Interfaces
{
  public interface IApplicationState
  {
    ApplicationStates GetCurrentState();

    Action FunctionInitialize { get; set; }
    Action<Exception> FunctionDisplayAndLogException { get; set; }
    Action FunctionRun { get; set; }
    Action FunctionExitApp { get; set; }
    Action FunctionShuttingDown { get; set; }
    Action FunctionShowConfirmQuit { get; set; }

    void EventInitializeStart();

    void EventException( Exception exception );
    void EventCloseDialog();
    void EventInitialized();
    void EventQuitConfirmed();
    void EventQuitCancelled();
    void EventTryQuit();
    void EventShuttingDownSuccess();
  }
}
