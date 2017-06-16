using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core.Conversation;
using Core.Endpoints.Telegram;
using Newtonsoft.Json;

namespace Core.Endpoints
{
    public class FacebookEndpoint : IEndpoint
    {
        private Overseer overseer;
        private HttpClient client = new HttpClient();
        private string PageAccessToken = Configuration.PageAccessToken;

        private void HandleSingleCallback(Callback cb)
        {
            if (cb.Message != null)
            {
                Friend f = overseer.Persons.GetFriendFromFacebookId(cb.Sender.Id);
                overseer.Senses.IncomingTextMessage(f, cb.Message.Text.Replace("\\n", Environment.NewLine));
            }
            if (cb.Read != null)
            {
                overseer.Speaking.Debug("Message read.");
            }
            if (cb.Delivery != null)
            {
                //overseer.Speaking.Debug("Message delivered.");
            }
            if (cb.Postback != null)
            {
                Friend f = overseer.Persons.GetFriendFromFacebookId(cb.Sender.Id);
                overseer.Senses.IncomingTextMessage(f, cb.Postback.Payload);
            }
        }

        public void IncomingCallback(string jsonText)
        {
            var callback = JsonConvert.DeserializeObject<CallbackEnvelope>(jsonText, Auxiliary.JsonSerializerSettings);
            foreach (var entry in callback.Entry)
            {
                foreach (var cb in entry.Messaging)
                {
                    HandleSingleCallback(cb);
                }
            }
        }

        public FacebookEndpoint(Overseer overseer)
        {
            this.overseer = overseer;
        }

        public async Task SendImageUrl(Friend friend, string url)
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

        public async Task SendMessage(Friend friend, string message, QuickReply[] quickReplies)
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

        public async Task SenderAction(Friend friend, NonMessageAction action)
        {
            string senderAction = "";
            switch (action)
            {
                case NonMessageAction.MarkSeen: senderAction = "mark_seen"; break;
                case NonMessageAction.TypingOff:
                    senderAction = "typing_off";
                    friend.Speaking.EndTypingToHome();
                    break;
                case NonMessageAction.TypingOn:
                    senderAction = "typing_on";
                    friend.Speaking.BeginTypingToHome();
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

        public Task SendFile(Friend friend, string filename)
        {
            return SendMessage(friend, "FILE CONTENTS:" + Environment.NewLine + System.IO.File.ReadAllText(filename), null);
        }

        public Task SendImageFile(Friend friend, string filepath)
        {
            return SendMessage(friend, "IMAGE UPLOAD NOT SUPPORTED ON FACEBOOK.", null);
            /*
            string url = "https://graph.facebook.com/v2.6/me/messages?access_token=" + PageAccessToken;
            using (var form = new MultipartFormDataContent())
            {
                form.Add(new StringContent("{\"id\":\"" + friend.Data.FacebookId + "\"}", Encoding.UTF8),
                    "recipient");
                form.Add(new StringContent(@"{""attachment"" : { ""type"":""image"", ""payload"":{}}}", Encoding.UTF8),
                    "message");
                using (FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                {
                    form.Add(new StreamContent(fileStream), "filedata", System.IO.Path.GetFileName(filepath));
                    await client.PostAsync(url, form);
                }
            }*/
        }

        private async Task PostJsonMessage(object contents)
        {
            var json = JsonConvert.SerializeObject(contents, Auxiliary.JsonSerializerSettings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response =
                await client.PostAsync("https://graph.facebook.com/v2.6/me/messages?access_token=" + PageAccessToken,
                    content);
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
    }
}
