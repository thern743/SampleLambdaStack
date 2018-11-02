using System;
using System.Collections.Concurrent;
using System.Linq;
using Amazon;
using Amazon.DynamoDBv2;
using Lambda.Common.Clients.Patronus;
using Lambda.Common.Clients.Patronus.Interfaces;
using Lambda.Common.Interfaces;

namespace Lambda.Common.Clients.Factories
{
    public class DynamoClientFactory : IClientFactory<IDynamoClient>, ISingleton
    {
        private static readonly Lazy<DynamoClientFactory> Instance = new Lazy<DynamoClientFactory>(() => new DynamoClientFactory());

        public static ConcurrentDictionary<string, IDynamoClient> Clients { get; }

        static DynamoClientFactory()
        {
            Clients = new ConcurrentDictionary<string, IDynamoClient>();
        }

        private DynamoClientFactory()
        {
        }

        public static IClientFactory<IDynamoClient> GetInstance()
        {
            return Instance.Value;
        }

        public IDynamoClient GetClient()
        {
            return Clients?.Count > 0
                ? Clients.FirstOrDefault().Value
                : GetClient("default");
        }

        public IDynamoClient GetClient(string tableName)
        {
            if (Clients.ContainsKey(tableName)) return Clients[tableName];
            var dynamoClient = new AmazonDynamoDBClient(RegionEndpoint.USWest2);
            var dynamoClientWrapper = new DynamoClientWrapper(dynamoClient, tableName);
            Clients.TryAdd(tableName, dynamoClientWrapper);
            return Clients[tableName];
        }

        public void Dispose() { }
    }
}
