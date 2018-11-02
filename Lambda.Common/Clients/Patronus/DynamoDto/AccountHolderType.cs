using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class AccountHolderType
    {
        [JsonProperty("S")]
        public string Value { get; set; }
    }
}