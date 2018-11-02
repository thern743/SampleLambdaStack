using System.Collections.Generic;
using Lambda.Routing.Interfaces;

namespace Lambda.Routing
{
    public class RouteTemplate : IRouteTemplate
    {
        public string Resource { get; set; }
        public string Path { get; set; }
        public IDictionary<string, string> PathParameters { get; set; }
        public IEnumerable<string> Verbs { get; set; }
    }
}
