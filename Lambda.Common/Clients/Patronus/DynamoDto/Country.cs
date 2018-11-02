using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class Country
    {
        [JsonProperty("S")]
        public string Value { get; set; }
    }
}