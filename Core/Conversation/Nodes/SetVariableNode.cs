using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Conversation.Nodes
{
    class SetVariableNode : ConversationNode
    {
        private readonly string _variablename;

        public SetVariableNode(string variablename)
        {
            _variablename = variablename;
        }
        public override Task Enter(Overseer overseer, Friend friend)
        {
            return Task.FromResult(0);
        }
        public override async Task<bool> ProcessMessage(Overseer overseer, Friend friend, string message)
        {
            friend.Memory.Persistent.SetVariable(_variablename, message);
            friend.SavePersistentMemory();
            await friend.Memory.MoveConversationTo(FollowingNode, overseer);
            return true;
        }
    }
}
