using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ProcessorCodes.Commands
{
    class Command
    {
        public string Keyword;
        public string Description;
        public Func<Friend, string, Overseer, Task> Action;

        public Command(string keyword, string description, Func<Friend, string, Overseer, Task> action)
        {
            Keyword = keyword;
            Description = description;
            Action = action;
        }
    }
}
