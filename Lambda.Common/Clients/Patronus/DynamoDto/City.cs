using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class City
    {
        [JsonProperty("S")]
        public string Value { get; set; }
    }
}