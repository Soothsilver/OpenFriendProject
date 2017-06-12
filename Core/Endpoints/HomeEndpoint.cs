using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Conversation;

namespace Core.Endpoints
{
    public class HomeEndpoint : IEndpoint
    {
        private Overseer overseer;

        public HomeEndpoint(Overseer overseer)
        {
            this.overseer = overseer;
        }

        public void SendMessage(Friend friend, string text)
        {
            overseer.Senses.IncomingTextMessage(friend, text);
        }

        public Task SendImageUrl(Friend friend, string url)
        {
            friend.Speaking.Say(url);
            return Task.FromResult(0);
        }

        public Task SendMessage(Friend friend, string message, QuickReply[] quickReplies)
        {
            friend.Speaking.Say(message);
            friend.Speaking.SetQuickReplies(quickReplies);
            return Task.FromResult(0);
        }

        public Task SenderAction(Friend friend, NonMessageAction action)
        {
            switch (action)
            {
                case NonMessageAction.TypingOff:
                    friend.Speaking.EndTyping();
                    break;
                case NonMessageAction.TypingOn:
                    friend.Speaking.BeginTyping();
                    break;
            }
            return Task.FromResult(0);
        }

        public Task SendFile(Friend friend, string filename)
        {
            friend.Speaking.SendFilename(filename);
            return Task.FromResult(0);
        }
    }
}
