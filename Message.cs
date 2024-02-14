using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chat2
{
    internal class Message
    {
        public string sender;
        public string addressee;
        public string text;

        public Message(string sender, string addressee, string text)
        {
            this.sender = sender;
            this.addressee = addressee;
            this.text = text;
        }

        public Message() { }

        public string ToJSon()
        {
            return JsonSerializer.Serialize(this);
        }

        public Message FromJSon(string message)
        {
            return JsonSerializer.Deserialize<Message>(message);
        }
    }
}
