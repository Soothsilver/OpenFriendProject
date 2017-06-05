using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class GoodbyeProcessorCode : ProcessorCode
    {
        public GoodbyeProcessorCode(Overseer overseer) : base(overseer)
        {

        }

        public override async Task<bool> ProcessMessage(Friend friend, string message)
        {
            if (message.ToLower().Contains("bye") || message.ToLower().Contains("see you"))
            {
                await Overseer.Speaking.SendMessage(friend,
                    "Take care, friend! Come say 'hi' anytime. I'll be looking forward to our next chat!");
                await friend.Memory.MoveConversationTo(null, this.Overseer);
                return true;
            }
            return false;
        }
    }
}
