using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity.Configuration;

namespace PSC_HRM.Module.Extend
{
    public class NhanSuRegister : IRegister
    {
        public void Register()
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = "NhanSu.config";
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            UnityConfigurationSection section = config.GetSection("unity") as UnityConfigurationSection;
            section.Configure(SystemContainer.Instance);
        }
    }
}
