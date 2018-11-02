using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class CardExpirationDate
    {
        [JsonProperty("S")]
        public string Value { get; set; }
    }
}