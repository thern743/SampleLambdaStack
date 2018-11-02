using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class Active
    {
        [JsonProperty("BOOL")]
        public bool Value { get; set; }
    }
}