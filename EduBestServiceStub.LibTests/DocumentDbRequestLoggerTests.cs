using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduBestServiceStub.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduBestServiceStub.Lib.NoarkTypes;
using Microsoft.Azure.Documents.Client;

namespace EduBestServiceStub.Lib.Tests
{
    [TestClass()]
    public class DocumentDbRequestLoggerTests
    {
        [TestMethod(), Ignore]
        public void LogTest()
        {
            PutMessageRequestType request = new PutMessageRequestType
            {
                envelope = new EnvelopeType
                {
                    conversationId = Guid.NewGuid().ToString(),
                    receiver = new AddressType {orgnr = "receiver"},
                    sender = new AddressType {orgnr = "sender"}
                },
                Payload = ""
            };
            var EndpointUri = @"";
            var primaryKey = "";
            var client = new DocumentClient(new Uri(EndpointUri), primaryKey, ConnectionPolicy.Default);

            var logger = new DocumentDbRequestLogger(client);

            logger.Log(request);
        }
    }
}