using System.Collections.Generic;

namespace Lambda.Routing.Interfaces
{
    public interface IHttpVerbAttribute
    {
        IEnumerable<string> Verbs { get; }
    }
}