using System;
using System.Collections.Concurrent;
using System.Linq;
using Amazon.SQS;
using Lambda.Common.Clients.Queue;
using Lambda.Common.Interfaces;
using Lambda.Common.ResponseMappers;

namespace Lambda.Common.Clients.Factories
{
    public class SqsQueueClientFactory : IClientFactory<IQueueClient>, ISingleton
    {
        private static readonly Lazy<SqsQueueClientFactory> Instance = new Lazy<SqsQueueClientFactory>(() => new SqsQueueClientFactory());
        private static IConfigService<IQueueConfig> _queueConfigService;
        public static ConcurrentDictionary<string, IQueueClient> Clients { get; }        

        static SqsQueueClientFactory()
        {            
            Clients = new ConcurrentDictionary<string, IQueueClient>();
            if (_queueConfigService?.Configs == null) return;
            foreach (var config in _queueConfigService.Configs)
            {
                Clients[config.Name] = GetClientByUrl(config.Url);                
            }
        }

        protected SqsQueueClientFactory()
        {            
        }

        #region Interface Implementations

        public static IClientFactory<IQueueClient> GetInstance(IConfigService<IQueueConfig> queueConfigService)
        {
            _queueConfigService = queueConfigService;
            return Instance.Value;
        }

        public IQueueClient GetClient()
        {
            return Clients?.Count > 0
                ? Clients.FirstOrDefault().Value
                : GetClient("default");
        }

        public virtual IQueueClient GetClient(string clientName)
        {
            if (Clients.ContainsKey(clientName)) return Clients[clientName];
            var queueConfig = _queueConfigService.GetConfigOrDefault(clientName);
            var queueClient = BuildClient(queueConfig?.Url ?? clientName);
            Clients.TryAdd(clientName, queueClient);
            return Clients[clientName];
        }

        private static IQueueClient GetClientByUrl(string queueUrl)
        {
            var queueClient = BuildClient(queueUrl);
            return queueClient;
        }

        private static IQueueClient BuildClient(string url)
        {
            var amazonSqsConfig = new AmazonSQSConfig { ServiceURL = "http://sqs.us-west-2.amazonaws.com" };
            var amazonSqsClient = new AmazonSQSClient(amazonSqsConfig);
            var sendResponseMapper = new SqsSendResponseMapper();
            var deleteResponseMapper = new SqsDeleteResponseMapper();
            var queueClient = new SqsQueueClient(url, amazonSqsClient, sendResponseMapper, deleteResponseMapper);
            return queueClient;
        }

        public void Dispose()
        {
            foreach (var client in Clients)
            {
                var sqsClient = client.Value;
                sqsClient = null; // sqsClient.Dispose()
            }
        }

        #endregion
    }
}
