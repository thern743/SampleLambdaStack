using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lambda.Common.Interfaces
{
    public interface IBatchQueueClient
    {
        Task<IEnumerable<IHttpMessageResponse>> SendMessage(IEnumerable<object> payloads);
        Task<IEnumerable<IHttpMessageResponse>> DeleteMessage(IEnumerable<string> ids);
    }
}