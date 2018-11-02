using System.Collections.Generic;
using Lambda.Common.Interfaces;

namespace Lambda.Common
{
    public class Request : IRequest
    {
        public string Path { get; set; }
        public string HttpMethod { get; set; }
        public string Body { get; set; }
        public string Resource { get; set; }
        public string RequestId { get; set; }
        public IDictionary<string, string> QueryStringParameters { get; set; }
        public IDictionary<string, string> PathParameters { get; set; }
        public IDictionary<string, string> Headers { get; set; }
    }
}