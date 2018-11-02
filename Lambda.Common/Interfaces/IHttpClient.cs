using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Lambda.Common.Interfaces
{
    public interface IHttpClient
    {
        HttpResponseMessage Get(string url);
        HttpResponseMessage Post(string url, HttpContent content);
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage message);
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
        HttpRequestHeaders DefaultRequestHeaders { get; }
    }
}
