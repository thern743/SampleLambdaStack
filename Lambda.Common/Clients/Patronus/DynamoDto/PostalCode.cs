﻿using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class PostalCode
    {
        [JsonProperty("S")]
        public string Value { get; set; }
    }
}