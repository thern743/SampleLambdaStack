using System.Collections.Generic;

namespace Lambda.Common.Interfaces
{
    public interface IConfigService<TConfig> where TConfig : IConfig
    {
        /// <summary>
        /// Gets and adds a new instance of the provided configuration to the service.
        /// </summary>
        /// <param name="config"></param>
        void AddConfig(TConfig config);

        /// <summary>
        /// Adds a range of configs to the service.
        /// </summary>
        /// <param name="configs"></param>
        void AddRange(IEnumerable<TConfig> configs);

        /// <summary>
        /// Gets the config from the service if it exists.
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        TConfig GetConfigOrDefault(string configName);

        List<TConfig> Configs { get; set; }
    }
}