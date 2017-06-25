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
            text =
                text
                    .Replace("{name}", _owner.Memory.Persistent.CommonName)
                    .Replace("{you}", _owner.Memory.Persistent.CaretakerName)
                    .Replace("{country}", _owner.Memory.Persistent.Country)
                ;
            foreach(var variable in _owner.Memory.Persistent.Variables)
            {
                text = text.Replace("{$" + variable.Key + "}", variable.Value);
            }
            return text;

        }

        public MacroReplacer(Friend owner)
        {
            _owner = owner;
        }
    }
}
