using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EduBestServiceStub.Lib.NoarkTypes;

namespace EduBestServiceStub.Lib
{
    public class EduMessage
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string ConverstationId { get; set; }
        public string JpId { get; set; }
        public string JpTitle { get; set; }

        public EduMessage()
        {
            
        }
        public EduMessage(PutMessageRequestType putMessageRequest)
        {
            Receiver = putMessageRequest.envelope.receiver.orgnr;
            Sender = putMessageRequest.envelope.sender.orgnr;
            ConverstationId = putMessageRequest.envelope.conversationId;

            var doc = GetXmlPayload(putMessageRequest);
            JpId = doc.Descendants().FirstOrDefault(n => n.Name == "jpId")?.Value;
            JpTitle = doc.Descendants().FirstOrDefault(n => n.Name == "jpTitle")?.Value;
        }

        private XDocument GetXmlPayload(PutMessageRequestType putMessageRequest)
        {
            XDocument doc;
            try
            {
                doc = XDocument.Parse(putMessageRequest.Payload);
            }
            catch (Exception)
            {
                return null;
            }
            return doc;
        }
    }
}
