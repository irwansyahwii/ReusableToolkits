using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace ReusableToolkits.Interfaces
{
  public interface IMessageBox
  {
    /// <summary>
    /// Show a regular message box with an OK button.
    /// </summary>
    /// <param name="parentWindow">Pass null if no parent window needed</param>
    /// <param name="message">Required</param>
    void Show( object parentWindow, string message );

    /// <summary>
    /// Show a message box with default icons to represent error and an OK button.
    /// </summary>
    /// <param name="parentWindow">Pass null if no parent window needed</param>
    /// <param name="caption">Pass null or empty string to use default caption</param>
    /// <param name="message">Required</param>
    void ShowError(object parentWindow, string caption, string message );
  }
}
