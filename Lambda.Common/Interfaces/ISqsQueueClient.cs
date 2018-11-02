using System.Threading.Tasks;
using Amazon.SQS.Model;

namespace Lambda.Common.Interfaces
{
    public interface ISqsQueueClient
    {
        Task<SendMessageResponse> SendMessageAsync(SendMessageRequest request);
        Task<DeleteMessageResponse> DeleteMessageAsync(DeleteMessageRequest request);
    }
}