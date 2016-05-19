using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using EduBestServiceStub.Lib;
using EduBestServiceStub.Lib.NoarkTypes;
using EduBestServiceStub.Slack;
using log4net;
using MessageType = EduBestServiceStub.Lib.MessageType;

namespace EduBestServiceStub.Service
{
    public class PutMessageHandler
    {
        private readonly INoarkExchange noarkExchangeClient;
        private readonly ILog log;
        private readonly INotifier notifier;

        public PutMessageHandler(INoarkExchange noarkExchangeClient, INotifier notifier = null)
        {
            this.notifier = notifier;
            log = LogManager.GetLogger(typeof(PutMessageHandler));
            this.noarkExchangeClient = noarkExchangeClient;
        }

        public PutMessageResponseType HandleRequest(PutMessageRequestType putMessageRequest)
        {
            var eduMessage = new EduMessage(putMessageRequest);
            if (!eduMessage.IsValid)
            {
                return GetErrorResponse(eduMessage.ErrorList);
            }

            log.Info($"Mottatt melding fra: {eduMessage.Sender}, til: {eduMessage.Receiver} conversationId: {eduMessage.ConverstationId} journalpostId: {eduMessage.JpId}");


            if (eduMessage.Type == MessageType.BestEduMessage)
            {
                PutMessageResponseType sendAppReceiptResult;
                try
                {
                    var id = new Random().Next(999999).ToString();
                    log.Info($"Created archiveId: {id}");
                    sendAppReceiptResult = SendAppReceipt(eduMessage.Sender, eduMessage.Receiver, eduMessage.ConverstationId, id);

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
                    sendAppReceiptResult = GetErrorResponse(new Dictionary<string, string> { {"3",message} });
                }
                return sendAppReceiptResult;
            }
            if (eduMessage.Type == MessageType.AppReceipt)
            {
                return GetAppReceiptResponse();
            }


            notifier?.SendNotification(eduMessage);
            return GetErrorResponse(new Dictionary<string, string> { { "3", "Unknown Error" } });
        }

        private PutMessageResponseType GetAppReceiptResponse()
        {
            return new PutMessageResponseType();
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

        
        private PutMessageResponseType GetErrorResponse(Dictionary<string, string> errormessages)
        {
            var response = new PutMessageResponseType
            {
                result = new AppReceiptType
                {
                    message = GetStatuMessages(errormessages),
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

        private StatusMessageType[] GetStatuMessages(Dictionary<string, string> errormessages)
        {
            return errormessages.Select(message => new StatusMessageType {code = message.Key, text = message.Value}).ToArray();
        }

        private XElement GetElement(XDocument doc, string elementName)
        {
            return doc.DescendantNodes().OfType<XElement>().FirstOrDefault(element => element.Name.LocalName.Equals(elementName));
        }
    }
}