using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ReusableToolkits.Interfaces;

namespace ReusableToolkits.Implementations
{
  public class WpfMessageBox : IMessageBox
  {
    #region IMessageBox Members

    public void Show( object parentWindow, string message )
    {
      if(parentWindow != null){
        System.Windows.MessageBox.Show( (Window) parentWindow, message );
      }
      else{
        System.Windows.MessageBox.Show( message );
      }
      
    }

    public void ShowError( object parentWindow, string caption, string message )
    {
      if( parentWindow != null )
      {
        System.Windows.MessageBox.Show( (Window) parentWindow, message, caption, MessageBoxButton.OK, MessageBoxImage.Error );
      }
      else
      {
        System.Windows.MessageBox.Show( message, caption, MessageBoxButton.OK, MessageBoxImage.Error );
      }      
    }

    #endregion
  }
}
