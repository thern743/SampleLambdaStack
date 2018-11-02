using System.Collections.Generic;
using System.Net;
using Lambda.Common.Interfaces;
using Newtonsoft.Json;

namespace Lambda.Common
{
    public class Response : IResponse
    {
        public Response() { }

        public Response(string body, HttpStatusCode statusCode) : this(statusCode)
        {
            Body = body;
        }

        public Response(HttpStatusCode statusCode)
        {
            StatusCode = (int)statusCode;
        }

        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("headers")]
        public IDictionary<string, string> Headers { get; set; }

        [JsonProperty("isBase64Encoded")]
        public bool IsBase64Encoded { get; set; }
    }
}