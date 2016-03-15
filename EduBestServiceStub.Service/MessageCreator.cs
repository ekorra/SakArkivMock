using System;
using System.Diagnostics;
using System.Web;
using EduBestServiceStub.Lib.NoarkTypes;

namespace EduBestServiceStub.Service
{
    public class MessageCreator
    {
        private readonly string sernderOrgNr;
        private noarkExchange noarkExchange;

        public MessageCreator(string sernderOrgNr, string endpointUrl)
        {
            this.sernderOrgNr = sernderOrgNr;
            noarkExchange = new noarkExchange {Url = endpointUrl};
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
                result = noarkExchange.GetCanReceiveMessage(message).result;
                Debug.WriteLine("result {result}", result);
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
                result = noarkExchange.PutMessage(request);
            }
            catch (Exception e)
            {
                return false;
            }
            
            return result.result.type == AppReceiptTypeType.OK;
        }

        private static string GetDateString()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        public bool SendAppReceipt(string sender, string receiver, string conversationId)
        {
            var message = new PutMessageRequestType();
            message.envelope = GetEnvelope(receiver, sender, conversationId);
            var payload = GetOkAppReceipt();
            message.Payload = HttpUtility.HtmlEncode(payload);

            Debug.WriteLine("Sending AppReceipt");
            var result =  noarkExchange.PutMessage(message);

            return result.result.type == AppReceiptTypeType.OK;
        }

        private AppReceiptType GetOkAppReceipt()
        {
            var appReceipt = new AppReceiptType();
            appReceipt.type = AppReceiptTypeType.OK;
            appReceipt.message = new StatusMessageType[1];
            appReceipt.message[0] = new StatusMessageType
            {
                code = "ID",
                text = new Random().Next(999999).ToString()
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
