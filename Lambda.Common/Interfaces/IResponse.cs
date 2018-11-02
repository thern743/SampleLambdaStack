using System.Collections.Generic;

namespace Lambda.Common.Interfaces
{
    public interface IResponse
    {
        int StatusCode { get; set; }
        string Body { get; set; }
        IDictionary<string, string> Headers { get; set; }
        bool IsBase64Encoded { get; set; }
    }
}
