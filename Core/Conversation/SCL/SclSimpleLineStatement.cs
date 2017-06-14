using System;
using Core.Conversation.SCL;

namespace Core.Conversation
{
    internal class SclSimpleLineStatement : SclStatement
    {
        public string Line { get; }
        public SclSimpleLineStatement(SclLexicalLine line)
        {
            this.Line = line.PureText;
        }
        public override string ToString()
        {
            return Line;
        }
        public override ConversationNode ToConversationNode(SclConversation conversation)
        {
            return new Line(this.Line);
        }
    }
}