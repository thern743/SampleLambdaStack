using Amazon.SQS.Model;
using Lambda.Common.AWS.SQS;

namespace Lambda.Common.ResponseMappers
{
    public class SqsSendResponseMapper : ResponseMapper<SendMessageResponse, SqsSendMessageResponse>
    {
        public override SqsSendMessageResponse Map(SendMessageResponse messageResponse)
        {
            Source = messageResponse;
            Destination = new SqsSendMessageResponse { MessageId = Source.MessageId, HttpStatusCode = Source.HttpStatusCode };
            return Destination;
        }
    }
}
