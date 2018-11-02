using System;

namespace Lambda.Common.Interfaces
{
    public interface IClientFactory<TClient> : IDisposable
    {
        TClient GetClient();
        TClient GetClient(string id);
    }
}