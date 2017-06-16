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
            friend.Speaking.SayToHome(url);
            return Task.FromResult(0);
        }

        public Task SendMessage(Friend friend, string message, QuickReply[] quickReplies)
        {
            friend.Speaking.SayToHome(message);
            friend.Speaking.SetQuickRepliesToHome(quickReplies);
            return Task.FromResult(0);
        }

        public Task SenderAction(Friend friend, NonMessageAction action)
        {
            switch (action)
            {
                case NonMessageAction.TypingOff:
                    friend.Speaking.EndTypingToHome();
                    break;
                case NonMessageAction.TypingOn:
                    friend.Speaking.BeginTypingToHome();
                    break;
            }
            return Task.FromResult(0);
        }

        public Task SendFile(Friend friend, string filename)
        {
            friend.Speaking.SendFilenameToHome(filename);
            return Task.FromResult(0);
        }

        public Task SendImageFile(Friend friend, string filepath)
        {
            friend.Speaking.SendFilenameToHome(filepath);
            return Task.FromResult(0);
        }
    }
}
