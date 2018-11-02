using System.Collections.Generic;
using System.Net;
using Amazon.SQS.Model;
using Lambda.Common.AWS.SQS;

namespace Lambda.Common.ResponseMappers
{
    public class SqsDeleteBatchResponseMapper : ResponseMapper<DeleteMessageBatchResponse, List<SqsDeleteMessageResponse>>
    {
        public override List<SqsDeleteMessageResponse> Map(DeleteMessageBatchResponse messageResponse)
        {
            Source = messageResponse;
            Destination = new List<SqsDeleteMessageResponse>();

            foreach (var message in messageResponse.Successful)
            {
                Destination.Add(new SqsDeleteMessageResponse { MessageId = message.Id, HttpStatusCode = HttpStatusCode.OK });
            }

            foreach (var message in messageResponse.Successful)
            {
                Destination.Add(new SqsDeleteMessageResponse { MessageId = message.Id, HttpStatusCode = HttpStatusCode.BadRequest });
            }

            return Destination;
        }
    }
}
