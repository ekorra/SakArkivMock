using EduBestServiceStub.Lib;
using EduBestServiceStub.Slack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EduBestServiceStub.SlackTests
{
    [TestClass()]
    public class SlackHookTests
    {
        [TestMethod()]
        public void testnoe()
        {
            var slackNotifier = new SlackNotifier();

            var message = new EduMessage
            {
                Sender = "Sender",
                Receiver = "Receiver",
                ConverstationId = "ConversationId",
                JpId = "JpId",
                JpTitle = "JpTitle"
            };

            slackNotifier.SendNotification(message);
        }
    }
}