using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReusableToolkits.Interfaces
{
  public enum ApplicationStates
  {
    StartState,
    InitializingState,
    ShowingExceptionDialogState,
    RunningState,
    FinalState,
    ConfirmingQuitState,
    ShuttingDown
  }
}
