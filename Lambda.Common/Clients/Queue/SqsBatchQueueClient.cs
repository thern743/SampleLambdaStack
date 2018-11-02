using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Lambda.Common.AWS.SQS;
using Lambda.Common.Extensions;
using Lambda.Common.Interfaces;

namespace Lambda.Common.Clients.Queue
{       
    public class SqsBatchQueueClient : IBatchQueueClient, ISqsBatchQueueClient
    {
        private readonly AmazonSQSClient _amazonSqsClient;
        private readonly string _queueUrl;
        private readonly IResponseMapper<SendMessageBatchResponse, List<SqsSendMessageResponse>> _sqsSendResponseMapper;
        private readonly IResponseMapper<DeleteMessageBatchResponse, List<SqsDeleteMessageResponse>> _sqsDeleteResponseMapper;

        public SqsBatchQueueClient(string queueUrl, AmazonSQSClient amazonSqsClient, 
            IResponseMapper<SendMessageBatchResponse, List<SqsSendMessageResponse>> sqsSendBatchResponseMapper, 
            IResponseMapper<DeleteMessageBatchResponse, List<SqsDeleteMessageResponse>> sqsDeleteBatchResponseMapper)
        {
            _amazonSqsClient = amazonSqsClient;
            _sqsSendResponseMapper = sqsSendBatchResponseMapper;
            _sqsDeleteResponseMapper = sqsDeleteBatchResponseMapper;
            _queueUrl = queueUrl;
        }

        public async Task<IEnumerable<IHttpMessageResponse>> SendMessage(IEnumerable<object> payloads)
        {
            try
            {
                var sendMessageBatchRequest = BuildSendMessageBatchRequest(payloads);
                var sendMessageBatchResponse = await SendMessageBatchAsync(sendMessageBatchRequest);
                Console.WriteLine($"SQS Batch Send Result: Failed = {sendMessageBatchResponse.Failed.Count}, Success = {sendMessageBatchResponse.Successful.Count}");
                var sqsSendMessageResponse = _sqsSendResponseMapper.Map(sendMessageBatchResponse);
                return sqsSendMessageResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not send batch payload.");
                HandleError(ex);
            }

            return new List<SqsSendMessageResponse>();
        }

        public async Task<IEnumerable<IHttpMessageResponse>> DeleteMessage(IEnumerable<string> ids)
        {
            try
            {
                var deleteMessageBatchRequest = BuildDeleteMessageBatchRequest(ids);
                var deleteMessageResponse = await DeleteMessageBatchAsync(deleteMessageBatchRequest);
                Console.WriteLine($"SQS Delete Response: {deleteMessageResponse.HttpStatusCode}");
                var sqsSendMessageResponse = _sqsDeleteResponseMapper.Map(deleteMessageResponse);
                return sqsSendMessageResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not delete message.");
                HandleError(ex);
            }

            return new List<SqsSendMessageResponse>();
        }
        private SendMessageBatchRequest BuildSendMessageBatchRequest(IEnumerable<object> payloads)
        {
            var jsonPayloads = payloads.Select(x => x.ToJson()).ToList();
            var sendMessageBatchRequest = new SendMessageBatchRequest { QueueUrl = _queueUrl, Entries = new List<SendMessageBatchRequestEntry>() };

            foreach (var message in jsonPayloads)
            {
                var entry = new SendMessageBatchRequestEntry(Guid.NewGuid().ToString(), message);
                sendMessageBatchRequest.Entries.Add(entry);
            }

            return sendMessageBatchRequest;
        }

        private DeleteMessageBatchRequest BuildDeleteMessageBatchRequest(IEnumerable<string> ids)
        {
            var deleteMessageBatchRequest =
                new DeleteMessageBatchRequest {QueueUrl = _queueUrl, Entries = new List<DeleteMessageBatchRequestEntry>()};

            foreach (var id in ids)
            {
                var entry = new DeleteMessageBatchRequestEntry(Guid.NewGuid().ToString(), id);
                deleteMessageBatchRequest.Entries.Add(entry);
            }

            return deleteMessageBatchRequest;
        }

        public virtual async Task<SendMessageBatchResponse> SendMessageBatchAsync(SendMessageBatchRequest sendMessageBatchRequest)
        {
            var sendMessageBatchResponse = await _amazonSqsClient.SendMessageBatchAsync(sendMessageBatchRequest);
            return sendMessageBatchResponse;
        }

        public virtual async Task<DeleteMessageBatchResponse> DeleteMessageBatchAsync(DeleteMessageBatchRequest deleteMessageBatchRequest)
        {
            var deleteMessageResponse = await _amazonSqsClient.DeleteMessageBatchAsync(deleteMessageBatchRequest);
            return deleteMessageResponse;
        }

        private static void HandleError(Exception ex)
        {
            // TODO: Fire off message to error queue
        }
    }
}
