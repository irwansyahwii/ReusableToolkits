using System.Drawing;
using System.IO;
using System.Reflection;
using ReusableToolkits.Interfaces;

namespace ReusableToolkits.Implementations
{
  public class ResourceFileFromEntryAssembly : IResourceFile
  {

    #region IResourceFile Members

    public Bitmap GetBitmap( string bitmapFullQualifiedNameWithNamespace )
    {
      Assembly assembly = GetAssembly();

      var resourceStream = assembly.GetManifestResourceStream(bitmapFullQualifiedNameWithNamespace);
      if (resourceStream == null)
      {
        assembly = System.Reflection.Assembly.GetExecutingAssembly();
        resourceStream = assembly.GetManifestResourceStream( bitmapFullQualifiedNameWithNamespace );
      }
      if( resourceStream == null )
      {
        assembly = System.Reflection.Assembly.GetCallingAssembly();
        resourceStream = assembly.GetManifestResourceStream( bitmapFullQualifiedNameWithNamespace );
      }

      Bitmap bmp = new Bitmap(resourceStream);

      return bmp;
    }

    #endregion

    protected Assembly GetAssembly()
    {
      Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();

      if( assembly == null )
      {
        assembly = System.Reflection.Assembly.GetExecutingAssembly();
        if (assembly.GetManifestResourceNames().Length <= 0)
        {
          assembly = System.Reflection.Assembly.GetCallingAssembly();
        }
      }
      return assembly;
    }

    #region IResourceFile Members


    public string GetText( string fullyQualifiedName )
    {
      Assembly assembly = GetAssembly();

      return GetTextFromAssembly(assembly, fullyQualifiedName);
    }

    #endregion

    #region IResourceFile Members


    public string GetTextFromAssembly( Assembly assembly, string fullyQualifiedName )
    {
      StreamReader streamReader = new StreamReader( assembly.
        GetManifestResourceStream( fullyQualifiedName ) );

      return streamReader.ReadToEnd();
    }

    #endregion
  }
}