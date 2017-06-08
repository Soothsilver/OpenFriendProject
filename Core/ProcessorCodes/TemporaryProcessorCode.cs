using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ProcessorCodes
{
    public abstract class TemporaryProcessorCode : ProcessorCode
    {
        protected TemporaryProcessorCode(Overseer overseer) : base(overseer)
        {
        }

        public abstract Task<TemporaryProcessorCodeResult> ProcessMessageByTemporaryProcessor(Friend friend,
            string message);

        public sealed override async Task<bool> ProcessMessage(Friend friend, string message)
        {
            var t = await ProcessMessageByTemporaryProcessor(friend, message);
            if (t.Eliminate)
            {
                friend.RemoveTemporaryProcessor(this);
            }
            return t.Processed;
        }
    }

    public class TemporaryProcessorCodeResult
    {
        public bool Processed;
        public bool Eliminate;

        public TemporaryProcessorCodeResult(bool processed, bool eliminate)
        {
            Processed = processed;
            Eliminate = eliminate;
        }
    }
}
