using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Conversation
{
    class ActionNode : ConversationNode
    {
        private readonly Action<Overseer,Friend> _action;

        public ActionNode(Action<Overseer, Friend> action)
        {
            _action = action;
        }

        public override Task Enter(Overseer overseer, Friend friend)
        {
            _action(overseer, friend);
            return Task.FromResult(0);
        }
    }
}
