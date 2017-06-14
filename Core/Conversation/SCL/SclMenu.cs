using System.Collections.Generic;
using Core.Conversation.Nodes;
using Core.Conversation.SCL;

namespace Core.Conversation
{
    class SclMenu : SclStatement
    {
        public string InitialLine { get; }
        public List<SclMenuOption> Options = new List<SclMenuOption>();

        public SclMenu(string initialLine)
        {
            InitialLine = initialLine;
        }
        public override ConversationNode ToConversationNode(SclConversation conversation)
        {
            var replies = new List<PossibleReply>();
            foreach(var option in Options)
            {
                replies.Add(new PossibleReply(option.Caption, option.Contents.ToConversationNode(conversation)));
            }
            return new Branch(InitialLine, replies.ToArray());
        }
    }
    class SclMenuOption
    {
        public string Caption;
        public SclStatementSequence Contents;
    }
}