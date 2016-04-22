using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduBestServiceStub.Slack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EduBestServiceStub.SlackTests;

namespace EduBestServiceStub.Slack.Tests
{
    [TestClass()]
    public class SlackHookTests
    {
        [TestMethod()]
        public void PublishMessageTest()
        {
           SlackHook slackHook = new SlackHook();

           var payload = XDocument.Parse(Resource.PayloadXml);
            
           //slackHook.PublishMessage();
        }
    }
}