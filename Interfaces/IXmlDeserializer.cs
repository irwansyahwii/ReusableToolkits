using System;
using System.Xml;

namespace ReusableToolkits.Interfaces
{
  public interface IXmlDeserializer
  {
    object ToObject(string xmlString, Type objectType);
    object ToObject( string xmlString, Type objectType, Type[] extraTypes );
    object ToObject(string xmlString, Type objectType, XmlNamespaceManager namespaceManager);
  }
}