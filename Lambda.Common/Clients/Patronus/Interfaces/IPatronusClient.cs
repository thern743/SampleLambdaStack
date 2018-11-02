using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lambda.Common.Clients.Patronus.Interfaces
{
    public interface IPatronusClient
    {
        Task<IEnumerable<string>> GetPrimaryFromChd(string chd);
        Task<IEnumerable<string>> GetPrimaryFromDurableId(string durableId);
        Task<IPatronusRecord> GetRecord(string chdUserTx);
    }
}