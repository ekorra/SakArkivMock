using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapper;

namespace Fiks
{
    class Program
    {
        static void Main(string[] args)
        {
            var fiksObject = new FiksDataObject
            {
                DokumentetsDato = GetDateString(),
                Journalaar = DateTime.Now.Year.ToString(),
                Journaldato = GetDateString(),
                Journalpostnummer = "1",
                Journalposttype = "U",
                Journalseksvensnummer = "1234",
                Journalstatus = "R",
                ReceiverOrgNr = "",
                SenderOrgNr = "",
                Saksbehandler = "Espen",
                Saksekvensnummer = "1",
                Saksaar = DateTime.Now.Year.ToString(),
                Tittel = "En tittel"
            };

            var request = fiksObject.GetBestEduMelding();
        }

        private static string GetDateString()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}
