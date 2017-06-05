using System.Threading.Tasks;

namespace Core.Conversations
{
    internal class ImageLine : ConversationNode
    {
        private readonly string _url;
        private readonly ConversationNode _next;

        public ImageLine(string url, ConversationNode next)
        {
            _url = url;
            _next = next;
        }

        public override async Task Enter(Overseer overseer, Friend friend)
        {
            await overseer.Speaking.SendImage(friend, _url);
            await friend.Memory.MoveConversationTo(_next, overseer);
        }
    }
}