using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using Lambda.Common.Clients.Http;
using Lambda.Common.Interfaces;

namespace Lambda.Common.Clients.Factories
{
    public class HttpClientFactory : IClientFactory<IHttpClient>, ISingleton
    {
        private static readonly Lazy<HttpClientFactory> Instance = new Lazy<HttpClientFactory>(() => new HttpClientFactory());
        private static int _timeout = 60;

        public static ConcurrentDictionary<string, HttpClientFacade> Clients { get; }

        static HttpClientFactory()
        {
            SetTimeout();
            Clients = new ConcurrentDictionary<string, HttpClientFacade>();
        }

        private static void SetTimeout()
        {
            var timeout = Environment.GetEnvironmentVariable("HttpRequestTimeoutSeconds") ?? "60";
            if (!int.TryParse(timeout, out _))
            {
                _timeout = 60;
            }
        }
        
        #region Interface Implementations

        public static IClientFactory<IHttpClient> GetInstance()
        {
            return Instance.Value;
        }

        public IHttpClient GetClient()
        {
            return Clients?.Count > 0 
                ? Clients.FirstOrDefault().Value 
                : GetClient("default");
        }

        public IHttpClient GetClient(string id)
        {
            if (Clients.ContainsKey(id)) return Clients[id];
            var handler = GetHandler();

            var client = new HttpClientFacade(new HttpClientFacadeConfig(){HttpMessageHandler = handler, Timeout = TimeSpan.FromSeconds(_timeout)});
            Clients.TryAdd(id, client);
            return Clients[id];
        }

        public void Dispose()
        {
            foreach(var client in Clients)
                client.Value.Dispose();
        }

        #endregion

        private static HttpClientHandler GetHandler()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback += (message, certificate2, arg3, arg4) => true;
            return handler;
        }
    }
}
