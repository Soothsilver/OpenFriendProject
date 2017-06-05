using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Core
{
    public class Senses
    {
        private Overseer overseer;

        public Senses(Overseer overseer)
        {
            this.overseer = overseer;
        }

        private async Task HandleSingleCallback(Callback cb)
        {
            if (cb.Message != null)
            {
                await StartProcessingMessage(cb.Sender.Id, cb.Message.Text.Replace("\\n", Environment.NewLine));
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
                await StartProcessingMessage(cb.Sender.Id, cb.Postback.Payload);
            }
        }
        private Task StartProcessingMessage(string senderId, string message)
        {
            overseer.Speaking.Debug("Incoming message: " + message);
            Friend friend = overseer.Persons.GetFriend(senderId);
            friend.StartProcessingInput(message);
            return Task.FromResult(0);
        }



        public async Task IncomingCallback(string jsonText)
        {
            var callback = JsonConvert.DeserializeObject<CallbackEnvelope>(jsonText, Auxiliary.JsonSerializerSettings);
            foreach(var entry in callback.Entry)
            {
                foreach(var cb in entry.Messaging) {
                    await HandleSingleCallback(cb);
                }
            }
        }


        public async Task IncomingTextMessage(string id, string text)
        {
            await StartProcessingMessage(id, text.Replace("\\n", Environment.NewLine));
        }
    }
}
