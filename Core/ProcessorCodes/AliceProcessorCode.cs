using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMLbot;

namespace Core.ProcessorCodes
{
    class AliceProcessorCode : ProcessorCode
    {
        public AliceProcessorCode(Overseer overseer) : base(overseer)
        { 
        }

        public override async Task<bool> ProcessMessage(Friend friend, string message)
        {
            if (friend.Memory.TalkingToAlice)
            {
                var b = friend.Memory.Alice.Bot;
                Request r = new Request(message, friend.Memory.Alice.User, b);
                Result res = b.Chat(r);
                await Overseer.Speaking.SendMessage(friend, res.Output);
                return true;
            }
            return false;
        }
    }
}
