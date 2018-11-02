using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class CardSequenceCount
    {
        [JsonProperty("N")]
        public string Value { get; set; }
    }
}