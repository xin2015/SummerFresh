using SummerFresh.Business.Entity;
using SummerFresh.Data;
using SummerFresh.Data.Attributes;
using SummerFresh.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
namespace SummerFresh.Business
{
    public class BSDContext<T> where T : CustomEntity, new()
    {
        public static IList<T> Instance
        {
            get
            {
                string cacheKey = GetKey();
                return CacheHelper.GetFromCache<IList<T>>(cacheKey, () =>
                {
                    return Dao.Get().SelectAll<T>();
                });
            }
        }

        private static IDictionary<string, T> _kvCollection;

        public static T KVCollection(Func<T, string> keySelector, string key)
        {
            var dict = _kvCollection ?? (_kvCollection = Instance.ToDictionary(keySelector, j => j));
            return dict.TryGetValue(key);
        }

        private static string GetKey()
        {
            string cacheKey = NamingCenter.GetCacheKey(CacheType.ENTITY_DATA, typeof(T).FullName);
            var attr = typeof(T).GetCustomAttribute<TableAttribute>(true);
            if (attr != null)
            {
                cacheKey = NamingCenter.GetCacheKey(CacheType.ENTITY_DATA, attr.Name);
            }
            return cacheKey;
        }

        public static bool ClearCache()
        {
            string cacheKey = GetKey();
            return CacheHelper.Remove(cacheKey);
        }
    }

    public static class BSDContextCache
    {
        public static bool ClearTableCache(string tableName)
        {
            bool result = false;
            string cacheKey = NamingCenter.GetCacheKey(CacheType.ENTITY_DATA, tableName);
            result = CacheHelper.Remove(cacheKey);
            cacheKey = NamingCenter.GetCacheKey(CacheType.DATA_TABLE, tableName);
            CacheHelper.Remove(cacheKey);
            return result;
        }
    }
}
