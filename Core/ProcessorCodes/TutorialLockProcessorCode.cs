using System;
using System.Threading.Tasks;

namespace Core
{
    internal class TutorialLockProcessorCode : ProcessorCode
    {
        public TutorialLockProcessorCode(Overseer overseer) : base(overseer)
        {
        }

        public override async Task<bool> ProcessMessage(Friend friend, string message)
        {
            if (friend.Data.GetVariable("inited") == null)
            {
                if (friend.Memory.CurrentConversation == null)
                {
                    await friend.Memory.MoveConversationTo(Overseer.LoadedConversations.All["story-1"], Overseer);
                }
                else
                {
                    await Overseer.Speaking.SendSystemMessage(friend, "Your virtual friend is not yet ready. Please first complete the introduction process.");
                    await friend.Memory.CurrentConversation.Enter(Overseer, friend);
                }
                return true;
            }
            return false;
        }
    }
}