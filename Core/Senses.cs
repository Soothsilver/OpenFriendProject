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

  
        private void StartProcessingMessage(Friend friend, string message)
        {
            overseer.Speaking.Debug("Incoming message: " + message);
            friend.StartProcessingInput(message);
        }



    


        public void IncomingTextMessage(Friend friend, string text)
        {
            StartProcessingMessage(friend, text.Replace("\\n", Environment.NewLine));
        }
    }
}
