using System;

namespace Lambda.Routing.Exceptions
{
    public class RouteInfoException : Exception
    {
        public RouteInfoException(string message) : base(message) { }
        public RouteInfoException(string message, Exception ex) : base(message, ex) { }
    }
}
