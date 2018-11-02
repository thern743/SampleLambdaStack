using System;
using System.Collections.Concurrent;
using System.Linq;
using Lambda.Common.Clients.Patronus;
using Lambda.Common.Clients.Patronus.Interfaces;
using Lambda.Common.Interfaces;

namespace Lambda.Common.Clients.Factories
{
    public class PatronusUatClientFactory : IClientFactory<IPatronusUatClient>, ISingleton
    {
        private static readonly Lazy<PatronusUatClientFactory> Instance = new Lazy<PatronusUatClientFactory>(() => new PatronusUatClientFactory());
        private static IConfigService<IDataStoreConfig> _dataStoreConfigService;

        public static ConcurrentDictionary<string, IPatronusUatClient> Clients { get; }

        static PatronusUatClientFactory()
        {
            Clients = new ConcurrentDictionary<string, IPatronusUatClient>();
            if (_dataStoreConfigService?.Configs == null) return;
        }

        private PatronusUatClientFactory()
        {
        }

        public static IClientFactory<IPatronusUatClient> GetInstance(IConfigService<IDataStoreConfig> dataStoreConfigService)
        {
            _dataStoreConfigService = dataStoreConfigService;
            foreach (var config in _dataStoreConfigService.Configs)
            {
                Clients[config.Name] = BuildClient(config);
            }
            return Instance.Value;
        }

        public IPatronusUatClient GetClient()
        {
            return Clients?.Count > 0
                ? Clients.FirstOrDefault().Value
                : GetClient("default");
        }

        public IPatronusUatClient GetClient(string tableName)
        {
            if (Clients.ContainsKey(tableName)) return Clients[tableName];
            var dynamoClientFactory = DynamoClientFactory.GetInstance();
            var patronusUatClient = new PatronusUatClient(dynamoClientFactory, tableName);
            Clients.TryAdd(tableName, patronusUatClient);
            return Clients[tableName];
        }

        private static IPatronusUatClient BuildClient(IDataStoreConfig config)
        {
            if (Clients.ContainsKey(config.Name)) return Clients[config.Name];
            var dynamoClientFactory = DynamoClientFactory.GetInstance();
            var patronusUatClient = new PatronusUatClient(dynamoClientFactory, config.TableName);
            Clients.TryAdd(config.Name, patronusUatClient);
            return Clients[config.Name];
        }

        public void Dispose() { }
    }
}
