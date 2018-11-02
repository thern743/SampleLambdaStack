using Lambda.Common.AWS.SQS;
using Lambda.Common.Messaging;

namespace SampleServerlessNetCoreLambda.Common
{
    public class MySampleEvent : SqsEvent<EventMessage>
    {
    }
}
