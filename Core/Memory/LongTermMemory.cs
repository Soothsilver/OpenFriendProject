using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Core
{
    [Serializable]
    public class LongTermMemory
    {
        public string InternalId;
        public string CommonName = "Daina";
        public string CaretakerName;
        public string FacebookId;

        internal string GetVariable(string key)
        {
            if (Variables.ContainsKey(key))
            {
                return Variables[key];
            }
            else
            {
                return null;
            }
        }

        public SerializableDictionary Variables;
        public int TelegramId;
        public string Country = null;
        public int CaretakersClockHasPlusThisManyHours;
        public bool HasRealisticTypingSpeed;

        public LongTermMemory()
        {
            this.InternalId = Guid.NewGuid().ToString();
            this.CaretakerName = "Caretaker #" + R.Next(10).ToString() + R.Next(10).ToString() + R.Next(10).ToString();
            this.HasRealisticTypingSpeed = true;
            this.Variables = new SerializableDictionary();
        }

        public void SetVariable(string variablename, string value)
        {
            Variables[variablename] = value;
        }
    }
    public static class SerializationExtensions
    {
        public static string Serialize<T>(this T obj)
        {
            var serializer = new DataContractSerializer(obj.GetType());
            using (var writer = new StringWriter())
            using (var stm = new XmlTextWriter(writer))
            {
                serializer.WriteObject(stm, obj);
                return writer.ToString();
            }
        }
        public static T Deserialize<T>(this string serialized)
        {
            var serializer = new DataContractSerializer(typeof(T));
            using (var reader = new StringReader(serialized))
            using (var stm = new XmlTextReader(reader))
            {
                return (T)serializer.ReadObject(stm);
            }
        }
    }
}