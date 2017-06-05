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
        private ConversationNode _then;
        private readonly Func<ConversationNode> _thenFunc;

        public Line (string line)
        {
            _line = line;
        }
        public Line(string line, ConversationNode then)
        {
            _line = line;
            _then = then;
        }
        public Line(string line, Func<ConversationNode> then)
        {
            _line = line;
            _thenFunc = then;
        }
        public override async Task Enter(Overseer overseer, Friend friend)
        {
            await overseer.Speaking.SendMessage(friend, _line);
            if (_thenFunc != null)
            {
                _then = _thenFunc();
            }
            await friend.Memory.MoveConversationTo(_then, overseer);
        }
    }
}
