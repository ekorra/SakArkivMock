using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using EduBestServiceStub.Lib.NoarkTypes;
using log4net;

namespace EduBestServiceStub.Service
{
    public class PutMessageHandler
    {
        private readonly INoarkExchange noarkExchangeClient;
        private ILog log;

        public PutMessageHandler(INoarkExchange noarkExchangeClient)
        {
            log = LogManager.GetLogger(typeof(NoarkExchange));
            this.noarkExchangeClient = noarkExchangeClient;
        }

        public PutMessageResponseType HandleRequest(PutMessageRequestType putMessageRequest)
        {
            if (string.IsNullOrEmpty(putMessageRequest.Payload))
            {
                log.Error(Resource.Payload_missing);
                return GetErrorResponse(Resource.Payload_missing);
            }
            
            var doc = GetXmlPayload(putMessageRequest);

            if (doc == null)
            {
                log.Error(Resource.Unknown_payload);
                return GetErrorResponse(Resource.Unknown_payload);
            }

            var messageType = GetMessageType(doc);

            var receiver = putMessageRequest.envelope.receiver.orgnr;
            var sender = putMessageRequest.envelope.sender.orgnr;
            var conversationId = putMessageRequest.envelope.conversationId;
            var jpId = doc.Descendants().FirstOrDefault(n => n.Name == "jpId")?.Value;


            log.Info($"Mottatt melding fra: {sender}, til: {receiver} conversationId: {conversationId} journalpostId: {jpId}");


            if (messageType == MessageType.BestEduMessage)
            {
                PutMessageResponseType sendAppReceiptResult;
                try
                {
                    var id = new Random().Next(999999).ToString();
                    log.Info($"Created archiveId: {id}");
                    sendAppReceiptResult = SendAppReceipt(sender, receiver, conversationId, id);


                    if (sendAppReceiptResult.result.type == AppReceiptTypeType.OK)
                    {
                        var response = GetOkResponse(id);
                        sendAppReceiptResult = response;
                    }
                }
                catch (WebException e)
                {
                    var message = e.InnerException?.Message ?? e.Message;
                    log.Error(e.Message, e);
                    sendAppReceiptResult = GetErrorResponse(message);
                }
                return sendAppReceiptResult;
            }
            if (messageType == MessageType.AppReceipt)
                return GetAppReceiptResponse(doc);

            return GetErrorResponse("Unknown message");
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
                    log.Info("Messgetype: BestEduMessage");
                    return MessageType.BestEduMessage;
                }
                if (rootName == "AppReceipt")
                {
                    log.Info("MessageType: AppReceipt");
                    return MessageType.AppReceipt;
                }
            }
            log.Warn("MessageType: Unknown");
            return MessageType.Unknown;
        }

        private enum MessageType
        {
            Unknown,
            AppReceipt,
            BestEduMessage
        }

       

        private PutMessageResponseType SendAppReceipt(string sender, string receiver, string conversationId, string id)
        {
            var messageCreator = new MessageCreator(noarkExchangeClient);
            
            var appReceipt = messageCreator.GetAppReceipt(sender, receiver, conversationId, id);
            Debug.WriteLine("Sending AppReceipt");
            var result = noarkExchangeClient.PutMessage(appReceipt);
                
            log.Info("AppReciept sendt");
            return result;
        }
        
        private PutMessageResponseType GetOkResponse(string id)
        {
            var response = new PutMessageResponseType
            {
                result = new AppReceiptType
                {
                    message = GetStatuMessage("Recno", id),
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