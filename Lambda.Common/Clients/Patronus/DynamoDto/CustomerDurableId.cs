using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class CustomerDurableId
    {
        [JsonProperty("S")]
        public string Value { get; set; }
    }
}