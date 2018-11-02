using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class EmployeeNumber
    {
        [JsonProperty("S")]
        public string Value { get; set; }
    }
}