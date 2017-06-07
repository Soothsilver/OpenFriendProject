using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Endpoints
{
    public class FacebookEndpoint
    {
        private Overseer overseer;

        public FacebookEndpoint(Overseer overseer)
        {
            this.overseer = overseer;
        }
    }
}
