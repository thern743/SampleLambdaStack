using System;
using System.Collections.Concurrent;
using System.Linq;
using Lambda.Common.Clients.Patronus;
using Lambda.Common.Clients.Patronus.Interfaces;
using Lambda.Common.Interfaces;

namespace Lambda.Common.Clients.Factories
{
    public class PatronusClientFactory : IClientFactory<IPatronusClient>, ISingleton
    {
        private static readonly Lazy<PatronusClientFactory> Instance = new Lazy<PatronusClientFactory>(() => new PatronusClientFactory());

        public static ConcurrentDictionary<string, IPatronusClient> Clients { get; }

        static PatronusClientFactory()
        {
            Clients = new ConcurrentDictionary<string, IPatronusClient>();
        }

        private PatronusClientFactory()
        {
        }

        public static IClientFactory<IPatronusClient> GetInstance()
        {
            return Instance.Value;
        }

        public IPatronusClient GetClient()
        {
            return Clients?.Count > 0
                ? Clients.FirstOrDefault().Value
                : GetClient("default");
        }

        public IPatronusClient GetClient(string tableName)
        {
            if (Clients.ContainsKey(tableName)) return Clients[tableName];
            var dynamoClientFactory = DynamoClientFactory.GetInstance();
            var patronusClient = new PatronusClient(dynamoClientFactory, tableName);
            Clients.TryAdd(tableName, patronusClient);
            return Clients[tableName];
        }

        public void Dispose() { }
    }
}
