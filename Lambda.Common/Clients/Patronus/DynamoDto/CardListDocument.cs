using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class CardListDocument
    {
        [JsonProperty("M")]
        public Card Card { get; set; }
    }
}