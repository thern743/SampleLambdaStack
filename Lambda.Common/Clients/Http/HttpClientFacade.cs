using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Lambda.Common.Interfaces;

namespace Lambda.Common.Clients.Http
{
    public class HttpClientFacade : IHttpClient, IDisposable
    {
        private readonly HttpClient _client;

        public HttpClientFacade(HttpClientFacadeConfig config = null)
        {
            _client = config != null ? new HttpClient(config.HttpMessageHandler) : new HttpClient();
            if (config?.Timeout != null) _client.Timeout = config.Timeout;
        }

        public HttpResponseMessage Get(string url) => GetAsync(url).Result;
        public HttpResponseMessage Post(string url, HttpContent content) => PostAsync(url, content).Result;
        public async Task<HttpResponseMessage> GetAsync(string url) => await _client.GetAsync(url);
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage message) => await _client.SendAsync(message);
        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content) => await _client.PostAsync(url, content);
        public HttpRequestHeaders DefaultRequestHeaders => _client.DefaultRequestHeaders;

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
