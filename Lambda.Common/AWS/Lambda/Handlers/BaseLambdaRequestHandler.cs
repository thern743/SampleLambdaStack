using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Lambda.Routing;
using Lambda.Routing.Exceptions;
using Lambda.Common.Adapters;
using Lambda.Common.Exceptions;
using Lambda.Common.Interfaces;

namespace Lambda.Common.AWS.Lambda.Handlers
{
    public class BaseLambdaRequestHandler<TController>
    {

        public async Task<IResponse> Handler(Request request, ILambdaContext context)
        {
            try
            {
                var requestAdapter = new AwsRequestAdapter();
                var localContext = requestAdapter.TransformContext(context);
                var routeTemplate = requestAdapter.TransformRequestToTemplate(request);
                var routeStrategy = new LambdaRouteInfoStrategy<TController>();
                var controllerFactory = ControllerFactory<TController>.GetInstance(routeStrategy);
                if (controllerFactory == null) throw new ControllerFactoryException("Could not load RouteInfo.");
                var routeInfo = controllerFactory.GetRouteInfo(routeTemplate);
                if (string.IsNullOrWhiteSpace(routeInfo?.MethodName) || routeInfo.MethodInfo == null) throw new RouteInfoException("Could not load RouteInfo.");
                var controllerInstance = controllerFactory.GetControllerInstance();
                var invoker = new ControllerMethodInvoker<IResponse>();
                Console.WriteLine($"Found Controller Route: {controllerInstance.GetType().Name}");
                var response = await invoker.QuasWexExortAsync(routeInfo, controllerInstance, new object[] { request, localContext });
                Console.WriteLine($"Base Handler returning.");
                if (response == null) throw new NullResponseException("Controller response was null.");
                return response;
            }
            catch (NullResponseException nre)
            {
                Console.WriteLine($"Caught exception: {nre.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Caught exception: {ex.Message}");

                return new Response
                {
                    StatusCode = 500,
                    Body = "Could not handle request."
                };
            }
        }
    }
}
