using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Conversations;

namespace Core.ProcessorCodes
{
    class CommandsProcessorCode : ProcessorCode
    {
        public CommandsProcessorCode(Overseer overseer) : base(overseer)
        {
        }

        public override async Task<bool> ProcessMessage(Friend friend, string message)
        {
            if (message.StartsWith("/rename "))
            {
                friend.Memory.Persistent.CommonName = message.Substring("/rename ".Length);
                friend.SavePersistentMemory();
                await Overseer.Speaking.SendMessage(friend, "My name has changed!");
            }
            if (message == "/name")
            {
                await Overseer.Speaking.SendMessage(friend,
                    "My name is '" + friend.Memory.Persistent.CommonName + "'.");
            }
            if (message.ToLower() == "exit" || message.ToLower() == "return" || message.ToLower() == "quit")
            {
                if (friend.Memory.TalkingToAlice)
                {
                    friend.Memory.TalkingToAlice = false;
                    friend.Memory.Alice.Dispose();
                }
                await Overseer.Speaking.SendMessage(friend, "Phew.");
                await Overseer.Speaking.SendMessage(friend, "Well, that was fun ^^.");
                return true;
            }
            if (message.ToLower().Contains("restart conversation"))
            {
                await friend.Memory.MoveConversationTo(BookConversation.GetBeginning(), Overseer);
                return true;
            }
            if (message.ToLower().Contains("talk to alice"))
            {
                friend.Memory.CurrentConversation = null;
                if (!friend.Memory.TalkingToAlice)
                {
                    friend.Memory.TalkingToAlice = true;
                    await Overseer.Speaking.SendMessage(friend, "Alright! I'm initializing the Alice subsystem.");
                    await Overseer.Speaking.SendMessage(friend, "Alice... I sometimes don't make too much sense when using that subsystem.");
                    await Overseer.Speaking.SendMessage(friend, "It's a little embarrassing so please don't judge me.");
                    await Overseer.Speaking.SendMessage(friend, "And type 'exit' or 'quit' any time you want to end the subsystem!");
                    friend.Memory.Alice = Overseer.Aiml.CreateAliceFor(friend);
                    await Overseer.Speaking.SendMessage(friend, "Done. Say hello to Alice!");
                    return true;
                }
            }
            return false;
        }
    }
}
