using System;
using EduBestServiceStub.Lib.NoarkTypes;

namespace EduBestServiceStub.Console
{
    class Program
    {
        private static noarkExchange noarkExchange;

        static void Main(string[] args)
        {
            const string senderOrgNr = "910075918";
            System.Console.WriteLine("Started");
            MessageSender messageSender = new MessageSender(senderOrgNr);

            bool canSend = false;
            string orgnr = string.Empty;

            try
            {
                canSend = messageSender.CanSend(orgnr);
                System.Console.WriteLine("{orgrnr} can receive messages: {canSend}");
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }

            if (canSend)
            {
                var result = messageSender.SendMessage("910077473");
                System.Console.WriteLine("Message {0} sent", result?"was":"was not");
            }

            System.Console.ReadKey();
        }

        

        

        
    }
}
