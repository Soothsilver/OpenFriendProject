using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class DefaultProcessorCode : ProcessorCode
    {
        public override async Task<bool> ProcessMessage(Friend friend, string message)
        {
            await Overseer.Speaking.SendMessage(friend,
                "Um... sorry, I'm not very good at recognizing natural speech yet.");
            await Overseer.Speaking.SendMessage(friend,
                "My Alice subsystem is good at it, though. Should I activate it?", new[] {
                    new Conversation.QuickReply("Talk to Alice."),
                    new Conversation.QuickReply("Restart conversation.")
                });
            return true;
        }

        public DefaultProcessorCode(Overseer overseer) : base(overseer)
        {
        }
    }
}
