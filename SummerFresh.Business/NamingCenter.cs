using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
namespace SummerFresh.Business
{
    public static class NamingCenter
    {
        public static string GetEntityTableId(Type entityType)
        {
            return "{0}_Table".FormatTo(entityType.FullName.Replace('.', '_'));
        }

        public static string GetEntityFormId(Type entityType)
        {
            return "{0}_Form".FormatTo(entityType.FullName.Replace('.', '_'));
        }

        public static string GetEntitySearchId(Type entityType)
        {
            return "{0}_Search".FormatTo(entityType.FullName.Replace('.', '_'));
        }

        public static string GetEntityToolbarId(Type entityType)
        {
            return "{0}_Toolbar".FormatTo(entityType.FullName.Replace('.', '_'));
        }

        public static string GetDefaultButtonId(ButtonEntityType buttonType)
        {
            return "btn{0}".FormatTo(buttonType.ToString());
        }



        public static string GetTimeRangeDatePickerStartId(string propertyName)
        {
            return "sdt{0}".FormatTo(propertyName);
        }

        public static string GetTimeRangeDatePickerEndId(string propertyName)
        {
            return "edt{0}".FormatTo(propertyName);
        }

        public static string GetCacheKey(CacheType cacheType,string key)
        {
            return "{0}_{1}".FormatTo(cacheType.ToString(), key);
        }
    }
}
