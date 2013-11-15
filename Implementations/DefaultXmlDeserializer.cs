using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using ReusableToolkits.Interfaces;

namespace ReusableToolkits.Implementations
{
  public class DefaultXmlDeserializer : IXmlDeserializer
  {
    #region IXmlDeserializer Members

    public object ToObject(string xmlString, Type objectType)
    {
      var serializer = new XmlSerializer( objectType );
      //serializer.UnknownAttribute +=new XmlAttributeEventHandler(serializer_UnknownAttribute);
      //serializer.UnknownElement += new XmlElementEventHandler( serializer_UnknownElement );
      //serializer.UnknownNode += new XmlNodeEventHandler( serializer_UnknownNode );
      //serializer.UnreferencedObject += new UnreferencedObjectEventHandler( serializer_UnreferencedObject );
      return serializer.Deserialize(new StringReader(xmlString));
    }

    void serializer_UnreferencedObject( object sender, UnreferencedObjectEventArgs e )
    {
      throw new NotImplementedException();
    }

    void serializer_UnknownNode( object sender, XmlNodeEventArgs e )
    {
      throw new NotImplementedException();
    }

    void serializer_UnknownElement( object sender, XmlElementEventArgs e )
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IXmlDeserializer Members


    public object ToObject( string xmlString, Type objectType, Type[] extraTypes )
    {
      var serializer = new XmlSerializer( objectType, extraTypes );
      //serializer.UnknownAttribute += new XmlAttributeEventHandler( serializer_UnknownAttribute );
      return serializer.Deserialize( new StringReader( xmlString ) );
      
    }

    void serializer_UnknownAttribute( object sender, XmlAttributeEventArgs e )
    {
      
    }

    #endregion

    #region IXmlDeserializer Members


    public object ToObject( string xmlString, Type objectType, XmlNamespaceManager namespaceManager )
    {
      XmlSerializer pageDeserializer = new XmlSerializer( objectType );

      using( TextReader txReader = new StringReader( xmlString ) )
      {
        // Create XmlReaderSettings
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.ConformanceLevel = ConformanceLevel.Auto;
        settings.IgnoreWhitespace = true;
        settings.IgnoreComments = true;
        


        // Create the XmlParserContext using the previous declared XmlNamespaceManager
        XmlParserContext ctx = new XmlParserContext(null, namespaceManager, null, XmlSpace.None);
        

        // Instantiate a new XmlReader, using the previous declared XmlReaderSettings and XmlParserContext
        XmlReader reader = XmlReader.Create(txReader, settings, ctx);

        // Finally, deserialize
        return pageDeserializer.Deserialize(reader);
      }
    }

    #endregion
  }

  public class NamespaceIgnorantXmlTextReader : XmlTextReader
  {
    public NamespaceIgnorantXmlTextReader( System.IO.TextReader reader ) : base( reader ) { }

    public override string NamespaceURI
    {
      get { return ""; }
    }
  }

  public class XmlDeserializerIgnoringNamespace : IXmlDeserializer
  {

    public object ToObject( string xmlString, Type objectType )
    {
      var serializer = new XmlSerializer(objectType);
      
      return serializer.Deserialize(new NamespaceIgnorantXmlTextReader(new StringReader(xmlString)));
    }

    public object ToObject( string xmlString, Type objectType, Type[] extraTypes )
    {
      var serializer = new XmlSerializer( objectType, extraTypes );
      
      
      serializer.UnknownElement += new XmlElementEventHandler( serializer_UnknownElement );
      return serializer.Deserialize( new NamespaceIgnorantXmlTextReader( new StringReader( xmlString ) ) );
    }

    void serializer_UnknownElement( object sender, XmlElementEventArgs e )
    {
      
    }

    


    public object ToObject( string xmlString, Type objectType, XmlNamespaceManager namespaceManager )
    {
      throw new NotImplementedException();
    }

  }



}