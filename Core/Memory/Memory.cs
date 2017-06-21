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
        
        public Memory(Friend friend)
        {
            this.Friend = friend;
            this.Persistent = new LongTermMemory();
        }
        public ConversationNode CurrentConversation;
        public Stack<ConversationNode> ConversationStack = new Stack<ConversationNode>();
        public bool TalkingToAlice;

        public async Task MoveConversationTo(ConversationNode node, Overseer overseer)
        {
            CurrentConversation = node;
            if (CurrentConversation != null)
            {
                await CurrentConversation.Enter(overseer, this.Friend);
            }
            else if (ConversationStack.Count > 0)
            {
                await ConversationStack.Pop().Enter(overseer, this.Friend);
            }
            else
            {
                /*
                await overseer.Speaking.SendMessage(this.Friend,
                    "I have no further topics. Say 'hi' when you want to talk again.",
                    new[] {  new Conversation.QuickReply("Hello, {name}!") });
                */
            }
        }

        public Task PushConversation(ConversationNode node, Overseer overseer)
        {
            if (CurrentConversation != null)
            {
                ConversationStack.Push(CurrentConversation);
            }
            return MoveConversationTo(node, overseer);
        }
    }
}
