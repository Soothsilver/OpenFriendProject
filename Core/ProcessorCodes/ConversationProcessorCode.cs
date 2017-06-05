using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ProcessorCodes
{
    class ConversationProcessorCode : ProcessorCode
    {
        public ConversationProcessorCode(Overseer overseer) : base(overseer)
        {
        }

        public override async Task<bool> ProcessMessage(Friend friend, string message)
        {
            if (friend.Memory.CurrentConversation != null)
            {
                if (await friend.Memory.CurrentConversation.ProcessMessage(Overseer, friend, message))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
