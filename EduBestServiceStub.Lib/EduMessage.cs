using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using EduBestServiceStub.Lib.NoarkTypes;
using log4net;

namespace EduBestServiceStub.Lib
{
    public class EduMessage
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string ConverstationId { get; set; }
        public string JpId { get; set; }
        public string JpTitle { get; set; }

        public bool IsValid { get; private set; } = true;
        public Dictionary<string,string> ErrorList { get; set; } = new Dictionary<string,string>();
        private ILog log;
        private XDocument xmlPayload;

        public MessageType Type { get; set; }

        public EduMessage()
        {
            log = LogManager.GetLogger(typeof(EduMessage));
        }

        public EduMessage(PutMessageRequestType putMessageRequest)
        {
            Receiver = putMessageRequest.envelope.receiver.orgnr;
            Sender = putMessageRequest.envelope.sender.orgnr;
            ConverstationId = putMessageRequest.envelope.conversationId;

            var doc = GetXmlPayload(putMessageRequest);
            SetMetaInfo(doc);
            Type = GetMessageType(xmlPayload);
        }

        private void SetMetaInfo(XDocument doc)
        {
            JpId = doc.Descendants().FirstOrDefault(n => n.Name == "jpId")?.Value;
            JpTitle = doc.Descendants().FirstOrDefault(n => n.Name == "jpTitle")?.Value;
        }

        private void SetDocument(PutMessageRequestType putMessageRequest)
        {
            var xmlPayload = GetXmlPayload(putMessageRequest);

            if (xmlPayload == null)
            {
                IsValid = false;
                log.Error(Resource.Payload_missing);
                ErrorList.Add("1", Resource.Payload_missing);
            }
        }



        private XDocument GetXmlPayload(PutMessageRequestType putMessageRequest)
        {
            try
            {
                xmlPayload = XDocument.Parse(putMessageRequest.Payload);
            }
            catch (Exception)
            {
                return null;
            }
            return xmlPayload;
        }

        private MessageType GetMessageType(XDocument doc)
        {
            if (doc.Root != null)
            {
                var rootName = doc.Root.Name.LocalName;

                if (rootName == "Melding")
                {
                    log.Info("Messgetype: BestEduMessage");
                    return MessageType.BestEduMessage;
                }
                if (rootName == "AppReceipt")
                {
                    log.Info("MessageType: AppReceipt");
                    return MessageType.AppReceipt;
                }
            }
            log.Error("MessageType: Unknown");
            IsValid = false;
            ErrorList.Add("2", Resource.Unknown_payload);
            return MessageType.Unknown;
        }
    }
}
