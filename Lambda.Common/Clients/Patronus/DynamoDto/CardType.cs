using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class CardType
    {
        [JsonProperty("S")]
        public string Value { get; set; }
    }
}