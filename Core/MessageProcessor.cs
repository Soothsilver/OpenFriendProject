using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ProcessorCodes;

namespace Core
{
    public class MessageProcessor
    {
        private Overseer overseer;
        private List<ProcessorCode> Processors = new List<ProcessorCode>();

        public MessageProcessor(Overseer overseer)
        {
            this.overseer = overseer;
            this.Processors.Add(new CommandsProcessorCode(overseer));
            this.Processors.Add(new AliceProcessorCode(overseer));
            this.Processors.Add(new ConversationProcessorCode(overseer));
            this.Processors.Add(new AsunaProcessorCode(overseer));
            this.Processors.Add(new HelloProcessorCode(overseer));
            this.Processors.Add(new GoodbyeProcessorCode(overseer));
            this.Processors.Add(new DefaultProcessorCode(overseer));
        }

        public async Task ProcessIncomingMessage(Friend friend, string text)
        {
            for(int i = friend.TemporaryProcessors.Count -1; i>=0;i--)
            {
                var code = friend.TemporaryProcessors[i];
                if (await code.ProcessMessage(friend, text))
                {
                    return;
                }
            }
            foreach (var code in this.Processors)
            {
                if (await code.ProcessMessage(friend, text))
                {
                    return;
                }
            }
        }
    }
}
