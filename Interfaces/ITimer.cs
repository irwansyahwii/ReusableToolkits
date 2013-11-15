using System;

namespace ReusableToolkits.Interfaces
{
  public interface ITimer
  {
    void InitTimer(Action<object> TimerCallback, int timerIntervalInSeconds);
    void SetInterval(int intervalInSeconds);
    void Start();
    void Stop();
  }
}