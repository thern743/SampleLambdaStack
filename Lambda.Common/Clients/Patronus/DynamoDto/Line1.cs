using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class Line1
    {
        [JsonProperty("S")]
        public string Value { get; set; }
    }
}