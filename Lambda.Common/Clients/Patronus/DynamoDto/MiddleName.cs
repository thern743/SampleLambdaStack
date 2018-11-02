using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class MiddleName
    {
        [JsonProperty("S")]
        public string Value { get; set; }

        [JsonProperty("NULL")]
        public bool NULL { get; set; }
    }
}