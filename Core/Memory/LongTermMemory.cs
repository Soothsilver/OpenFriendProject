using System;

namespace Core
{
    [Serializable]
    public class LongTermMemory
    {
        public string InternalId;
        public string CommonName = "Daina";
        public string CaretakerName = "friend";
        public string FacebookId;
        public int TelegramId;

        public LongTermMemory()
        {
            this.InternalId = Guid.NewGuid().ToString();
        }
    }
}