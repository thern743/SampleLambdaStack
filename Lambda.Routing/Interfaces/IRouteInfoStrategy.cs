using System;
using System.Collections.Generic;

namespace Lambda.Routing.Interfaces
{
    public interface IRouteInfoStrategy
    {
        IEnumerable<ILambdaRouteInfo> GetRouteInfo();
    }
}