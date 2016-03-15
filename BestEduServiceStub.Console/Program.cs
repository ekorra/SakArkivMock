using System;
using EduBestServiceStub.Lib;
using EduBestServiceStub.Lib.NoarkTypes;
using EduBestServiceStub.Service;

namespace EduBestServiceStub.Console
{
    class Program
    {
        private static noarkExchange noarkExchange;

        static void Main(string[] args)
        {
            const string senderOrgNr = "910075918";
            System.Console.WriteLine("Started");
            MessageCreator messageCreator = new MessageCreator(senderOrgNr, "");

            bool canSend = false;
            string receiverOrgnr = string.Empty;
            System.Console.WriteLine("Enter receivers orgnr:");
            receiverOrgnr = System.Console.ReadLine();
            
            try
            {
                canSend = messageCreator.CanSend(receiverOrgnr);
                System.Console.WriteLine("{orgrnr} can receive messages: {canSend}");
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }

            if (canSend)
            {
                var result = messageCreator.SendMessage("910077473");
                System.Console.WriteLine("Message {0} sent", result?"was":"was not");
            }

            System.Console.WriteLine("Hit a key to continue");
            System.Console.ReadKey();
        }
    }
}
