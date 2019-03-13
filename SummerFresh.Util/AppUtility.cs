using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace SummerFresh.Util
{
    public static class AppUtility
    {
        public const string CONFIG_DIRECTORY_KEY = "Application.Configuration.Directory";
        public const string CONFIG_DIRECTORY_NAME1 = "App_Config";
        public const string CONFIG_DIRECTORY_NAME2 = "Config";
        public static string UnitTestHttpContextKey = "UnitTestHttpContextKey";
        /// <summary>
        /// 在当前应用中读出某节（section）配置信息对应的配置对象
        /// </summary>
        /// <remarks>
        /// 此方法优先从<c>configFileName</c>中指定的配置文件中检索，如果该配置文件不存在再从默认配置中进行检索
        /// </remarks>
        /// <typeparam name="T">
        /// 要返回的配置对象，必须是<see cref="System.Configuration.ConfigurationSection">ConfigurationSection</see>的实现类
        /// </typeparam>
        /// <param name="sectionName">配置节的在配置文件中的名称</param>
        /// <param name="configFileName">外部配置文件的名称</param>
        /// <returns>不存在返回null</returns>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">ConfigurationErrorsException</exception>
        public static T GetConfigSection<T>(string sectionName, string configFileName) where T : ConfigurationSection
        {
            FileInfo configFile;

            if (FindConfigFile(configFileName, out configFile))
            {
                ExeConfigurationFileMap map =
                    new ExeConfigurationFileMap() { ExeConfigFilename = configFile.FullName };

                Configuration configuration =
                    ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

                return configuration.GetSection(sectionName) as T;
            }
            else
            {
                return ConfigurationManager.GetSection(sectionName) as T;
            }
        }

        /// <summary>
        /// 在应用的配置目录<see cref="P:Bingosoft.Core.App.ConfigDirectory">App.ConfigDirectory</see>下查找指定的文件
        /// </summary>
        /// <param name="fileName">要查找的文件名</param>
        /// <param name="fileInfo">要查找的文件对应的<c>FileInfo</c>对象</param>
        /// <returns>
        /// 文件是否存在，存在返回<c>true</c>,否则返回<c>false</c>
        /// </returns>
        public static bool FindConfigFile(string fileName, out FileInfo fileInfo)
        {
            string file = FindConfigDirectory().FullName + "\\" + fileName;

            if (File.Exists(file))
            {
                fileInfo = new FileInfo(file);
                return true;
            }
            else
            {
                fileInfo = null;
                return false;
            }
        }

        /// <summary>
        /// 在应用的配置目录<see cref="P:Bingosoft.Core.App.ConfigDirectory">App.ConfigDirectory</see>下查找指定的目录
        /// </summary>
        /// <param name="dirName">要查找的目录名</param>
        /// <param name="dirInfo">要查找的目录对应的<c>DirectoryInfo</c>对象</param>
        /// <returns>
        /// 目录是否存在，存在返回<c>true</c>,否则返回<c>false</c>
        /// </returns>
        public static bool FindConfigDirectory(string dirName, out DirectoryInfo dirInfo)
        {
            string dir = FindConfigDirectory().FullName + "\\" + dirName;

            if (!Directory.Exists(dir))
            {
                dirInfo = null;
                return false;
            }
            else
            {
                dirInfo = new DirectoryInfo(dir);
                return true;
            }
        }

        private static DirectoryInfo FindConfigDirectory()
        {
            DirectoryInfo dir;

            if (!AppUtility.FindDirectoryOrConfig(CONFIG_DIRECTORY_KEY, CONFIG_DIRECTORY_NAME1, out dir) &&
               !AppUtility.FindDirectory(CONFIG_DIRECTORY_NAME2, out dir))
            {
                //log.Debug("application config directory not found,use 'AppDomain.BaseDirectory'");
                dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            }
            else
            {
                //log.Debug("Found application config directory : {0}", dir.Name);
            }

            return dir;
        }

        /// <summary>
        /// TODO : Document me
        /// </summary>
        /// <param name="configKey"></param>
        /// <param name="dirName"></param>
        /// <param name="dirInfo"></param>
        /// <returns></returns>
        public static bool FindDirectoryOrConfig(string configKey, string dirName, out DirectoryInfo dirInfo)
        {
            string dirPath = ConfigurationManager.AppSettings[configKey];
            if (!string.IsNullOrEmpty(dirPath))
            {
                if (dirPath.StartsWith("~") && null != HttpContext.Current)
                {
                    dirPath = HttpContext.Current.Server.MapPath(dirPath);
                }

                if (Directory.Exists(dirPath))
                {
                    dirInfo = new DirectoryInfo(dirPath);
                    return true;
                }
                else
                {
                    dirInfo = null;
                    return false;
                }
            }
            else
            {
                return FindDirectory(dirName, out dirInfo);
            }
        }

        /// <summary>
        /// TODO : Document me
        /// </summary>
        /// <param name="dirName"></param>
        /// <param name="dirInfo"></param>
        /// <returns></returns>
        public static bool FindDirectory(string dirName, out DirectoryInfo dirInfo)
        {
            string dir = null;

            //TODO 完善：适用测试环境HttpContext.Current.Server.MapPath("~")报错的问题            
            if (HttpContext.Current!=null && !HttpContext.Current.Items.Contains(UnitTestHttpContextKey))
            {
                //ASP.NET Application
                dir = HttpContext.Current.Server.MapPath("~") + "\\" + dirName;
            }
            else
            {
                //Console Or WinForm Application
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                dir = baseDir + "\\" + dirName;

                if (!Directory.Exists(dir))
                {
                    if (baseDir.ToLower().LastIndexOf("bin\\debug") > 0 ||
                       baseDir.ToLower().LastIndexOf("bin\\release") > 0)
                    {
                        dir = baseDir + "\\..\\..\\" + dirName;
                    }
                }
            }

            if (!Directory.Exists(dir))
            {
                dirInfo = null;
                return false;
            }
            else
            {
                dirInfo = new DirectoryInfo(dir);
                return true;
            }
        }
    }
}
