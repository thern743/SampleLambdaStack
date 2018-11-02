using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class ClosedDate
    {
        [JsonProperty("S")]
        public string Value { get; set; }

        [JsonProperty("NULL")]
        public string NULL { get; set; }
    }
}