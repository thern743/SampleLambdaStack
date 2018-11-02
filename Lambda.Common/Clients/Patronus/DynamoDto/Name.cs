using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class Name
    {
        [JsonProperty("M")]
        public NameDocument NameDocument { get; set; }
    }
}