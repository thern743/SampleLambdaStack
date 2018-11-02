using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lambda.Routing.Exceptions;
using Lambda.Routing.Interfaces;

namespace Lambda.Routing
{
    public class ControllerFactory<TController>
    {
        private static class TypeLocker<T>
        {
            public static readonly object Locker = new object();
        }

        private static ControllerFactory<TController> _instance;
        protected IRouteInfoStrategy RouteInfoStrategy;
        
        private TController _controllerInstance;

        private ControllerFactory(IRouteInfoStrategy routeInfoStrategy)
        {
            RouteInfoStrategy = routeInfoStrategy;
        }

        public static ControllerFactory<TController> GetInstance(IRouteInfoStrategy routeInfoStrategy)
        {
            if (_instance != null) return _instance;
            lock (TypeLocker<TController>.Locker) _instance = new ControllerFactory<TController>(routeInfoStrategy);
            return _instance;
        }

        public ILambdaRouteInfo GetRouteInfo(IRouteTemplate template)
        {
            const string errMsg = "Could not find controller.";

            try
            {
                var resource = string.IsNullOrWhiteSpace(template.Resource) ? "default" : template.Resource;
                var path = template.Path;
                var parameters = template.PathParameters != null && template.PathParameters.Any() 
                    ? template.PathParameters 
                    : new Dictionary<string, string>();

                var verbs = template.Verbs != null && template.Verbs.Any()
                    ? template.Verbs
                    : new string[] { };

                var aggregate = RouteInfoStrategy.GetRouteInfo()
                    .Where(lambdaRoute => lambdaRoute.RouteAttribute != null)
                    .FirstOrDefault(
                        lambdaRouteInfo => IsMatch(lambdaRouteInfo, resource, path, parameters, verbs));

                if (aggregate != null) return aggregate;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Caught exception: {ex.Message}");
                throw;
            }

            Console.WriteLine($"Error: {errMsg}");
            throw new Exception(errMsg);
        }

        public TController GetControllerInstance()
        {
            if (_controllerInstance == null)
            {
                try
                {
                    _controllerInstance = Activator.CreateInstance<TController>();
                }
                catch (TargetInvocationException tie)
                {
                    Console.WriteLine($"Caught exception: {tie.Message}");
                    if (tie.InnerException != null ) Console.WriteLine($"Caught exception: {tie.InnerException.Message}");
                    throw new ControllerFactoryException($"Could not create instance of {typeof(TController).FullName}.", tie);
                }

                Console.WriteLine($"Creating {_controllerInstance.GetType()}");
            }

            Console.WriteLine($"Using {_controllerInstance.GetType().Name}");
            return _controllerInstance;
        }

        public bool IsMatch(ILambdaRouteInfo routeInfo, string resource, string path, IDictionary<string, string> pathParameters,
            IEnumerable<string> verbs)
        {
            if (routeInfo.RouteAttribute == null) return false;
            var matchString = pathParameters.Aggregate(routeInfo.RouteAttribute.Resource, (current, parameter) => current.Replace("{" + parameter.Key + "}", parameter.Value));

            var verbMatch = routeInfo?.VerbAttribute == null
                ? true
                : !routeInfo.VerbAttribute.Verbs.Except(verbs).Any() && !verbs.Except(routeInfo.VerbAttribute.Verbs).Any();

            var isMatch = (routeInfo.RouteAttribute.Resource.Equals(resource, StringComparison.CurrentCultureIgnoreCase)
                              || matchString.Equals(path, StringComparison.InvariantCultureIgnoreCase))
                && verbMatch;

            return isMatch;
        }
    }
}
