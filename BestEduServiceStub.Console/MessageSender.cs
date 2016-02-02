using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduBestServiceStub.Lib.NoarkTypes;
using log4net.Util;

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
            var fiksObject = new Mapper.FiksDataObject
            {
                DokumentetsDato = GetDateString(),
                Journalaar = DateTime.Now.Year.ToString(),
                Journaldato = GetDateString(),
                Journalpostnummer = "1",
                Journalposttype = "U",
                Journalseksvensnummer = "1234",
                Journalstatus = "R",
                ReceiverOrgNr = receiverOrgnr,
                SenderOrgNr = sernderOrgNr,
                Saksbehandler = "En saksbehandler",
                Saksekvensnummer = "1",
                Saksaar = DateTime.Now.Year.ToString(),
                Tittel = "En tittel"
            };

            var request = fiksObject.GetBestEduMelding();

            var result = noarkExchange.PutMessage(request);
            return result.result.type == AppReceiptTypeType.OK;
        }

        private static string GetDateString()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}
