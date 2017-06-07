using System;

namespace Core
{
    [Serializable]
    public class LongTermMemory
    {
        public string InternalId;
        public string CommonName = "Daina";
        public string CaretakerName = "Petr";
        public string FacebookId;

        public LongTermMemory()
        {
            this.InternalId = Guid.NewGuid().ToString();
        }
    }
}