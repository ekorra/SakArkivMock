using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduBestServiceStub.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EduBestServiceStub.Lib.NoarkTypes;
using EduBestServiceStub.ServiceTests;
using NSubstitute;

namespace EduBestServiceStub.Service.Tests
{
    [TestClass()]
    public class PutMessageHandlerTests
    {
        [TestMethod]
        public void GetResponseTest_BestEduMessage360Style_ReturnsAppReceitp()
        {
            var noarkExchangeClient = Substitute.For<INoarkExchange>();
            var request = CrateMessage();

            request.Payload = Resource1.P360BestEduMessagePayload;

            noarkExchangeClient.PutMessage(Arg.Any<PutMessageRequestType>())
                .Returns(new PutMessageResponseType { result = new AppReceiptType { type = AppReceiptTypeType.OK } });

            var putMessageHandler = new PutMessageHandler(request, noarkExchangeClient);
            var result = putMessageHandler.GetResponse();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.result.type, AppReceiptTypeType.OK);
            noarkExchangeClient.Received().PutMessage(Arg.Any<PutMessageRequestType>());
        }

       

        [TestMethod]
        public void GetResponseTest_BestEduMessageEphorteStyle_ReturnsAppReceitp()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetResponseTest_AppReceipt360Style_ReturnEmptyResponse()
        {
            var noarkExchangeClient = Substitute.For<INoarkExchange>();
            var request = CrateMessage();
            request.Payload = Resource1.P360AppReceiptPayload;

            var putMessageHandler = new PutMessageHandler(request, noarkExchangeClient);
            var result = putMessageHandler.GetResponse();

            
            Assert.IsNotNull(result);
            Assert.IsNull(result.result);
            noarkExchangeClient.DidNotReceive().PutMessage(Arg.Any<PutMessageRequestType>());
        }

        [TestMethod]
        public void GetResponseTest_AppReceiptWebSakStyle_ReturnEmptyResponse()
        {
            var noarkExchangeClient = Substitute.For<INoarkExchange>();
            var request = CrateMessage();
            request.Payload = Resource1.P360AppReceiptPayload;

            var putMessageHandler = new PutMessageHandler(request, noarkExchangeClient);
            var result = putMessageHandler.GetResponse();

            Assert.IsNotNull(result);
            Assert.IsNull(result.result);
            noarkExchangeClient.DidNotReceive().PutMessage(Arg.Any<PutMessageRequestType>());
        }

        [TestMethod]
        public void GetResponseTest_EmptyPayload_ReturnsErrorResponse()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetResponseTest_UnknownPayload_ReturnErrorResponse()
        {
            throw new NotImplementedException();
        }

        private PutMessageRequestType CrateMessage()
        {
            return new PutMessageRequestType
            {
                envelope = new EnvelopeType
                {
                    conversationId = Guid.NewGuid().ToString(),
                    receiver = new AddressType { email = "email", orgnr = "123456789", name = "receiver" },
                    sender = new AddressType { email = "email", orgnr = "987654321", name = "sender" }
                }
            };
        }
    }
}