using System;
using System.Collections.Concurrent;
using System.Linq;
using Amazon;
using Amazon.SimpleSystemsManagement;
using Lambda.Common.Interfaces;

namespace Lambda.Common.Clients.Factories
{
    public class SsmClientFactory : IClientFactory<IEncryptedParameterClient>, ISingleton
    {
        private static readonly Lazy<SsmClientFactory> Instance = new Lazy<SsmClientFactory>(() => new SsmClientFactory());

        public static ConcurrentDictionary<string, IEncryptedParameterClient> Clients { get; }

        static SsmClientFactory()
        {
            Clients = new ConcurrentDictionary<string, IEncryptedParameterClient>();
        }

        private SsmClientFactory()
        {
        }

        public static IClientFactory<IEncryptedParameterClient> GetInstance()
        {
            return Instance.Value;
        }

        public IEncryptedParameterClient GetClient()
        {
            return Clients?.Count > 0
                ? Clients.FirstOrDefault().Value
                : GetClient("default");
        }

        public IEncryptedParameterClient GetClient(string id)
        {
            if (Clients.ContainsKey(id)) return Clients[id];
            var config = new AmazonSimpleSystemsManagementConfig { RegionEndpoint = RegionEndpoint.USWest2 };
            var ssmClient = new AmazonSimpleSystemsManagementClient(config);
            var parameterStoreClient = new EncryptedParameterStoreClient(ssmClient);
            Clients.TryAdd(id, parameterStoreClient);
            return Clients[id];
        }

        public void Dispose() { }
    }
}
