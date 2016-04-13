using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using EduBestServiceStub.Lib;
using EduBestServiceStub.Lib.NoarkTypes;

namespace EduBestServiceStub.Service
{
    public class MessageCreator
    {
        
        private INoarkExchange noarkExchangeClient;

        public MessageCreator(INoarkExchange noarkExchangeClient)
        {
            this.noarkExchangeClient = noarkExchangeClient;
        }

        public bool CanSend(string receiverOrgnr)
        {
            bool result = false;
            try
            {
                var message = new GetCanReceiveMessageRequestType();
                message.receiver = new AddressType
                {
                    orgnr = receiverOrgnr,
                };
                result = noarkExchangeClient.GetCanReceiveMessage(message).result;
                Debug.WriteLine($"result {result}", result);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return result;
        }

        public bool SendMessage(string receiverOrgnr)
        {
            
            //fiks
            var request = new PutMessageRequestType();
            PutMessageResponseType result;

            try
            {
                result = noarkExchangeClient.PutMessage(request);
            }
            catch (Exception)
            {
                return false;
            }
            
            return result.result.type == AppReceiptTypeType.OK;
        }

        public PutMessageRequestType GetAppReceipt(string sender, string receiver, string conversationId, string id)
        {
            var message = new PutMessageRequestType();
            message.envelope = GetEnvelope(receiver, sender, conversationId);
            var appReceipt = GetOkAppReceipt(id);
            var xmlString = appReceipt.SerializeObject();
            message.Payload = xmlString;

            return message;
        }

        private AppReceiptType GetOkAppReceipt(string id)
        {
            var appReceipt = new AppReceiptType();
            appReceipt.type = AppReceiptTypeType.OK;
            appReceipt.message = new StatusMessageType[1];
            appReceipt.message[0] = new StatusMessageType
            {
                code = "ID",
                text = id
            };
            return appReceipt;

        }


        private EnvelopeType GetEnvelope(string receiver, string sender, string conversationId)
        {
            var envelope = new EnvelopeType
            {
                conversationId = conversationId,
                sender = new AddressType {orgnr = sender},
                receiver = new AddressType {orgnr = receiver}
            };
            return envelope;
        }
    }
}
