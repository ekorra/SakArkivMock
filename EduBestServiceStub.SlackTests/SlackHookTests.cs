using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduBestServiceStub.Slack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EduBestServiceStub.Lib;
using EduBestServiceStub.SlackTests;
using NSubstitute;
using Slack.Webhooks;

namespace EduBestServiceStub.Slack.Tests
{
    [TestClass()]
    public class SlackHookTests
    {
        [TestMethod()]
        public void PublishMessageTest()
        {
            var slackHook = new SlackHook();

            var message = new EduMessage
            {
                Sender = "Sender",
                Receiver = "Receiver",
                ConverstationId = "ConversationId",
                JpId = "JpId",
                JpTitle = "JpTitle"
            };

            slackHook.PublishMessage(message);
        }
    }
}