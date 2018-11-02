using System;
using System.Collections.Concurrent;
using System.Linq;
using Amazon.SQS;
using Lambda.Common.Clients.Queue;
using Lambda.Common.Interfaces;
using Lambda.Common.ResponseMappers;

namespace Lambda.Common.Clients.Factories
{
    public class SqsBatchQueueClientFactory : IClientFactory<IBatchQueueClient>, ISingleton
    {
        private static readonly Lazy<SqsBatchQueueClientFactory> Instance = new Lazy<SqsBatchQueueClientFactory>(() => new SqsBatchQueueClientFactory());
        private static IConfigService<IQueueConfig> _queueConfigService;

        public static ConcurrentDictionary<string, IBatchQueueClient> Clients { get; }

        static SqsBatchQueueClientFactory()
        {
            Clients = new ConcurrentDictionary<string, IBatchQueueClient>();
            if (_queueConfigService?.Configs == null) return;
            foreach (var config in _queueConfigService.Configs)
            {
                Clients[config.Name] = GetClientByUrl(config.Url);
            }
        }

        protected SqsBatchQueueClientFactory()
        {
        }

        #region Interface Implementations

        public static IClientFactory<IBatchQueueClient> GetInstance(IConfigService<IQueueConfig> queueConfigService)
        {
            _queueConfigService = queueConfigService;
            return Instance.Value;
        }

        public IBatchQueueClient GetClient()
        {
            return Clients?.Count > 0
                ? Clients.FirstOrDefault().Value
                : GetClient("default");
        }

        public virtual IBatchQueueClient GetClient(string clientName)
        {
            if (Clients.ContainsKey(clientName)) return Clients[clientName];
            var queueConfig = _queueConfigService.GetConfigOrDefault(clientName);
            var queueClient = BuildClient(queueConfig?.Url ?? clientName);
            Clients.TryAdd(clientName, queueClient);
            return Clients[clientName];
        }

        private static IBatchQueueClient GetClientByUrl(string queueUrl)
        {
            var queueClient = BuildClient(queueUrl);
            return queueClient;
        }

        private static IBatchQueueClient BuildClient(string url)
        {
            var amazonSqsConfig = new AmazonSQSConfig { ServiceURL = "http://sqs.us-west-2.amazonaws.com" };
            var amazonSqsClient = new AmazonSQSClient(amazonSqsConfig);
            var sendBatchResponseMapper = new SqsSendBatchResponseMapper();
            var deleteBatchResponseMapper = new SqsDeleteBatchResponseMapper();
            var queueClient = new SqsBatchQueueClient(url, amazonSqsClient, sendBatchResponseMapper, deleteBatchResponseMapper);
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
