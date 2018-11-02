using System;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Lambda.Common.AWS.SQS;
using Lambda.Common.Extensions;
using Lambda.Common.Interfaces;

namespace Lambda.Common.Clients.Queue
{       
    public class SqsQueueClient : IQueueClient, ISqsQueueClient
    {
        private readonly AmazonSQSClient _amazonSqsClient;
        private readonly string _queueUrl;
        private readonly IResponseMapper<SendMessageResponse, SqsSendMessageResponse> _sqsSendResponseMapper;
        private readonly IResponseMapper<DeleteMessageResponse, SqsDeleteMessageResponse> _sqsDeleteResponseMapper;

        public SqsQueueClient(string queueUrl, AmazonSQSClient amazonSqsClient, 
            IResponseMapper<SendMessageResponse, SqsSendMessageResponse> sqsSendResponseMapper,
            IResponseMapper<DeleteMessageResponse, SqsDeleteMessageResponse> sqsDeleteResponseMapper)
        {
            _amazonSqsClient = amazonSqsClient;
            _sqsSendResponseMapper = sqsSendResponseMapper;
            _sqsDeleteResponseMapper = sqsDeleteResponseMapper;
            _queueUrl = queueUrl;
        }

        public async Task<IHttpMessageResponse> SendMessage(object payload)
        {
            try
            {
                var jsonPayload = payload.ToJson();
                var sendMessageRequest = new SendMessageRequest { QueueUrl = _queueUrl, MessageBody = jsonPayload };
                var sendMessageResponse = await SendMessageAsync(sendMessageRequest);
                Console.WriteLine($"SQS Send Message ID: {sendMessageResponse.MessageId}");
                var sqsSendMessageResponse = _sqsSendResponseMapper.Map(sendMessageResponse);
                return sqsSendMessageResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not send payload.");
                HandleError(ex);
            }

            return new SqsSendMessageResponse { MessageId = string.Empty };
        }

        public virtual async Task<SendMessageResponse> SendMessageAsync(SendMessageRequest sendMessageRequest)
        {
            var sendMessageResponse = await _amazonSqsClient.SendMessageAsync(sendMessageRequest);
            return sendMessageResponse;
        }

        public async Task<IHttpMessageResponse> DeleteMessage(string id)
        {
            try
            {
                var deleteMessageRequest = new DeleteMessageRequest { QueueUrl = _queueUrl, ReceiptHandle = id };
                var deleteMessageResponse = await DeleteMessageAsync(deleteMessageRequest);
                Console.WriteLine($"SQS Delete Response: {deleteMessageResponse.HttpStatusCode}");
                var sqsSendMessageResponse = _sqsDeleteResponseMapper.Map(deleteMessageResponse);
                return sqsSendMessageResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not delete message.");
                HandleError(ex);
            }

            return new SqsSendMessageResponse();
        }

        public async Task<DeleteMessageResponse> DeleteMessageAsync(DeleteMessageRequest deleteMessageRequest)
        {
            var deleteMessageResponse = await _amazonSqsClient.DeleteMessageAsync(deleteMessageRequest);
            return deleteMessageResponse;
        }
        
        private static void HandleError(Exception ex)
        {
            // TODO: Fire off message to error queue
        }
    }
}
