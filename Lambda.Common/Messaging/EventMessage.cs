using System.Collections.Generic;
using Lambda.Common.Interfaces;
using Newtonsoft.Json;

namespace Lambda.Common.Messaging
{
    [JsonObject]
    public class EventMessage : IEventMessage
    {        
        public string MessageId { get; set; }        
        public string ReceiptHandle { get; set; }        
        public string Body { get; set; }        
        public IDictionary<string, string> Attributes { get; set; }
    }
}