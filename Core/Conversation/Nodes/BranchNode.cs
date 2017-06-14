using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Conversation
{
    class Branch : ConversationNode
    {
        private readonly string _line;
        private QuickReply[] _options;
        private PossibleReply[] _possibleReplies;
        public Branch(string line, params PossibleReply[] options)
        {
            _line = line;
            _options = options.Select(str => new QuickReply(str.Title)).ToArray();
            _possibleReplies = options;
        }

        public override async Task Enter(Overseer overseer, Friend friend)
        {
            await overseer.Speaking.SendMessage(friend, _line, _options);
        }

        public override async Task<bool> ProcessMessage(Overseer overseer, Friend friend, string message)
        {
            string msg = message.ToLower();
            foreach(var reply in _possibleReplies)
            {
                if (friend.MacroReplacer.ReplaceMacrosInOutgoingText(reply.Title).ToLower() == msg)
                {
                    if (this.FollowingNode != null)
                    {
                        friend.Memory.ConversationStack.Push(this.FollowingNode);
                    }
                    await friend.Memory.MoveConversationTo(reply.WhatNext, overseer);
                    return true;
                }
            }
            return false;
        }
    }

    public class PossibleReply
    {
        public string Title { get; }
        public ConversationNode WhatNext { get; }

        public PossibleReply(string title, ConversationNode whatNext)
        {
            Title = title;
            WhatNext = whatNext;
        }
    }
    public class QuickReply
    {
        public string Content_type { get; set; } = "text";
        public string Title { get; set; }
        public string Payload { get; set; }

        public QuickReply(string title)
        {
            this.Title = title;
            this.Payload = title;
        }
    }
}
