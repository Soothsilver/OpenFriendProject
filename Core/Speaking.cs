using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core.Conversation;
using Newtonsoft.Json;

namespace Core
{
    public class Speaking
    {
        private Overseer overseer;
        private HttpClient client = new HttpClient();
        private string PageAccessToken = Configuration.PageAccessToken;



        public event Action<string> DebugMessage;

        public Speaking(Overseer overseer)
        {
            this.overseer = overseer;
        }

        public void Debug(string s)
        {
            DebugMessage?.Invoke(s);
        }

        public async Task SendImage(Friend friend, string url)
        {
            overseer.Speaking.Debug("Sending an image: " + url);
            if (friend.IsFacebook)
            {
                var contents = new
                {
                    Recipient = new
                    {
                        Id = friend.Memory.Persistent.FacebookId
                    },
                    Message = new
                    {
                        Attachment = new
                        {
                            Type = "image",
                            Payload = new
                            {
                                Url = url
                            }
                        }
                    },
                };
                await PostJsonMessage(contents);
            }
          
             friend.Speaking.Say(url);
            
        }
        public async Task SenderAction(Friend friend, MessengerSenderAction action)
        {
            string senderAction = null;
            switch (action)
            {
                case MessengerSenderAction.MarkSeen: senderAction = "mark_seen"; break;
                case MessengerSenderAction.TypingOff:
                    senderAction = "typing_off";
                    friend.Speaking.EndTyping();
                    break;
                case MessengerSenderAction.TypingOn:
                    senderAction = "typing_on";
                    friend.Speaking.BeginTyping();
                    break;
            }
            if (friend.IsFacebook)
            {
                var contents = new
                {
                    Recipient = new
                    {
                        Id = friend.Memory.Persistent.FacebookId
                    },
                    Sender_action = senderAction
                };
                await PostJsonMessage(contents);
            }
        }
        public async Task SendMessage(Friend friend, string message, QuickReply[] quickReplies = null, bool honorRealisticTypingSpeed = true)
        {
            overseer.Speaking.Debug("Saying: " + message);
            if (friend.HasRealisticTypingSpeed && honorRealisticTypingSpeed)
            {
                await SenderAction(friend, MessengerSenderAction.MarkSeen);
                await SenderAction(friend, MessengerSenderAction.TypingOn);
                await Task.Delay(Math.Min(message.Length * 1000 / 36, 3000));
                await SenderAction(friend, MessengerSenderAction.TypingOff);
            }
            if (friend.IsFacebook)
            {
                var contents = new
                {
                    Recipient = new
                    {
                        Id = friend.Memory.Persistent.FacebookId
                    },
                    Message = new
                    {
                        Text = message,
                        Quick_replies = quickReplies
                    },
                    Notification_type = "SILENT_PUSH"
                };
                await PostJsonMessage(contents);
            }

            friend.Speaking.Say(message);
            friend.Speaking.SetQuickReplies(quickReplies);
            
        }

        private async Task PostJsonMessage(object contents)
        {
            var json = JsonConvert.SerializeObject(contents, Auxiliary.JsonSerializerSettings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://graph.facebook.com/v2.6/me/messages?access_token=" + PageAccessToken, content);
            string responseString = await response.Content.ReadAsStringAsync();
        }

        public async Task PostJsonProfileApiMessage(object contents)
        {
            overseer.Speaking.Debug("Sending configuration message...");
            var json = JsonConvert.SerializeObject(contents, Auxiliary.JsonSerializerSettings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://graph.facebook.com/v2.6/me/messenger_profile?access_token=" + PageAccessToken, content);
            string responseString = await response.Content.ReadAsStringAsync();
            overseer.Speaking.Debug("Response: " + response.StatusCode);
        }

        public Task SendSystemMessage(Friend friend, string message)
        {
            return SendMessage(friend, "SYSTEM: " + message, honorRealisticTypingSpeed: false);
        }
    }

    public enum MessengerSenderAction
    {
        TypingOn,
        TypingOff,
        MarkSeen
    }
}