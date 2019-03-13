using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerFresh.Basic
{
    public static class SysConfig
    {
        static SysConfig()
        {
            AppSettings = new AppSettingPropery();
            SystemTitle = AppSettings["SystemTitle"];
            CopyRightName = AppSettings["CopyRightName"];
            MailHost = AppSettings["MailHost"];
            MailPort = AppSettings["MailPort"].ConverTo<int>();
            MailUserName = AppSettings["MailUserName"];
            MailPassword = AppSettings["MailPassword"];
            MaintainerEmails = AppSettings["MaintainerEmails"];
            Assemblies = AppSettings["Assemblies"];
            SystemStatus = AppSettings["SystemStatus"];
        }

        public static AppSettingPropery AppSettings { get; private set; }

        public static string SystemTitle { get; private set; }

        public static string CopyRightName { get; private set; }

        public static string SystemVersion { get; private set; }

        /// <summary>
        /// 系统当前状态：Develop,Test,Run
        /// </summary>
        public static string SystemStatus { get; private set; }


        public static string MailHost { get; private set; }

        public static int MailPort { get; private set; }

        public static string MailUserName { get; private set; }

        public static string MailPassword { get; private set; }

        public static string MaintainerEmails { get; private set; }

        public static string Assemblies { get; private set; }
    }

    public class AppSettingPropery : Dictionary<string, string>
    {
        private AppSettingsReader _reader;
        private AppSettingsReader Reader
        {
            get
            {
                return _reader ?? (_reader = new AppSettingsReader());
            }
        }

        public new string this[string key]
        {
            get
            {
                if (!base.Keys.Contains(key))
                {
                    base[key] = Reader.GetValue(key, typeof(string)) as string;    
                }
                return base[key];
            }
            set
            {
                base[key] = value;
            }
        }
    }
}
