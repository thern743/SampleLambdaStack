using System.Threading.Tasks;

namespace Lambda.Common.Interfaces
{
    public interface IQueueClient
    {
        Task<IHttpMessageResponse> SendMessage(object payload);
        Task<IHttpMessageResponse> DeleteMessage(string id);
    }
}