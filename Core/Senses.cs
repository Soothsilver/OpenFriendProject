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
                Friend f = overseer.Persons.GetFriendFromFacebookId(cb.Sender.Id);
                await StartProcessingMessage(f, cb.Message.Text.Replace("\\n", Environment.NewLine));
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
                await StartProcessingMessage(f, cb.Postback.Payload);
            }
        }
        private Task StartProcessingMessage(Friend friend, string message)
        {
            overseer.Speaking.Debug("Incoming message: " + message);
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


        public async Task IncomingTextMessage(Friend friend, string text)
        {
            await StartProcessingMessage(friend, text.Replace("\\n", Environment.NewLine));
        }
    }
}
