

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lambda.Common.Interfaces
{
    public interface IEventMessage
    {
        [JsonProperty("messageId", Required = Required.Always)]
        string MessageId { get; set; }

        [JsonProperty("receiptHandle", Required = Required.Always)]
        string ReceiptHandle { get; set; }

        [JsonProperty("body", Required = Required.Always)]
        string Body { get; set; }

        [JsonProperty("attributes")]
        IDictionary<string, string> Attributes { get; set; }
    }
}
