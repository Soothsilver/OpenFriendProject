using Core.Conversation.SCL;

namespace Core.Conversation
{
    internal abstract class SclStatement
    {
        public abstract ConversationNode ToConversationNode(SclConversation conversation);
    }
}