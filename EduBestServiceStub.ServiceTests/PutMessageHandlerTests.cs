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
        [ClassInitialize]
        public void InitializeClass()
        {
            //XmlConfigurator.Configure();
        }

        [TestMethod]
        public void GetResponseTest_BestEduMessage360Style_ReturnsOkResponse()
        {
            var noarkExchangeClient = Substitute.For<INoarkExchange>();
            var request = CrateMessage();

            request.Payload = Resource1.P360BestEduMessagePayload;

            noarkExchangeClient.PutMessage(Arg.Any<PutMessageRequestType>())
                .Returns(new PutMessageResponseType { result = new AppReceiptType { type = AppReceiptTypeType.OK } });

            var putMessageHandler = new PutMessageHandler(noarkExchangeClient);
            var result = putMessageHandler.HandleRequest(request);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.result.type, AppReceiptTypeType.OK);
        }

        [TestMethod]
        public void GetResponseTest_BestEduMessage360Style_SendsAppReceitp()
        {
            var noarkExchangeClient = Substitute.For<INoarkExchange>();
            var request = CrateMessage();

            request.Payload = Resource1.P360BestEduMessagePayload;

            noarkExchangeClient.PutMessage(Arg.Any<PutMessageRequestType>())
                .Returns(new PutMessageResponseType { result = new AppReceiptType { type = AppReceiptTypeType.OK } });

            var putMessageHandler = new PutMessageHandler(noarkExchangeClient);
            var result = putMessageHandler.HandleRequest(request);
            
            noarkExchangeClient.Received().PutMessage(Arg.Any<PutMessageRequestType>());
        }

       [TestMethod]
        public void GetResponseTest_BestEduMessageEphorteStyle_ReturnsOkResponse()
        {
            var noarkExchangeClient = Substitute.For<INoarkExchange>();
            var request = CrateMessage();

            request.Payload = Resource1.EphorteBestEduMessagePayload;

            noarkExchangeClient.PutMessage(Arg.Any<PutMessageRequestType>())
                .Returns(new PutMessageResponseType { result = new AppReceiptType { type = AppReceiptTypeType.OK } });

            var putMessageHandler = new PutMessageHandler(noarkExchangeClient);
            var result = putMessageHandler.HandleRequest(request);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.result.type, AppReceiptTypeType.OK);
        }
        
        [TestMethod]
        public void GetResponseTest_MessageWithoutPayload_ReturnEmptyResponse()
        {
            var noarkExchangeClient = Substitute.For<INoarkExchange>();
            var request = CrateMessage();

            var putMessageHandler = new PutMessageHandler(noarkExchangeClient);
            var result = putMessageHandler.HandleRequest(request);
            
            Assert.IsNotNull(result);
            Assert.AreEqual(result.result.type, AppReceiptTypeType.ERROR);
        }

        [TestMethod]
        public void GetResponseTest_BestEduMessageWebSakStyle_ReturnsOkResponse()
        {
            var noarkExchangeClient = Substitute.For<INoarkExchange>();
            var request = CrateMessage();
            request.Payload = Resource1.WebSakBestEduMessagePayload2;

            noarkExchangeClient.PutMessage(Arg.Any<PutMessageRequestType>())
                .Returns(new PutMessageResponseType { result = new AppReceiptType { type = AppReceiptTypeType.OK } });

            var putMessageHandler = new PutMessageHandler(noarkExchangeClient);
            var result = putMessageHandler.HandleRequest(request);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.result.type, AppReceiptTypeType.OK);
        }

        [TestMethod]
        public void GetResponseTest_BestEduMessageWebSakStyle_SendsAppReceipt()
        {
            var noarkExchangeClient = Substitute.For<INoarkExchange>();
            var request = CrateMessage();
            request.Payload = Resource1.WebSakBestEduMessagePayload2;

            noarkExchangeClient.PutMessage(Arg.Any<PutMessageRequestType>())
                .Returns(new PutMessageResponseType { result = new AppReceiptType { type = AppReceiptTypeType.OK } });

            var putMessageHandler = new PutMessageHandler(noarkExchangeClient);
            var result = putMessageHandler.HandleRequest(request);

            noarkExchangeClient.Received().PutMessage(Arg.Any<PutMessageRequestType>());
        }

        [TestMethod]
        public void GetResponseTest_AppReceiptWebSakStyle_ReturnEmptyResponse()
        {
            var noarkExchangeClient = Substitute.For<INoarkExchange>();
            var request = CrateMessage();
            request.Payload = Resource1.WebSakAppReceipt;

            noarkExchangeClient.PutMessage(Arg.Any<PutMessageRequestType>())
                .Returns(new PutMessageResponseType { result = new AppReceiptType { type = AppReceiptTypeType.OK } });

            var putMessageHandler = new PutMessageHandler(noarkExchangeClient);
            var result = putMessageHandler.HandleRequest(request);

            Assert.IsNotNull(result);
            Assert.IsNull(result.result);
        }

        [TestMethod]
        public void GetResponseTest_EmptyPayload_ReturnsErrorResponse()
        {
            var noarkExchangeClient = Substitute.For<INoarkExchange>();
            var request = CrateMessage();
            request.Payload = null;

            var putMessageHandler = new PutMessageHandler(noarkExchangeClient);
            var result = putMessageHandler.HandleRequest(request);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.result.type, AppReceiptTypeType.ERROR);
        }

        [TestMethod]
        public void GetResponseTest_UnknownPayload_ReturnErrorResponse()
        {
            var noarkExchangeClient = Substitute.For<INoarkExchange>();
            var request = CrateMessage();
            request.Payload = "Something";

            var putMessageHandler = new PutMessageHandler(noarkExchangeClient);
            var result = putMessageHandler.HandleRequest(request);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.result.type, AppReceiptTypeType.ERROR);
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