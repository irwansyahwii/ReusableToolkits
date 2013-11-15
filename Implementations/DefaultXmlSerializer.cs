using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using ReusableToolkits.Interfaces;

namespace ReusableToolkits.Implementations
{
  public class DefaultXmlSerializer : IXmlSerializer
  {
    #region IXmlSerializer Members

    public void SaveToFile( string filePath, object theObject, Type[] includedTypes )
    {
      var serializer = new XmlSerializer( theObject.GetType(), includedTypes );
      using( TextWriter tw = new StreamWriter( filePath, false ) )
      {
        serializer.Serialize( tw, theObject );
      }
    }

    public string ToString( object theObject, Type[] includedTypes )
    {
      var serializer = new XmlSerializer( theObject.GetType(), includedTypes );
      var sbTarget = new StringBuilder();
      using( TextWriter tw = new StringWriter( sbTarget ) )
      {
        serializer.Serialize( tw, theObject );
      }

      return sbTarget.ToString();
    }

    public void SaveToFile(string filePath, object theObject)
    {
      var serializer = new XmlSerializer(theObject.GetType());
      using (TextWriter tw = new StreamWriter(filePath, false))
      {
        serializer.Serialize(tw, theObject);
      }
    }

    public string ToString(object theObject)
    {
      var serializer = new XmlSerializer(theObject.GetType());
      var sbTarget = new StringBuilder();
      using (TextWriter tw = new StringWriter(sbTarget))
      {
        serializer.Serialize(tw, theObject);
      }

      return sbTarget.ToString();
    }

    #endregion
  }

}