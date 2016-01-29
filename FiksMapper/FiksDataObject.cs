using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using EduBestServiceStub.Lib.NoarkTypes;

namespace Mapper
{
    public class FiksDataObject
    {

        const string BetsEduNamespace = "http://www.arkivverket.no/Noark4-1-WS-WD/types";

        public string SenderOrgNr { get; set; }

        public string ReceiverOrgNr { get; set; }
        public string Saksekvensnummer { get; set; }

        public string Saksaar { get; set; }

        public string Journalaar { get; set; }

        public string Journalseksvensnummer { get; set; }

        public string Journalpostnummer { get; set; }

        public string Journalposttype { get; set; }

        public string Journalstatus { get; set; }

        public string Journaldato { get; set; }

        public string DokumentetsDato { get; set; }

        public string Tittel { get; set; }

        public string Saksbehandler { get; set; }

        public byte[] Fil { get; set; }

        public Dictionary<string,string> EkstraMetadata { get; set; }

        public FiksDataObject()
        {
        }

        public FiksDataObject(JournpostType journpost, NoarksakType noarksak)
        {
            Saksekvensnummer = noarksak.saSeknr;
            Saksaar = noarksak.saSaar;
            Journalposttype = journpost.jpNdoktype;
            Journalstatus = journpost.jpStatus;
            DokumentetsDato = journpost.jpDokdato;
            Fil = journpost.dokument.First().fil.Item;
        }

        

        public PutMessageRequestType GetBestEduMelding()
        {
            return new PutMessageRequestType
            {
                envelope = CreateEnvelope(),
                Payload = CreatePayload()
            };
        }

        private string CreatePayload()
        {
            var melding = GetMelding();
            var serializer = new System.Xml.Serialization.XmlSerializer(melding.GetType());

            string encodedXml;
            using (StringWriter sww = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                serializer.Serialize(writer, melding);
                var xml = sww.ToString();
                encodedXml = HttpUtility.HtmlEncode(xml);
            }

            return encodedXml;
        }

        private MeldingType GetMelding()
        {
            var melding = new MeldingType
            {
                journpost = new JournpostType
                {
                    jpJaar = Journalaar,
                    jpSeknr = Journalseksvensnummer,
                    jpJpostnr = Journalpostnummer,
                    jpNdoktype = Journalposttype,
                    jpStatus = Journalstatus,
                    jpJdato = Journaldato,
                    jpDokdato = DokumentetsDato,
                    dokument = GetDokumentType()
                },
                noarksak = new NoarksakType
                {
                    saSeknr = Saksekvensnummer,
                    saSaar = Saksaar,
                    saAnsvnavn = Saksbehandler
                }
            };
            return melding;
        }

        private DokumentType[] GetDokumentType()
        {
            var dok = new DokumentType[1];
            dok[0] = new DokumentType { fil = new FilType { Item = Fil }, dbTittel = Tittel };
            return dok;
        }

        private EnvelopeType CreateEnvelope()
        {
            return new EnvelopeType()
            {
                conversationId = Guid.NewGuid().ToString(),
                contentNamespace = BetsEduNamespace,
                sender = new AddressType { orgnr = SenderOrgNr },
                receiver = new AddressType { orgnr = ReceiverOrgNr},
            };
        }
    }
}
