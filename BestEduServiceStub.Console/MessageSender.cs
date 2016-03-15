using System;
using EduBestServiceStub.Lib.NoarkTypes;

namespace EduBestServiceStub.Console
{
    public class MessageSender
    {
        private readonly string sernderOrgNr;
        private noarkExchange noarkExchange;

        public MessageSender(string sernderOrgNr)
        {
            this.sernderOrgNr = sernderOrgNr;
            noarkExchange = new noarkExchange();
            noarkExchange.Url = "http://test-win-meldingsutveksling-app01.difi.local:9093/noarkExchange";
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
                System.Console.WriteLine("result {result}", result);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
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
    }
}
