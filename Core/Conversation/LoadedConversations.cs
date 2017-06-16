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
            foreach(var filename in System.IO.Directory.EnumerateFiles("Conversations", "*.txt"))
            {
                All.Add(System.IO.Path.GetFileNameWithoutExtension(filename),
                    _overseer.DialogueLoader.LoadFromFile(filename));
            }
            //All.Add("set-locale", _overseer.DialogueLoader.LoadFromFile("Conversations/set-locale.txt"));
            //All.Add("destroy", _overseer.DialogueLoader.LoadFromFile("Conversations/destroy.txt"));
        }
        
    }
}
