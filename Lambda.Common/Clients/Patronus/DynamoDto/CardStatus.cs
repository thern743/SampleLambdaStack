using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class CardStatus
    {
        [JsonProperty("M")]
        public CardStatusDocument CardStatusDocument { get; set; }
    }
}