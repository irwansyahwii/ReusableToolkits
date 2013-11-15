using StructureMap;

namespace ReusableToolkits.Implementations
{
  /// <summary>
  /// This class is not trying to abstract the IoC framework used but what this class do 
  /// is making calls to IoC framework easier because when using StructureMap and typing
  /// object the code completion will return object type instead of ObjectFactory.
  /// </summary>
  public class DependencyInjection
  {
    public static T GetInstance<T>()
    {
      return ObjectFactory.GetInstance<T>();
    }

    public static T GetInstance<T>(string name)
    {
      return ObjectFactory.GetNamedInstance<T>(name);
    }

    public static void Use<T1>(object concreteInstance, string named)
    {
      ObjectFactory.Configure( x => x.For( typeof( T1 ) ).Use( concreteInstance ).Named( named ) );
    }

    public static void Use<T1>(object concreteInstance)
    {
      ObjectFactory.Configure(x => x.For(typeof (T1)).Use(concreteInstance));
    }

    public static void Use<T1, T2>()
    {

      ObjectFactory.Configure( x => { x.AddType(typeof (T1), typeof (T2)); } );
    }
  }
}