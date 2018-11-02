using System;
using System.Net.Http;

namespace Lambda.Common.Clients.Http
{
    public class HttpClientFacadeConfig
    {
        public HttpMessageHandler HttpMessageHandler { get; set; }
        public TimeSpan Timeout { get; set; }
    }
}
