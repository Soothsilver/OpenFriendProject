using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Conversation
{
    public class MacroReplacer
    {
        private readonly Friend _owner;

        public string ReplaceMacrosInOutgoingText(string text)
        {
            return
                text
                    .Replace("{name}", _owner.Memory.Persistent.CommonName)
                    .Replace("{you}", _owner.Memory.Persistent.CaretakerName)
                ;

        }

        public MacroReplacer(Friend owner)
        {
            _owner = owner;
        }
    }
}
