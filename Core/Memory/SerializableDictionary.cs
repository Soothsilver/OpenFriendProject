using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Core
{
    [XmlRoot("SerializableDictionary")]
    public class SerializableDictionary : Dictionary<String, String>, IXmlSerializable
    {
        internal Boolean _ReadOnly = false;

        public Boolean ReadOnly
        {
            get { return this._ReadOnly; }

            set
            {
                this.CheckReadOnly();
                this._ReadOnly = value;
            }
        }

        public new string this[String key]
        {
            get
            {
                string value;
                

                return this.TryGetValue(key, out value) ? value : null;
            }

            set
            {
                this.CheckReadOnly();

                if (value != null)
                {
                    base[key] = value;
                }
                else
                {
                    this.Remove(key);
                }
            }
        }

        internal void CheckReadOnly()
        {
            if (this._ReadOnly)
            {
                throw new Exception("Collection is read only");
            }
        }

        public new void Clear()
        {
            this.CheckReadOnly();

            base.Clear();
        }

        public new void Add(String key, string value)
        {
            this.CheckReadOnly();
            
            base.Add(key, value);
        }

        public new void Remove(String key)
        {
            this.CheckReadOnly();

            base.Remove(key);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            Boolean wasEmpty = reader.IsEmptyElement;

            reader.Read();

            if (wasEmpty)
            {
                return;
            }

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                if (reader.Name == "Item")
                {
                    String key = reader.GetAttribute("Key");
                    Type type = Type.GetType(reader.GetAttribute("TypeName"));

                    reader.Read();
                    if (type != null)
                    {
                        this.Add(key, (string)new XmlSerializer(type).Deserialize(reader));
                    }
                    else
                    {
                        reader.Skip();
                    }
                    reader.ReadEndElement();

                    reader.MoveToContent();
                }
                else
                {
                    reader.ReadToFollowing("Item");
                }
            }
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (KeyValuePair<String, String> item in this)
            {
                writer.WriteStartElement("Item");
                writer.WriteAttributeString("Key", item.Key);
                writer.WriteAttributeString("TypeName", item.Value.GetType().AssemblyQualifiedName);

                new XmlSerializer(item.Value.GetType()).Serialize(writer, item.Value);

                writer.WriteEndElement();
            }
        }
    }
}
