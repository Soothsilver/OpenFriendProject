using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Endpoints;
using Core.ProcessorCodes;

namespace Core
{
    public class Friend
    {
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

        public bool HasRealisticTypingSpeed => true;

        public FriendMouth Speaking;

        public Memory Memory;

        private Overseer overseer;

        public List<TemporaryProcessorCode> TemporaryProcessors = new List<TemporaryProcessorCode>();

        private ThreadQueue queue = new ThreadQueue();

        public Friend(Overseer overseer)
        {
            Memory = new Memory(this);
            Speaking = new FriendMouth(this);
            this.overseer = overseer;
        }

        public Friend(LongTermMemory memory, Overseer overseer)
            : this(overseer)
        {
            this.Memory.Persistent = memory;
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
            queue.EnqueueAction(() =>
            {
                overseer.MessageProcessor.ProcessIncomingMessage(this, message).Wait();
            });
        }

        public void SavePersistentMemory()
        {
            MemoryStorage.SaveToFile(this.Memory.Persistent);
        }

        public override string ToString()
        {
            return this.Memory.Persistent.CommonName + " (ID " + this.Memory.Persistent.InternalId + ", caretaker " +
                   this.Memory.Persistent.CaretakerName + ")";
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