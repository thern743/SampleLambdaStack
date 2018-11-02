using System.Reflection;
using Lambda.Routing.Interfaces;

namespace Lambda.Routing
{
    public class LambdaRouteInfo : ILambdaRouteInfo
    {
        public string MethodName { get; set; }
        public MethodInfo MethodInfo { get; set; }
        public ILambdaRouteAttribute RouteAttribute { get; set; }
        public IHttpVerbAttribute VerbAttribute { get; set; }
    }
}
