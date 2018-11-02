using Amazon.Lambda.Core;
using Lambda.Routing.Interfaces;

namespace Lambda.Common.Interfaces
{
    public interface IRequestAdapter
    {
        IContext TransformContext(ILambdaContext lambdaContext);
        IRouteTemplate TransformRequestToTemplate(IRequest request);
    }
}
