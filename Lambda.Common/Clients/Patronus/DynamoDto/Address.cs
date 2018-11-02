using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class Address
    {
        [JsonProperty("M")]
        public AddressDocument AddressDocument { get; set; }
    }
}