using System;
using System.Threading.Tasks;

namespace Core
{
    [Serializable]
    public class Friend
    {
        public bool IsFacebook => Memory.Persistent.FacebookId != null;

        public bool HasRealisticTypingSpeed => true;

        public Memory Memory;

        private Overseer overseer;

        private ThreadQueue queue = new ThreadQueue();

        public Friend(Overseer overseer)
        {
            Memory = new Memory(this);
            this.overseer = overseer;
        }

        public Friend(LongTermMemory memory, Overseer overseer)
        {
            Memory = new Memory(this);
            this.Memory.Persistent = memory;
            this.overseer = overseer;
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
    }
}