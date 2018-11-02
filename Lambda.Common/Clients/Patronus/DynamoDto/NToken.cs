using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class NToken
    {
        [JsonProperty("S")]
        public string Value { get; set; }
    }
}