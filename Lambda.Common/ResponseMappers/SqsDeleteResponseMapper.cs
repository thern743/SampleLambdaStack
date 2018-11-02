using Amazon.SQS.Model;
using Lambda.Common.AWS.SQS;

namespace Lambda.Common.ResponseMappers
{
    public class SqsDeleteResponseMapper : ResponseMapper<DeleteMessageResponse, SqsDeleteMessageResponse>
    {
        public override SqsDeleteMessageResponse Map(DeleteMessageResponse messageResponse)
        {
            Source = messageResponse;
            Destination = new SqsDeleteMessageResponse { HttpStatusCode = Source.HttpStatusCode };
            return Destination;
        }
    }
}
