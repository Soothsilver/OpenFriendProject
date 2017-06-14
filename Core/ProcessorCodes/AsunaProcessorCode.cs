using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fastenshtein;

namespace Core.ProcessorCodes
{
    class AsunaProcessorCode : ProcessorCode
    {
        public AsunaProcessorCode(Overseer overseer) : base(overseer)
        {
        }

        public override async Task<bool> ProcessMessage(Friend friend, string message)
        {
            string msg = message.ForMachineComparison();
            Levenshtein l = new Levenshtein(msg);
            Freeform.FreeformPhrase closestPhrase = null;
            int closestDistance = int.MaxValue;
            foreach (var phrase in Overseer.FreeformPhrases.All)
            {
                string phr = phrase.Phrase.ForMachineComparison();
                int distance = l.Distance(phr);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPhrase = phrase;
                }
            }
            if (closestDistance <= 2)
            {
                await closestPhrase.Action(friend, Overseer);
                return true;
            }
            else if (closestDistance <= 4)
            {
                await Overseer.Speaking.SendMessage(friend,
                    "Did you mean '" + closestPhrase.Phrase + "'?",
                    new[] {  new Conversation.QuickReply(closestPhrase.Phrase) });
                return true;
            }
            return false;
        }
    }
}
