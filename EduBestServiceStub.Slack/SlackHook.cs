using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Slack.Webhooks;

namespace EduBestServiceStub.Slack
{
    public class SlackHook
    {
        private readonly string SlackUrl =
            "https://hooks.slack.com/services/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";

        private SlackClient slackClient;

        public SlackHook()
        {
            slackClient = new SlackClient(SlackUrl);
        }

        public void PublishMessage()
        {
            var slackMessage = new SlackMessage
            {
                Channel = "#sakarkivmock",
                Text = "God damn I'm good",
                IconEmoji = Emoji.Muscle,
                Username = "espenssakarkiv"
            };

            
            var slackAttachment = new SlackAttachment
            {
                Fallback = "New open task [Urgent]: <http://url_to_task|Test out Slack message attachments>",
                Text = "New open task *[Urgent]*: <http://url_to_task|Test out Slack message attachments>",
                Color = "#D00000",
                Fields =
            new List<SlackField>
                {
                    new SlackField
                        {
                            Title = "Notes",
                            Value = "This is much *easier* than I thought it would be."
                        },
                    new SlackField
                        {
                            Title = "Short2Title",
                            Value = "Short2value",
                            Short = true
                        },
                    new SlackField
                        {
                            Title = "Short3Title",
                            Value = "Short3value",
                            Short = true
                        },
                    new SlackField
                        {
                            Title = "Short4Title",
                            Value = "Short4value",
                            Short = true
                        }

                }
            
            };
            slackMessage.Attachments = new List<SlackAttachment> { slackAttachment };

            var result = slackClient.Post(slackMessage);
        }
    }
}
