using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Conversation.Nodes;

namespace Core.Conversation.SCL
{
    class SclStatementSequence
    {
        public List<SclStatement> Statements = new List<SclStatement>();

        public ConversationNode ToConversationNode(SclConversation conversation)
        {
            ConversationNode first = new DoNothingNode();
            ConversationNode last = first;
            foreach(var statement in Statements)
            {
                ConversationNode from = statement.ToConversationNode(conversation);
                last.FollowingNode = from;
                last = from;
            }
            return first;
        }
    }
}
