using System;
using System.Collections.Generic;
using System.Linq;
using Lambda.Common.Interfaces;
using Newtonsoft.Json;

namespace Lambda.Common.Builders.PingResponse
{
    public class PingResponseBuilder : IPingResponseBuilder
    {
        private IDictionary<IDependency, string> Dependencies { get; } = new Dictionary<IDependency,string>();
        private const string Ok = "OK";
        private const string Bad = "BAD";
        public string Build()
        {
            if (Dependencies == null || !Dependencies.Any()) throw new InvalidOperationException("Dependencies must be added first.");
            return JsonConvert.SerializeObject(Dependencies);
        }

        public bool AllDependenciesAreHealthy()
        {
            if (Dependencies == null || !Dependencies.Any()) throw new InvalidOperationException("Dependencies must be added first.");
            return Dependencies.All(d => d.Key.IsHealthy);
        }


        public PingResponseBuilder AddDependency(IDependency dependency)
        {
            Dependencies[dependency] = dependency.IsHealthy ? Ok : Bad;
            return this;
        }

        public PingResponseBuilder AddDependencies(IEnumerable<IDependency> dependencies)
        {
            foreach (var dependency in dependencies)
            {
               Dependencies[dependency] = dependency.IsHealthy ? Ok : Bad;
            }            
            return this;
        }
    }
}