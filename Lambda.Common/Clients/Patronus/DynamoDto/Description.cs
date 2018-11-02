using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class Description
    {
        [JsonProperty("S")]
        public string Value { get; set; }
    }
}