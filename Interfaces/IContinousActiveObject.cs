using System;

namespace ReusableToolkits.Interfaces
{
  public interface IContinousActiveObject : IActiveObject
  {
    void Stop();
    Action<IContinousActiveObject> OnFinished { get; set; }
  }

  public interface IActiveObjectQueue<T> : IContinousActiveObject
  {
    void Enqueue(T item);    
  }
}