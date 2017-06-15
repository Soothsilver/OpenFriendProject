using System;
using System.Threading.Tasks;
using Core.Conversation;

namespace Core
{
    public class FriendMouth
    {
        private readonly Friend _friend;

        public FriendMouth(Friend friend)
        {
            _friend = friend;
        }

        public event Action<string> Message;
        public event Action<QuickReply[]> QuickRepliesSet;
        public event Action TypingBegan;
        public event Action TypingEnded;
        public event Action<string> FilenameSent;

        internal void BeginTypingToHome()
        {
            TypingBegan?.Invoke();
        }
        internal void EndTypingToHome()
        {
            TypingEnded?.Invoke();
        }

        internal void SayToHome(string msg)
        {
            TypingEnded?.Invoke();
            Message?.Invoke(msg);
        }
        internal void SendFilenameToHome(string filename)
        {
            FilenameSent?.Invoke(filename);
        }
        internal void SetQuickRepliesToHome(QuickReply[] replies)
        {
            QuickRepliesSet?.Invoke(replies);
        }

        public Task Say(string msg)
        {
            return _friend.Overseer.Speaking.SendMessage(_friend, msg);
        }
    }
}