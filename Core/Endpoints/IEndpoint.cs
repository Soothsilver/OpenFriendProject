using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Conversation;

namespace Core.Endpoints
{
    public interface IEndpoint
    {
        Task SendImageUrl(Friend friend, string url);
        Task SendMessage(Friend friend, string message, QuickReply[] quickReplies);
        Task SenderAction(Friend friend, NonMessageAction action);
    }
}
