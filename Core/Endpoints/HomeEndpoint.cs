using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Endpoints
{
    public class HomeEndpoint
    {
        private Overseer overseer;

        public HomeEndpoint(Overseer overseer)
        {
            this.overseer = overseer;
        }

        public async Task SendMessage(Friend friend, string text)
        {
            await overseer.Senses.IncomingTextMessage(friend, text);
        }
    }
}
