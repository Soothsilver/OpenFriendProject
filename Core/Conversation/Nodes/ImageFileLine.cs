using System.Threading.Tasks;

namespace Core.Conversations
{
    internal class ImageLine : ConversationNode
    {
        private readonly string _url;

        public ImageLine(string url, ConversationNode next)
        {
            _url = url;
            FollowingNode = next;
        }

        public override async Task Enter(Overseer overseer, Friend friend)
        {
            await overseer.Speaking.SendImage(friend, _url);
            await friend.Memory.MoveConversationTo(FollowingNode, overseer);
        }
    }
}