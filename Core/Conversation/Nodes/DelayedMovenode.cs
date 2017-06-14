using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Conversation.Nodes
{
    class DelayedMoveNode : ConversationNode
    {
        private readonly Func<Friend, ConversationNode> _func;

        public DelayedMoveNode(Func<Friend, ConversationNode> func)
        {
            _func = func;
        }

        public override async Task Enter(Overseer overseer, Friend friend)
        {
            await friend.Memory.MoveConversationTo(_func(friend), overseer);
        }
    }
}
