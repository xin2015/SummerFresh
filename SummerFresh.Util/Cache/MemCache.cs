using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Util.Cache
{
    public class MemCache : ICache
    {
        private static IDictionary<string, object> cache = new Dictionary<string, object>();

        public IList<string> AllKeys
        {
            get
            {
                return cache.Keys.ToList();
            }
        }

        public void Clean()
        {
            foreach (var key in AllKeys)
            {
                cache.Remove(key);
            }
        }

        public object GetValue(string key)
        {
            if (cache.Keys.Contains(key))
            {
                return cache[key];
            }
            return null;
        }

        public void Remove(string key)
        {
            if (cache.Keys.Contains(key))
            {
                cache.Remove(key);
            }
        }

        public void SetValue(string key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            cache[key] = value;
        }
    }
}
