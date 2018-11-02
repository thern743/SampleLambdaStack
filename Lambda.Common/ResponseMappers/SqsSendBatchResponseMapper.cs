using System.Collections.Generic;
using System.Net;
using Amazon.SQS.Model;
using Lambda.Common.AWS.SQS;

namespace Lambda.Common.ResponseMappers
{
    public class SqsSendBatchResponseMapper : ResponseMapper<SendMessageBatchResponse, List<SqsSendMessageResponse>>
    {
        public override List<SqsSendMessageResponse> Map(SendMessageBatchResponse messageResponse)
        {
            Source = messageResponse;
            Destination = new List<SqsSendMessageResponse>();

            foreach (var message in messageResponse.Successful)
            {
                Destination.Add(new SqsSendMessageResponse { MessageId = message.MessageId, HttpStatusCode = HttpStatusCode.OK });
            }

            foreach (var message in messageResponse.Failed)
            {
                Destination.Add(new SqsSendMessageResponse { MessageId = message.Id, HttpStatusCode = HttpStatusCode.BadRequest });
            }

            return Destination;
        }
    }
}
