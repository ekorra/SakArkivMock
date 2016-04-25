using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Slack.Webhooks;

namespace EduBestServiceStub.Slack
{
    public class SlackNotifier
    {
        private readonly string SlackUrl =
            "https://hooks.slack.com/services/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";

        private SlackClient slackClient;
        private bool useShortMessages;

        public SlackNotifier()
        {
            slackClient = new SlackClient(SlackUrl);
            useShortMessages = false;
        }

        public void SendNotification(Lib.EduMessage eduMessage)
        {
            var slackMessage = new SlackMessage
            {
                Channel = "#sakarkivmock",
                Text = $"Melding fra {eduMessage.Sender}",
                IconEmoji = Emoji.Muscle,
                Username = "sakarkmock"
            };

            
            var slackAttachment = new SlackAttachment
            {
                Text = $"Tittel: {eduMessage.JpTitle}",
                Color = "#D3D3D3",
                Fields =
            new List<SlackField>
                {
                    new SlackField
                        {
                            Title = "Fra",
                            Value = eduMessage.Sender,
                            Short = useShortMessages
                        },
                    new SlackField
                        {
                            Title = "Til",
                            Value = eduMessage.Receiver,
                            Short = useShortMessages
                        },
                    new SlackField
                        {
                            Title = "Journlpostnummer",
                            Value = eduMessage.JpId,
                            Short = useShortMessages
                        }
                }
            };
            slackMessage.Attachments = new List<SlackAttachment> { slackAttachment };

            var result = slackClient.Post(slackMessage);
        }
    }
}
