using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public abstract class ProcessorCode
    {
        protected Overseer Overseer { get; }

        protected ProcessorCode(Overseer overseer)
        {
            this.Overseer = overseer;
        }
        public abstract Task<bool> ProcessMessage(Friend friend, string message);
    }
}
