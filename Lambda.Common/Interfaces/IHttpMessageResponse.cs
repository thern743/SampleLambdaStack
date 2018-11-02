using System.Collections.Generic;
using System.Net;

namespace Lambda.Common.Interfaces
{
    public interface IHttpMessageResponse : IMessageResponse
    {
        HttpStatusCode HttpStatusCode { get; set; }
        IDictionary<string, string> Links { get; set; }
    }
}
