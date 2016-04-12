using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using EduBestServiceStub.Lib.NoarkTypes;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace EduBestServiceStub.Lib
{
    public static class Dumper
    {
        public static string DumpToString(this object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented);
        }

        public static Stream StringToStream(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }

    public static class XmlHelper
    {
       public static string SerializeObject<T>(this T toSerialize)
        {
           var settings = new XmlWriterSettings
           {
               OmitXmlDeclaration = true,
               Indent = true
           };

           var xmlSerializer = new XmlSerializer(toSerialize.GetType());
            
            using (var textWriter = new StringWriter())
            {
                var writer = XmlWriter.Create(textWriter, settings);
                xmlSerializer.Serialize(writer, toSerialize);
                return textWriter.ToString();
            }
        }
    }



}