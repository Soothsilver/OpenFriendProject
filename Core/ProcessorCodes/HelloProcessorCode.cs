using Core.Conversations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class HelloProcessorCode : ProcessorCode
    {
        public HelloProcessorCode(Overseer overseer) : base(overseer)
        {
        }

        public override async Task<bool> ProcessMessage(Friend friend, string message)
        {
            if (message.ToLower().Contains("hello") || message.ToLower().Contains("hi") || message.ToLower().Contains("greeting"))
            {
                await Overseer.Speaking.SendMessage(friend, "Hello to you, too, {you}!");
                if (friend.Memory.CurrentConversation != null)
                {
                    await Overseer.Speaking.SendMessage(friend, "Let's return to our previous conversation, okay?");
                    await friend.Memory.CurrentConversation.Enter(Overseer, friend);
                }
                else
                {
                    await friend.Memory.MoveConversationTo(BookConversation.GetBeginning(), Overseer);
                }
                return true;
            }
            return false;
        }
    }
}
