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
        public string TelegramName;

        public LongTermMemory()
        {
            this.InternalId = Guid.NewGuid().ToString();
        }
    }
}