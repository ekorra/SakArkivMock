using System.IO;
using System.Xml;
using System.Xml.Serialization;
using EduBestServiceStub.Lib.NoarkTypes;

namespace EduBestServiceStub.Lib
{
    public class NoarkExchangeXmlFileReader
    {
        public object GetMessage(string filepath)
        {
            object result = null;
            if (File.Exists(filepath))
            {
                using (var filestream = new FileStream(filepath, FileMode.Open))
                {
                    var xmlSerializer = new XmlSerializer(typeof(PutMessageRequestType));
                    result = xmlSerializer.Deserialize(filestream);
                }
            }
            return result;
        }
    }
}
