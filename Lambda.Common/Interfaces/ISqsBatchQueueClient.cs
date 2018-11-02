using System.Threading.Tasks;
using Amazon.SQS.Model;

namespace Lambda.Common.Interfaces
{
    public interface ISqsBatchQueueClient
    {
        Task<SendMessageBatchResponse> SendMessageBatchAsync(SendMessageBatchRequest requesty);
        Task<DeleteMessageBatchResponse> DeleteMessageBatchAsync(DeleteMessageBatchRequest request);
    }
}