using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace POSApplication.Common
{
    public class ConfigHelper
    {
        /// <summary>
        /// 返回＊.exe.config文件中appSettings配置节的value项
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string GetAppConfig(string strKey)
        {
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key == strKey)
                    return ConfigurationManager.AppSettings[strKey];
            }
            return "";
        }

        /// <summary>
        /// 在＊.exe.config文件中appSettings配置节增加一对键、值对（对于已存在的，删除再添加）
        /// </summary>
        /// <param name="newKey"></param>
        /// <param name="newValue"></param>
        public static void UpdateAppConfig(string newKey, string newValue)
        {
            bool isModified = false;
            //检查是否已存在key
            foreach (string key in ConfigurationManager.AppSettings)
                if (key == newKey)
                    isModified = true;

            // Open App.Config of executable
            Configuration config =ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            // 如果是修改，先删除已存在的key
            if (isModified)
                config.AppSettings.Settings.Remove(newKey);

            config.AppSettings.Settings.Add(newKey, newValue);

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
