using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMLbot;

namespace Core.Aiml
{
    public class AimlCore
    {
        private readonly Overseer _overseer;

        public AimlCore(Overseer overseer)
        {
            _overseer = overseer;
        }
        

        public void Initialize()
        {
           
            Console.WriteLine("Loading.");
            Console.WriteLine("Loaded.");
        }

        public Alice.Alice CreateAliceFor(Friend friend)
        {
            Bot b = new Bot();
            User user = new User("Self.David", b);
            b.loadSettings();
            b.isAcceptingUserInput = false;
            b.loadAIMLFromFiles();
            b.isAcceptingUserInput = true;
            _overseer.Speaking.Debug("Alice initialized.");
            return new Alice.Alice(b, user, this);
        }
    }
}
