using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public abstract class ConversationNode
    {
        public ConversationNode FollowingNode;

        public abstract Task Enter(Overseer overseer, Friend friend);

        public virtual Task<bool> ProcessMessage(Overseer overseer, Friend friend, string message)
        {
            return Task.FromResult(false);
        }
    }
}
