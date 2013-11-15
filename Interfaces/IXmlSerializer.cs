using System;

namespace ReusableToolkits.Interfaces
{
  public interface IXmlSerializer
  {
    void SaveToFile( string filePath, object theObject, Type[] includedTypes );
    string ToString( object theObject, Type[] includedTypes );
    void SaveToFile(string filePath, object theObject);
    string ToString(object theObject);
  }
}