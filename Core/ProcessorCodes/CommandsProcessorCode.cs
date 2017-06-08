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
            if (message.StartsWith("/"))
            {
                int space = message.IndexOf(' ');
                string keyword = message;
                if (space != -1)
                {
                    keyword = message.Substring(0, space);
                }
                foreach (var command in Commands.AllCommands.All)
                {
                    if ("/" + command.Keyword == keyword.ToLower())
                    {
                        await command.Action(friend, message, Overseer);
                        return true;
                    }
                }
                await Overseer.Speaking.SendSystemMessage(friend,
                    "Unrecognized command. Type '/help' for a list of commands.");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
