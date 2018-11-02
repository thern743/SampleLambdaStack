using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class Line2
    {
        [JsonProperty("S")]
        public string Value { get; set; }

        [JsonProperty("NULL")]
        public bool NULL { get; set; }
    }
}