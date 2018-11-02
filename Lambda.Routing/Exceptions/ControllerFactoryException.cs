using System;

namespace Lambda.Routing.Exceptions
{
    public class ControllerFactoryException : Exception
    {
        public ControllerFactoryException(string message) : base(message) { }
        public ControllerFactoryException(string message, Exception ex) : base(message, ex) { }
    }
}
