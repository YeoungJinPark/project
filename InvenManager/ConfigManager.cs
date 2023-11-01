using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace InvenManager
{
    class ConfigManager
    {
        public string GetValue(string key)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
            {
                return ConfigurationManager.AppSettings[key];
            }
            else
            {
                return null;
            }
        }
        
        /// <summary>
        /// app.config에 key-value속성을 가진 add요소 추가
        /// </summary>
        /// <param name="key"></param> 추가할 key
        /// <param name="value"></param> 추가할 value
        /// <param name="save"></param> true면 save
        public void SetValue(string key, string value)
        {
            Configuration con = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (con.AppSettings.Settings[key] == null)
                con.AppSettings.Settings.Add(key, value);
            else
                con.AppSettings.Settings[key].Value = value;

            con.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
