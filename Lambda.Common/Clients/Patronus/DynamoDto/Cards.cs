using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class Cards
    {
        [JsonProperty("L")]
        public List<CardListDocument> CardListDocument { get; set; }
    }
}