using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Core
{
    public static class MemoryStorage
    {
        public static string FolderName = "friends";
        private static void EnsureFolderExists()
        {
            if (!System.IO.Directory.Exists(FolderName))
            {
                System.IO.Directory.CreateDirectory(FolderName);
            }
        }
        public static IEnumerable<LongTermMemory> LoadFriendData()
        {
            EnsureFolderExists();
            XmlSerializer serializer = new XmlSerializer(typeof(LongTermMemory));
            foreach (var filename in System.IO.Directory.EnumerateFiles(FolderName, "*.xml"))
            {

                StreamReader sr = new StreamReader(filename);
                var memory = (LongTermMemory) serializer.Deserialize(sr);
                sr.Close();
                yield return memory;
            }
        }
        public static void SaveToFile(LongTermMemory memoryPersistent)
        {
            EnsureFolderExists();
            XmlSerializer serializer = new XmlSerializer(typeof(LongTermMemory));
            StreamWriter sw = new StreamWriter(Path.Combine(FolderName, memoryPersistent.InternalId + ".xml"));
            serializer.Serialize(sw, memoryPersistent);
            sw.Close();
        }
    }
}