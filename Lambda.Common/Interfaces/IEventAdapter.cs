using Amazon.Lambda.Core;
using Lambda.Routing.Interfaces;

namespace Lambda.Common.Interfaces
{
    public interface IEventAdapter
    {
        IContext TransformContext(ILambdaContext lambdaContext);
        IRouteTemplate TransformRequestToTemplate();
    }
}
