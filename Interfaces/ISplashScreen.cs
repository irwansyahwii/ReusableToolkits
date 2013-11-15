using System;


namespace ReusableToolkits.Interfaces
{  
  public interface ISplashScreen
  {
    Action<ISplashScreen> InitializationFunction { get; set; }
    void ShowModal();
    void CloseScreen();    
    void Update(string key, object value);
  }
}