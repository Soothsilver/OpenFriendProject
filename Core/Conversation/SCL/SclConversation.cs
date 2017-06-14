using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Conversation.Nodes;

namespace Core.Conversation.SCL
{
    class SclConversation : SclStatementSequence
    {
        private Dictionary<string, ConversationNode> _labeledNodes = new Dictionary<string, ConversationNode>();
        public ConversationNode GetNodeFromLabel(string label)
        {
            return _labeledNodes[label];
        }

        public void AddLabeledNode(string label, DoNothingNode node)
        {
            _labeledNodes.Add(label, node);
        }
    }
}
