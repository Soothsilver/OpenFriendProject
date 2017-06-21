using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Conversation
{
    class Line : ConversationNode
    {
        private readonly string _line;
        private readonly Func<ConversationNode> _thenFunc;

        public Line (string line) : this(line, (ConversationNode)null)
        {
        }
        public Line(string line, ConversationNode then)
        {
            _line = line.Replace("\\n", Environment.NewLine); 
            FollowingNode = then;
        }
        public Line(string line, Func<ConversationNode> then)
        {
            _line = line.Replace("\\n", Environment.NewLine); 
            _thenFunc = then;
        }
        public override async Task Enter(Overseer overseer, Friend friend)
        {
            if (_line.StartsWith("SYS "))
            {
                await overseer.Speaking.SendSystemMessage(friend, _line.Substring("SYS ".Length));
            }
            else
            {
                await overseer.Speaking.SendMessage(friend, _line);
            }
            if (_thenFunc != null)
            {
                FollowingNode = _thenFunc();
            }
            await friend.Memory.MoveConversationTo(FollowingNode, overseer);
        }
    }
}
