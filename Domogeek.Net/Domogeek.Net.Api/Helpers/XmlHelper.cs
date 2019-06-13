using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Domogeek.Net.Api.Helpers
{
    public static class XmlHelper
    {
        public static string XmlSerialize<T>(T value) where T : class
        {
            if (value == null) return null;
            try
            {
                var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                var serializer = new XmlSerializer(typeof(T));
                var settings = new XmlWriterSettings { OmitXmlDeclaration = true };
                //settings.Indent = true;

                using (var stream = new StringWriter())
                using (var writer = XmlWriter.Create(stream, settings))
                {
                    serializer.Serialize(writer, value, emptyNamepsaces);
                    return stream.ToString();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public static T XmlDeserialize<T>(string value) where T : class
        {
            if (value == null) return null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var stream = new StringReader(value))
                    return serializer.Deserialize(stream) as T;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
