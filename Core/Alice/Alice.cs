using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMLbot;
using Core.Aiml;

namespace Core.Alice
{
    public class Alice
    {
        public Bot Bot { get; }
        public User User { get; }
        private AimlCore aimlCore;

        public Alice(Bot bot, User user, AimlCore aimlCore)
        {
            Bot = bot;
            User = user;
            this.aimlCore = aimlCore;
        }

        public void Dispose()
        {
            // Disposal unnecessary.   
        }

        public string Handle(string hello)
        {
            var b = Bot;
            Request r = new Request(hello, User, b);
            Result res = b.Chat(r);
            return res.Output;
        }
    }
}
