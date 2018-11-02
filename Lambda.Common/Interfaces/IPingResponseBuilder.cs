using System.Collections.Generic;
using Lambda.Common.Builders.PingResponse;

namespace Lambda.Common.Interfaces
{
    public interface IPingResponseBuilder
    {
        /// <summary>
        /// Add multiple dependencies to the builder. If they exist already on the builder, they will be overwritten.
        /// </summary>
        /// <param name="dependencies"></param>
        /// <returns></returns>
        PingResponseBuilder AddDependencies(IEnumerable<IDependency> dependencies);
        /// <summary>
        /// Add one dependency to the builder. If it exists already on the builder, it will be overwritten.
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        PingResponseBuilder AddDependency(IDependency dependency);
        /// <summary>
        /// Builds a serialized response object from the mapped dependencies and their various health states.
        /// </summary>
        /// <returns></returns>
        string Build();
        /// <summary>
        /// Checks if all dependencies on the builder are in a healthy state.
        /// </summary>
        /// <returns></returns>
        bool AllDependenciesAreHealthy();
    }
}