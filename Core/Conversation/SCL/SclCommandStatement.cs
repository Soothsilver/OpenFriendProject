using System;
using Core.Conversation.Nodes;
using Core.Conversation.SCL;
using Core.Conversations;

namespace Core.Conversation
{
    internal class SclCommandStatement : SclStatement
    {
        public string Command { get; }
        public string[] Arguments { get; }
        public SclCommandStatement(SclLexicalLine line)
        {
            this.Command = line.Command;
            this.Arguments = line.Arguments;
        }
        public override string ToString()
        {
            return Command + " " + String.Join(" ", Arguments);
        }
        public override ConversationNode ToConversationNode(SclConversation conversation)
        {
            switch (Command)
            {
                case "/goto":
                    return new DelayedMoveNode((friend) =>
                    {
                        return conversation.GetNodeFromLabel(this.Arguments[0]);
                    });
                case "/end":
                    return new DelayedMoveNode((f) => null);
                case "/escape":
                    return new DelayedMoveNode((friend) =>
                    {
                        friend.Memory.ConversationStack.Pop();
                        return conversation.GetNodeFromLabel(this.Arguments[0]);
                    });
                case "/label":
                    DoNothingNode n = new DoNothingNode();
                    conversation.AddLabeledNode(this.Arguments[0], n);
                    return n;
                case "/picture":
                    return new ImageFileLine("Data/" + this.Arguments[0]);
                case "/file":
                    return new DocumentFileLine("Data/" + this.Arguments[0]);
                case "/input":
                    return new Nodes.SetVariableNode(this.Arguments[0]);
                case "/set":
                    return new LambdaActionNode(async (friend) =>
                    {
                        friend.Data.SetVariable(this.Arguments[0], this.Arguments[1]);
                        await friend.Overseer.Speaking.SendSystemMessage(friend, "Configuration property '" + Arguments[0] + "' set to '" + this.Arguments[1] + "'.");
                        friend.SavePersistentMemory();
                    });
                default:
                    if (Command.StartsWith("/special:"))
                    {
                        string specialName = Command.Substring("/special:".Length);
                        return new SpecialActionNode(specialName, Arguments);
                    }
                    return new DoNothingNode();
            }
        }
    }
}