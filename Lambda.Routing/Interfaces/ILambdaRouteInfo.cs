using System.Reflection;

namespace Lambda.Routing.Interfaces
{
    public interface ILambdaRouteInfo
    {
        string MethodName { get; set; }
        MethodInfo MethodInfo { get; set; }
        ILambdaRouteAttribute RouteAttribute { get; set; }
        IHttpVerbAttribute VerbAttribute { get; set; }
    }
}