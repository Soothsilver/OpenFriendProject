using System.Threading.Tasks;
using Core.ProcessorCodes;

namespace Core.ProcessorCodes
{
    internal class MergeGuidProcessorCode : TemporaryProcessorCode
    {
        public MergeGuidProcessorCode(Overseer overseer) : base(overseer)
        {
        }

        public override async Task<TemporaryProcessorCodeResult> ProcessMessageByTemporaryProcessor(Friend friend, string message)
        {
            foreach(var fr in Overseer.Persons.GetAllFriends())
            {
                string guid = fr.Memory.Persistent.InternalId;
                if (guid.Trim().ToLower() == message.Trim().ToLower())
                {
                    // Merge
                    if (friend.Memory.Persistent.FacebookId != null)
                    {
                        fr.Memory.Persistent.FacebookId = friend.Memory.Persistent.FacebookId;
                        friend.Memory.Persistent.FacebookId = null;
                        friend.SavePersistentMemory();
                        fr.SavePersistentMemory();
                        await Overseer.Speaking.SendSystemMessage(friend, "Facebook account correspondence changed.");

                    }
                    // Merge telegram
                    if (friend.Memory.Persistent.TelegramName != null)
                    {
                        fr.Memory.Persistent.TelegramName = friend.Memory.Persistent.TelegramName;
                        friend.Memory.Persistent.TelegramName = null;
                        friend.SavePersistentMemory();
                        fr.SavePersistentMemory();
                        await Overseer.Speaking.SendSystemMessage(friend, "Telegram account correspondence changed.");
                    }
                    await Overseer.Speaking.SendSystemMessage(friend, "Merge successful. You are now talking to '" + fr.Memory.Persistent.CommonName + "'.");

                    return new TemporaryProcessorCodeResult(true, true);
                }
            }
            await Overseer.Speaking.SendSystemMessage(friend, "Merge failed. No such GUID in database.");
            return new TemporaryProcessorCodeResult(true, true);
        }
    }
}