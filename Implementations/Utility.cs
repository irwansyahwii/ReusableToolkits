using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReusableToolkits.Implementations
{
  public class Utility
  {
    public static string CleanForFileName( string fileName )
    {
      if( !string.IsNullOrEmpty( fileName ) )
      {
        return Path.GetInvalidFileNameChars().Aggregate( fileName, ( current, c ) => current.Replace( c.ToString(), string.Empty ) );
      }
      return fileName;
    }
  }
}
