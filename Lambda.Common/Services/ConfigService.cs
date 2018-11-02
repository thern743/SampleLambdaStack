using System;
using System.Collections.Generic;
using System.Linq;
using Lambda.Common.Interfaces;

namespace Lambda.Common.Services
{
    public class ConfigService<TConfig> : IConfigService<TConfig> where TConfig : IConfig
    {
        public List<TConfig> Configs { get; set; } = new List<TConfig>();

        public void AddRange(IEnumerable<TConfig> configs)
        {
            Configs.AddRange(configs);
        }

        public TConfig GetConfigOrDefault(string configName)
        {
            return Configs.FirstOrDefault(c => string.Equals(c.Name, configName, StringComparison.InvariantCultureIgnoreCase));
        }

        public void AddConfig(TConfig config)
        {                   
            Configs.Add(config);
        }
    }
}
