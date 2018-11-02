using Amazon.Lambda.Core;
using Lambda.Common.Interfaces;
using Lambda.Routing;
using Lambda.Routing.Interfaces;

namespace Lambda.Common.Adapters
{
    public class SqsEventAdapter : IEventAdapter
    {
        public IContext TransformContext(ILambdaContext lambdaContext)
        {
            var context = new Context
            {
                RequestId = lambdaContext.AwsRequestId
            };

            return context;
        }

        public IRouteTemplate TransformRequestToTemplate()
        {
            return new RouteTemplate { Resource = "default" };
        }
    }
}
