using System;
using System.Threading.Tasks;

namespace Core
{
    public class Friend
    {
        public string UserId { get; }
        public bool IsHome => UserId == "home";
        public bool IsFacebook => UserId != "home";

        public bool HasRealisticTypingSpeed => true;

        private Overseer overseer;
        private ThreadQueue queue = new ThreadQueue();

        public Memory Memory;

        public Friend(string userId, Overseer overseer)
        {
            UserId = userId;
            Memory = new Memory(this);
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
    }
}