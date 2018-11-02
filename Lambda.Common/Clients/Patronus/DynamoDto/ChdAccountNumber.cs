using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class ChdAccountNumber
    {
        [JsonProperty("S")]
        public string Value { get; set; }
    }
}