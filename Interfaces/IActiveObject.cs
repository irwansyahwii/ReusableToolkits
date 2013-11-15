using System;
using System.Collections.Generic;

namespace ReusableToolkits.Interfaces
{
  public interface IActiveObject
  {    
    Action<object> WorkerMethod { get; set; }
    void Run(object param);
    void ForceStop();
  }
}