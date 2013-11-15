using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReusableToolkits.Interfaces;

namespace ReusableToolkits.Implementations
{
  public abstract class ApplicationWithSplashScreen : IApplication
  {
    private GuardUtility GuardUtility;
    protected ISplashScreen SplashScreen;
    protected ISystemLog SystemLog;


    public ApplicationWithSplashScreen( ISplashScreen splashScreen
      , ISystemLog systemLog
      )
    {
      GuardUtility = new GuardUtility( "ApplicationWithSplashScreen" );
      SplashScreen = splashScreen;
      SystemLog = systemLog;
    }

    protected virtual void RunInsideExceptionHandler( Action action, string methodName, Action finallyAction )
    {
      try
      {
        action();
      }
      catch( Exception ex )
      {
        ShowErrorMessageBox( string.Format( "Error:{0}", ex.ToString() ), "Error" );
        SystemLog.LogException( ex, GuardUtility.GetFullMethodName( methodName ) );
      }
      finally
      {
        if( finallyAction != null ) 
        {
          finallyAction();
        }        
      }
    }

    #region IApplication Members

    protected bool IsApplicationProperlyInitialized;
    public void Initialize()
    {
      IsApplicationProperlyInitialized = false;
      const string methodName = "Initialize()";
      SplashScreen.InitializationFunction = screen =>
      {
        RunInsideExceptionHandler( () =>
        {
          DoInitializationWhileShowingSplashScreen();
          IsApplicationProperlyInitialized = true;
        }, methodName
                                    , () =>
                                    {
                                      screen.CloseScreen();
                                    } );

      };
    }

    public void Run()
    {
      const string methodName = "Run()";

      RunInsideExceptionHandler( () =>
      {
        SplashScreen.ShowModal();
        SplashScreen = null;

        if( IsApplicationProperlyInitialized )
        {
          DoRunApplicationOnThePlatform();
        }


      }, methodName, null );


    }

    protected abstract void DoRunApplicationOnThePlatform();
    protected abstract void DoInitializationWhileShowingSplashScreen();
    protected abstract void ShowErrorMessageBox( string message, string caption );
    protected abstract void DoShutdown();

    public void Stop()
    {
      DoShutdown();
      //const string methodName = "Stop()";
      //RunInsideExceptionHandler(() =>
      //                            {                                    
      //                              Application.Exit();                                    
      //                            }, methodName, null);
    }

    #endregion
  }
}
