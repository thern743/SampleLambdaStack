using System.Threading.Tasks;

namespace Lambda.Common.Clients.Patronus.Interfaces
{
    public interface IPatronusUatClient : IPatronusClient
    {
        Task<PatronusGenerationResponse> GenerateRecord(string durable = null, string chd = null);
    }
}