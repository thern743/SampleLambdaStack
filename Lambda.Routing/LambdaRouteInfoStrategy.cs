using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lambda.Routing.Interfaces;

namespace Lambda.Routing
{
    public class LambdaRouteInfoStrategy<TController> : IRouteInfoStrategy
    {
        private IEnumerable<ILambdaRouteInfo> _routeInfo;


        public IEnumerable<ILambdaRouteInfo> GetRouteInfo()
        {
            return _routeInfo ??
                    (
                        _routeInfo =
                            typeof(TController)
                                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                .Select(x => new LambdaRouteInfo
                                    {
                                        RouteAttribute = (LambdaRouteAttribute)Attribute.GetCustomAttribute(x, typeof(LambdaRouteAttribute)),
                                        VerbAttribute = (HttpVerbAttribute)Attribute.GetCustomAttribute(x, typeof(HttpVerbAttribute)),
                                        MethodName = x.Name,
                                        MethodInfo = x
                                    })
                                .Where(routeInfo => routeInfo.RouteAttribute != null)
                    );
        }
    }
}
