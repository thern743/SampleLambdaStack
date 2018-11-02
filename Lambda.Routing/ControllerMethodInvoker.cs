using System.Threading.Tasks;
using Lambda.Routing.Interfaces;

namespace Lambda.Routing
{
    public class ControllerMethodInvoker<TResponse>
    {
        public TResponse QuasWexExort(ILambdaRouteInfo routeInfo, object controllerInstance, object[] data)
        {
            var response = (TResponse)routeInfo?.MethodInfo.Invoke(controllerInstance, data);
            return response;
        }

        public async Task<TResponse> QuasWexExortAsync(ILambdaRouteInfo routeInfo, object controllerInstance, object[] data)
        {
            var response = await (Task<TResponse>)routeInfo.MethodInfo.Invoke(controllerInstance, data);
            return response;
        }
    }
}
