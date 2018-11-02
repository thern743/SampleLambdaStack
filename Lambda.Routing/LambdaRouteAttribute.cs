using System;
using System.Collections.Generic;
using System.Linq;
using Lambda.Routing.Interfaces;

namespace Lambda.Routing
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class LambdaRouteAttribute : Attribute, ILambdaRouteAttribute
    {
        public string Resource { get; }
        public IEnumerable<string> RouteParameters { get; private set; }

        public LambdaRouteAttribute(string resource)
        {
            if (string.IsNullOrWhiteSpace(resource)) throw new ArgumentNullException(nameof(resource));
            Resource = resource;
            BuildParameters();
        }

        private void BuildParameters()
        {
            RouteParameters = Resource.Split(new[] {'/',}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
