using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Conversation.Nodes
{
    /// <summary>
    /// This conversation tree node immediately performs the given C# code and ends when the code ends.
    /// </summary>
    class LambdaActionNode : ConversationNode
    {
        private readonly Func<Friend, Task> whatDo;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="whatDo">The action to perform when this conversation node is reached.</param>
        public LambdaActionNode(Func<Friend, Task> whatDo)
        {
            this.whatDo = whatDo;
        }

        public override async Task Enter(Overseer overseer, Friend friend)
        {
            await this.whatDo(friend);
            await friend.Memory.MoveConversationTo(this.FollowingNode, overseer);
        }
    }
}
