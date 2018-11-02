using System.Collections.Generic;

namespace Lambda.Common.Interfaces
{
    public interface IRequest
    {
        string Path { get; set; }
        string HttpMethod { get; set; }
        string Body { get; set; }
        string Resource { get; set; }
        string RequestId { get; set; }
        IDictionary<string, string> QueryStringParameters { get; set; }
        IDictionary<string, string> PathParameters { get; set; }
        IDictionary<string, string> Headers { get; set; }
    }
}