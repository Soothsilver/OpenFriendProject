using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Conversation
{
    public class LoadedConversations
    {
        private readonly Overseer _overseer;
        public Dictionary<string, ConversationNode> All = new Dictionary<string, ConversationNode>();

        public LoadedConversations(Overseer overseer)
        {
            _overseer = overseer;
            All.Add("set-locale", _overseer.DialogueLoader.LoadFromFile("Conversations\\set-locale.txt"));
        }
        
    }
}
