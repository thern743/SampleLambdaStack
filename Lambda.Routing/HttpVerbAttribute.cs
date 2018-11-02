using System;
using System.Collections.Generic;
using System.Linq;
using Lambda.Routing.Interfaces;

namespace Lambda.Routing
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HttpVerbAttribute : Attribute, IHttpVerbAttribute
    {
        public IEnumerable<string> Verbs { get; private set; }

        public HttpVerbAttribute(params string[] verbs)
        {
            if (verbs.Length == 0 || verbs.Any(string.IsNullOrEmpty)) throw new ArgumentNullException(nameof(verbs));
            Verbs = verbs;
        }
    }
}