using System.Threading.Tasks;

namespace Lambda.Common.Interfaces
{
    public interface IDependency
    {
        /// <summary>
        /// Checks the health status of the dependency provided.
        /// </summary>
        /// <returns></returns>
        Task<bool> CheckHealth();
        /// <summary>
        /// The name of the dependency being evaluated.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Boolean value of health status.
        /// </summary>
        bool IsHealthy { get; set; }        
    }
}