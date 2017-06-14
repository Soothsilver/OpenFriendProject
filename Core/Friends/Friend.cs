using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Conversation;
using Core.Endpoints;
using Core.ProcessorCodes;

namespace Core
{
    public class Friend
    {
        public MacroReplacer MacroReplacer;

        public Memory Memory;

        private Overseer overseer;

        private ThreadQueue queue = new ThreadQueue();

        public FriendMouth Speaking;

        public List<TemporaryProcessorCode> TemporaryProcessors = new List<TemporaryProcessorCode>();

        public Friend(Overseer overseer)
        {
            Memory = new Memory(this);
            Speaking = new FriendMouth(this);
            MacroReplacer = new MacroReplacer(this);
            this.overseer = overseer;
        }

        public Friend(LongTermMemory memory, Overseer overseer)
            : this(overseer)
        {
            Memory.Persistent = memory;
        }

        public bool HasRealisticTypingSpeed => true;

        public bool IsFacebook => Memory.Persistent.FacebookId != null;
        public bool IsTelegram => Memory.Persistent.TelegramId != 0;

        public IEnumerable<IEndpoint> Endpoints
        {
            get
            {
                if (IsFacebook) yield return overseer.Facebook;
                if (IsTelegram) yield return overseer.Telegram;
                yield return overseer.Home;
            }
        }

        public void ScheduleWithDelay(TimeSpan delay, Action action)
        {
            Task.Run(async () =>
            {
                await Task.Delay(delay);
                queue.EnqueueAction(action);
            });
        }

        public void StartProcessingInput(string message)
        {
            queue.EnqueueAction(() => { overseer.MessageProcessor.ProcessIncomingMessage(this, message).Wait(); });
        }

        public void SavePersistentMemory()
        {
            MemoryStorage.SaveToFile(Memory.Persistent);
        }

        public override string ToString()
        {
            return Memory.Persistent.CommonName + " (ID " + Memory.Persistent.InternalId + ", caretaker " +
                   Memory.Persistent.CaretakerName + ")";
        }

        public void RemoveTemporaryProcessor(TemporaryProcessorCode temporaryProcessorCode)
        {
            TemporaryProcessors.Remove(temporaryProcessorCode);
        }

        public void AddTemporaryProcessor(TemporaryProcessorCode temporaryProcessorCode)
        {
            TemporaryProcessors.Add(temporaryProcessorCode);
        }
    }
}