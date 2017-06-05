using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Core
{
    class CallbackEnvelope
    {
        public string Object { get; set; }
        public Entry[] Entry { get; set; }
    }
    class Entry
    {
        public string Id { get; set; }
        public ulong Time { get; set; }
        public Callback[] Messaging { get; set; }
    }
    class Callback
    {
        public IdContainer Sender { get; set; }
        public IdContainer Recipient { get; set; }
        public ulong Timestamp { get; set; }
        public JsonMessage Message { get; set; }
        public JsonDelivery Delivery { get; set; }
        public JsonMessageRead Read { get; set; }

        public JsonPostback Postback { get; set; }
    }
    class JsonDelivery
    {
        public string[] Mids { get; set; }
        public ulong Watermark { get; set; }
        public int Seq { get; set; }
    }
    class JsonMessageRead
    {
        public ulong Watermark { get; set; }
        public int Seq { get; set; }
    }
    class JsonMessage
    {
        public string Mid { get; set; }
        public string Text { get; set; }
        public PayloadContainer Quick_reply { get; set; }
    }
    class PayloadContainer
    {
        public string Payload { get; set; }
    }
    class IdContainer
    {
        public string Id { get; set; }
    }
    class JsonPostback
    {
        public string Payload { get; set; }
    }
}
