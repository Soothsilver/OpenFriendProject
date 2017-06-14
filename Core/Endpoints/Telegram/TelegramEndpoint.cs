using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core.Conversation;
using Newtonsoft.Json;

namespace Core.Endpoints.Telegram
{
    public class TelegramEndpoint : IEndpoint
    {
        private Overseer overseer;
        private HttpClient client = new HttpClient();

        public TelegramEndpoint(Overseer overseer)
        {
            this.overseer = overseer;
        }

        public void HandleUpdate(TelegramUpdate update)
        {
            if (update.Message != null)
            {
                var f = overseer.Persons.GetFriendFromTelegramId(update.Message.From.Id);
                overseer.Senses.IncomingTextMessage(f, update.Message.Text);
            }
        }

        public async Task SendImageUrl(Friend friend, string url)
        {
            await PostJsonMessage("sendPhoto", new
            {
                ChatId = friend.Memory.Persistent.TelegramId,
                Photo = url
            });
        }

        public async Task SendMessage(Friend friend, string message, QuickReply[] quickReplies)
        {
            if (quickReplies == null)
            {
                await PostJsonMessage("sendMessage", new
                {
                    ChatId = friend.Memory.Persistent.TelegramId,
                    Text = message,
                    ReplyMarkup = new
                    {
                        RemoveKeyboard = true
                    }
                });
            }
            else
            {
                await PostJsonMessage("sendMessage", new
                {
                    ChatId = friend.Memory.Persistent.TelegramId,
                    Text = message,
                    ReplyMarkup = new
                    {
                        ResizeKeyboard = true,
                        OneTimeKeyboard = true,
                        Keyboard = quickReplies.Select(reply=> new[] { new { Text = reply.Title }  }).ToArray()
                    }
                });
            }
        }

        public async Task SenderAction(Friend friend, NonMessageAction action)
        {
            if (action == NonMessageAction.TypingOn)
            {
                await PostJsonMessage("sendChatAction", new
                {
                    ChatId = friend.Memory.Persistent.TelegramId,
                    Action = "typing"
                });
            }
        }

        public async Task SendFile(Friend friend, string filename)
        {

            string url = TelegramCommunicator.GetUrlFromQuery("sendDocument", null);
            using (var form = new MultipartFormDataContent())
            {
                form.Add(new StringContent(friend.Memory.Persistent.TelegramId.ToString(), Encoding.UTF8), "chat_id");
                using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    form.Add(new StreamContent(fileStream), "document", System.IO.Path.GetFileName(filename));
                    await client.PostAsync(url, form);
                }
            }
        }

        private async Task PostJsonMessage(string method, object contents)
        {
            var json = JsonConvert.SerializeObject(contents, Auxiliary.JsonSerializerSettings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            string url = TelegramCommunicator.GetUrlFromQuery(method, null);
            var response =
                await client.PostAsync(url, content);
            string responseString = await response.Content.ReadAsStringAsync();

        }
    }
}
