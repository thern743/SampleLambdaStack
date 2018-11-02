using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class CardSubType
    {
        [JsonProperty("S")]
        public string Value { get; set; }
    }
}