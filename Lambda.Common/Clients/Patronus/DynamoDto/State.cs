using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class State
    {
        [JsonProperty("S")]
        public string Value { get; set; }
    }
}