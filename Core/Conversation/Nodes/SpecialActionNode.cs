using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Conversation.Nodes
{
    class SpecialActionNode : ConversationNode
    {
        private string[] arguments;
        private string specialName;

        public SpecialActionNode(string specialName, string[] arguments)
        {
            this.specialName = specialName;
            this.arguments = arguments;
        }

        public override async Task Enter(Overseer overseer, Friend friend)
        {
            List<string> parsedArguments = arguments
                .Select(arg => friend.MacroReplacer.ReplaceMacrosInOutgoingText(arg)).ToList();

            switch (specialName)
            {
                case "SetTimeOffsetFromTime":
                    int v;
                    if (parsedArguments[0].Length >= 2)
                    {
                        if (int.TryParse(parsedArguments[0].Substring(0, 2), out v))
                        {
                            int ourHours = DateTime.Now.Hour;
                            int theirHours = v;
                            int diff = theirHours - ourHours;
                            friend.Memory.Persistent.CaretakersClockHasPlusThisManyHours = diff;
                            friend.SavePersistentMemory();
                            await overseer.Speaking.SendSystemMessage(friend,
                                "Your time difference is " + diff + " hours from the Open Friend Project's servers. {name} will remember this.");
                        }
                    }
                    break;
                case "SetCountry":
                    friend.Memory.Persistent.Country = parsedArguments[0];
                    friend.SavePersistentMemory();
                    await overseer.Speaking.SendSystemMessage(friend,
                        friend.Memory.Persistent.CommonName + " now lives in " + friend.Memory.Persistent.Country + ".");
                    break;
                default:
                    await overseer.Speaking.SendSystemMessage(friend, "Unknown function '" + specialName + "' called.");
                    break;
            }
            await friend.Memory.MoveConversationTo(this.FollowingNode, overseer);
        }
    }
}
