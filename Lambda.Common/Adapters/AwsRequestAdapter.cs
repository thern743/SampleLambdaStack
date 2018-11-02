using Amazon.Lambda.Core;
using Lambda.Common.Interfaces;
using Lambda.Routing;
using Lambda.Routing.Interfaces;

namespace Lambda.Common.Adapters
{
    public class AwsRequestAdapter : IRequestAdapter
    {
        public IContext TransformContext(ILambdaContext lambdaContext)
        {
            var context = new Context
            {
                RequestId = lambdaContext.AwsRequestId
            };

            return context;
        }

        public IRouteTemplate TransformRequestToTemplate(IRequest request)
        {
            var routeTemplate = new RouteTemplate
            {
                Path = request.Path,
                Resource = request.Resource,
                PathParameters = request.PathParameters,
                Verbs = new[] { request.HttpMethod }
            };

            return routeTemplate;
        }
    }
}
