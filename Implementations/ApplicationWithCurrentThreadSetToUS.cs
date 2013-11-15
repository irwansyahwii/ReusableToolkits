using System;
using System.Globalization;
using System.Threading;
using ReusableToolkits.Interfaces;

namespace ReusableToolkits.Implementations
{
  public class ApplicationWithCultureSetToUS : IApplication
  {
    private GuardUtility guardUtility;
    public IApplication Decoratee { get; set; }

    public ApplicationWithCultureSetToUS( IApplication decoratee )
    {
      const string methodName = "ApplicationWithCultureSetToUS( IApplication decoratee )";

      guardUtility = new GuardUtility( "ApplicationWithCultureSetToUS" );
      
      guardUtility.GuardParamNotNull(decoratee, "decoratee", methodName);

      Decoratee = decoratee;
    }

    #region IApplication Members

    public void Initialize()
    {
      CultureInfo cultureInfo = CultureInfo.GetCultureInfo("en-US");

      Thread.CurrentThread.CurrentCulture = cultureInfo;
      Thread.CurrentThread.CurrentUICulture = cultureInfo;

      Decoratee.Initialize();
    }

    public void Run()
    {
      Decoratee.Run();
    }

    public void Stop()
    {
      Decoratee.Stop();
    }

    #endregion
  }
}