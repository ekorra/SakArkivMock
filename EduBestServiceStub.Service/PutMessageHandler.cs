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
                PutMessageResponseType sendAppReceiptResult;
                try
                {
                    var id = new Random().Next(999999).ToString();
                    sendAppReceiptResult = SendAppReceipt(doc, putMessageRequest, id);
                    if (sendAppReceiptResult.result.type == AppReceiptTypeType.OK)
                    {
                        var response = GetOkResponse(id);
                        sendAppReceiptResult = response;
                    }
                }
                catch (WebException e)
                {
                    var message = e.InnerException?.Message ?? e.Message;
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

       

        private PutMessageResponseType SendAppReceipt(XDocument doc, PutMessageRequestType putMessageRequest, string id)
        {
            var journalpost = doc.Descendants("journpost").FirstOrDefault();

            var messageCreator = new MessageCreator(Resource.Organisasjonsnummer, noarkExchangeClient);

            var receiver = putMessageRequest.envelope.receiver.orgnr;
            var sender = putMessageRequest.envelope.sender.orgnr;
            var conversationId = putMessageRequest.envelope.conversationId;
            

            Debug.WriteLine($"Mottatt melding fra: {sender}, til: {receiver} conversationId: {conversationId} journalpostId: {journalpost}");

            try
            {
                var appReceipt = messageCreator.GetAppReceipt(sender, receiver, conversationId, id);
                Debug.WriteLine("Sending AppReceipt");
                var result = noarkExchangeClient.PutMessage(appReceipt);
                
                return result;
            }
            catch (WebException ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"AppReceipt feilet: {ex.Message}");
                throw;
            }
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