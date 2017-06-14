using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Conversation.Nodes
{
    class DoNothingNode : ConversationNode
    {
        public override Task Enter(Overseer overseer, Friend friend)
        {
            return friend.Memory.MoveConversationTo(this.FollowingNode, overseer);
        }
    }
}
