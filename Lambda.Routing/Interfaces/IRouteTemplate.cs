using System.Collections.Generic;

namespace Lambda.Routing.Interfaces
{
    public interface IRouteTemplate
    {
        string Resource { get; set; }
        string Path { get; set; }
        IDictionary<string, string> PathParameters { get; set; }
        IEnumerable<string> Verbs { get; set; }
    }
}