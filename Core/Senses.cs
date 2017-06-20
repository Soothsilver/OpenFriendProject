using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Core
{
    public class Senses
    {
        private Overseer overseer;

        public Senses(Overseer overseer)
        {
            this.overseer = overseer;
        }     

        public void IncomingTextMessage(Friend friend, string text)
        {
            overseer.Speaking.Debug("Incoming message: " + text.Replace("\\n", Environment.NewLine));
            friend.StartProcessingInput(text.Replace("\\n", Environment.NewLine));
        }
    }
}
