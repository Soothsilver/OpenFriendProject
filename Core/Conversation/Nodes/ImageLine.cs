using System.Threading.Tasks;

namespace Core.Conversations
{
    internal class ImageFileLine : ConversationNode
    {
        private readonly string _filepath;

        public ImageFileLine(string path)
        {
            this._filepath = path;
        }

        public override async Task Enter(Overseer overseer, Friend friend)
        {
            await overseer.Speaking.SendImageFile(friend, _filepath);
            await friend.Memory.MoveConversationTo(FollowingNode, overseer);
        }
    }
    internal class DocumentFileLine : ConversationNode
    {
        private readonly string _filepath;

        public DocumentFileLine(string path)
        {
            this._filepath = path;
        }

        public override async Task Enter(Overseer overseer, Friend friend)
        {
            await overseer.Speaking.SendFile(friend, _filepath);
            await friend.Memory.MoveConversationTo(FollowingNode, overseer);
        }
    }
}