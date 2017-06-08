using System;
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

        internal void BeginTyping()
        {
            TypingBegan?.Invoke();
        }
        internal void EndTyping()
        {
            TypingEnded?.Invoke();
        }

        internal void Say(string msg)
        {
            TypingEnded?.Invoke();
            Message?.Invoke(msg);
        }
        internal void SetQuickReplies(QuickReply[] replies)
        {
            QuickRepliesSet?.Invoke(replies);
        }
    }
}