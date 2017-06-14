using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core.Conversation;
using Newtonsoft.Json;
using MoreLinq;

namespace Core
{
    public class Speaking
    {
        private Overseer overseer;



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
            await friend.Endpoints.ForEachAsync(ep => ep.SendImageUrl(friend, url));
            
        }
        public async Task SenderAction(Friend friend, NonMessageAction action)
        {
            await friend.Endpoints.ForEachAsync(ep => ep.SenderAction(friend, action));
        }
        public async Task SendMessage(Friend friend, string message, QuickReply[] quickReplies = null, bool honorRealisticTypingSpeed = true)
        {
            if (quickReplies != null)
            {
                foreach (var reply in quickReplies)
                {
                    reply.Title = friend.MacroReplacer.ReplaceMacrosInOutgoingText(reply.Title);
                }
            }
            overseer.Speaking.Debug("Saying: " + message);
            if (friend.HasRealisticTypingSpeed && honorRealisticTypingSpeed)
            {
                await SenderAction(friend, NonMessageAction.MarkSeen);
                await SenderAction(friend, NonMessageAction.TypingOn);
                await Task.Delay(Math.Min(message.Length * 1000 / 36, 3000));
                await SenderAction(friend, NonMessageAction.TypingOff);
            }
            await friend.Endpoints.ForEachAsync((ep) => ep.SendMessage(friend, friend.MacroReplacer.ReplaceMacrosInOutgoingText(message), quickReplies));
        }



        public Task SendSystemMessage(Friend friend, string message)
        {
            return SendMessage(friend, "SYSTEM MESSAGE:" + Environment.NewLine + message, honorRealisticTypingSpeed: false);
        }

        public async Task SendFile(Friend friend, string filename)
        {
            await friend.Endpoints.ForEachAsync((ep) => ep.SendFile(friend, filename));
        }
    }
}