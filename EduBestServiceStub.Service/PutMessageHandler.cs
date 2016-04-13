using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using EduBestServiceStub.Lib.NoarkTypes;

namespace EduBestServiceStub.Service
{
    public class PutMessageHandler
    {
        private readonly INoarkExchange noarkExchangeClient;

        public PutMessageHandler(INoarkExchange noarkExchangeClient)
        {
            this.noarkExchangeClient = noarkExchangeClient;
        }

        public PutMessageResponseType GetResponse(PutMessageRequestType putMessageRequest)
        {
            if (string.IsNullOrEmpty(putMessageRequest.Payload))
            {
                return GetErrorResponse("Payload missing");
            }
            
            var doc = GetXmlPayload(putMessageRequest);

            if (doc == null)
            {
                return GetErrorResponse("Something wrong with the payload");
            }

            var messageType = GetMessageType(doc);

            if (messageType == MessageType.BestEduMessage)
            {
                var sendAppReceiptResult = SendAppReceipt(doc, putMessageRequest);
                return sendAppReceiptResult;
            }
            if (messageType == MessageType.AppReceipt)
                return GetAppReceiptResponse(doc);

            return GetErrorResponse("Unknown message");
        }

        private XDocument GetXmlPayload(PutMessageRequestType putMessageRequest)
        {
            string payload;
            payload = GetPayload(putMessageRequest);

            XDocument doc;
            try
            {
                doc = XDocument.Parse(payload);
            }
            catch (Exception)
            {
                return null;
            }
            return doc;
        }

        private string GetPayload(PutMessageRequestType putMessageRequest)
        {
            var document = XDocument.Load(new StringReader($"<payload>{putMessageRequest.Payload}</payload>"));

            var cdata = document.DescendantNodes().OfType<XCData>().FirstOrDefault();
            var xmlString = cdata != null ? cdata.Value : HttpUtility.HtmlDecode(putMessageRequest.Payload);
            return xmlString;
        }

        private PutMessageResponseType GetAppReceiptResponse(XDocument doc)
        {
            return new PutMessageResponseType();
        }

        private MessageType GetMessageType(XDocument doc)
        {
            if (doc.Root != null)
            {
                var rootName = doc.Root.Name.LocalName;

                if (rootName == "Melding")
                {
                    return MessageType.BestEduMessage;
                }
                if (rootName == "AppReceipt")
                {
                    return MessageType.AppReceipt;
                }
            }
            return MessageType.Unknown;
        }

        private enum MessageType
        {
            Unknown,
            AppReceipt,
            BestEduMessage
        }

       

        private PutMessageResponseType SendAppReceipt(XDocument doc, PutMessageRequestType putMessageRequest)
        {
            var journalpost = doc.Descendants("journpost").FirstOrDefault();

            //var melding = GetMelding(putMessageRequest.Payload);
            var messageCreator = new MessageCreator(Resource.Organisasjonsnummer, noarkExchangeClient);

            var receiver = putMessageRequest.envelope.receiver.orgnr;
            var sender = putMessageRequest.envelope.sender.orgnr;
            var conversationId = putMessageRequest.envelope.conversationId;

            Debug.WriteLine($"Mottatt melding fra: {sender}, til: {receiver} conversationId: {conversationId} journalpostId: {journalpost}");

            try
            {
                var appReceipt = messageCreator.GetAppReceipt(sender, receiver, conversationId);
                Debug.WriteLine("Sending AppReceipt");
                var result = noarkExchangeClient.PutMessage(appReceipt);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"AppReceipt feilet: {ex.Message}");
                throw;
            }
        }


        public Stream StringToStream(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }


        private PutMessageResponseType GetOkResponse()
        {
            var response = new PutMessageResponseType
            {
                result = new AppReceiptType
                {
                    message = GetStatuMessage("Recno", new Random().Next(999999).ToString()),
                    type = AppReceiptTypeType.OK
                }
            };
            return response;
        }

        private PutMessageResponseType GetErrorResponse(string errormessage)
        {
            var response = new PutMessageResponseType
            {
                result = new AppReceiptType
                {
                    message = GetStatuMessage("ERROR", errormessage),
                    type = AppReceiptTypeType.ERROR
                }
            };
            return response;
        }

        private StatusMessageType[] GetStatuMessage(string code, string text)
        {
            return new[]
            {
                new StatusMessageType
                {
                    code = code,
                    text = text
                }
            };
        }

        private XElement GetElement(XDocument doc, string elementName)
        {
            foreach (XNode node in doc.DescendantNodes())
            {
                if (node is XElement)
                {
                    XElement element = (XElement)node;
                    if (element.Name.LocalName.Equals(elementName))
                        return element;
                }
            }
            return null;
        }
    }
}