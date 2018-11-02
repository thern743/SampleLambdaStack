using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class ChdUserTx
    {
        [JsonProperty("S")]
        public string Value { get; set; }
    }
}