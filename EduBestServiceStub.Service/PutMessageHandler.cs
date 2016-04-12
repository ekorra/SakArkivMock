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
        private readonly PutMessageRequestType putMessageRequest;
        private readonly INoarkExchange noarkExchangeClient;

        public PutMessageHandler(PutMessageRequestType putMessageRequest, INoarkExchange noarkExchangeClient)
        {
            this.putMessageRequest = putMessageRequest;
            this.noarkExchangeClient = noarkExchangeClient;
        }

        public PutMessageResponseType GetResponse()
        {
            if (string.IsNullOrEmpty(putMessageRequest.Payload))
            {
                return GetErrorResponse("Payload missing");
            }

            var decoded = HttpUtility.HtmlDecode(putMessageRequest.Payload);
            var doc = XDocument.Parse(decoded);

            var messageType = GetMessageType(doc);

            if (messageType == MessageType.BestEduMessage)
            {
                var sendAppReceiptResult = SendAppReceipt(doc);
                return sendAppReceiptResult;
            }
            if (messageType == MessageType.AppReceipt)
                return GetAppReceiptResponse(doc);

            return GetErrorResponse("Unknown message");
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

       

        private PutMessageResponseType SendAppReceipt(XDocument doc)
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
                throw ex;
            }
            return GetOkResponse();
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