using System.Collections.Generic;

namespace Lambda.Routing.Interfaces
{
    public interface ILambdaRouteAttribute
    {
        string Resource { get; }
        IEnumerable<string> RouteParameters { get; }
    }
}