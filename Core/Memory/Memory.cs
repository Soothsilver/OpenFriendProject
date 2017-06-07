using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Memory
    {
        public LongTermMemory Persistent;
        public Friend Friend { get; }

        public Alice.Alice Alice;
        public Memory(Friend friend)
        {
            this.Friend = friend;
            this.Persistent = new LongTermMemory();
        }
        public ConversationNode CurrentConversation;
        public bool TalkingToAlice;

        public async Task MoveConversationTo(ConversationNode node, Overseer overseer)
        {
            CurrentConversation = node;
            if (CurrentConversation != null)
            {
                await CurrentConversation.Enter(overseer, this.Friend);
            }
            else
            {
                await overseer.Speaking.SendMessage(this.Friend,
                    "Say 'hi' when you want to talk again!",
                    new[] {  new Conversation.QuickReply("Hello, Amethyst!") });
            }
        }
    }
}
