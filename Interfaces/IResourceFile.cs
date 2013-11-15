using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ReusableToolkits.Interfaces
{
  public interface IResourceFile
  {
    Bitmap GetBitmap(string bitmapFullQualifiedNameWithNamespace);
    string GetText(string fullyQualifiedName);
    string GetTextFromAssembly(Assembly assembly, string fullyQualifiedName);
  }
}
