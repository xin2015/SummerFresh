using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace SummerFresh.Security.Cache
{
    internal class SecurityCache 
    {
        private static readonly object _syncRoot = new object();
        private static readonly string _cacheKey = typeof (SecurityCache).FullName;

        private readonly IDictionary<string, object> _items = new Dictionary<string, object>();
        private readonly ReaderWriterLockSlim _itemsLock = new ReaderWriterLockSlim();

        public static object Get(string name)
        {
            return Cache[name];
        }

        public static void Set(string name,object value)
        {
            Cache[name] = value;
        }

        public static SecurityCache Cache
        {
            get
            {
                SecurityCache cache = HttpRuntime.Cache[_cacheKey] as SecurityCache;
                if (null == cache)
                {
                    lock (_syncRoot)
                    {
                        if ((cache = HttpRuntime.Cache[_cacheKey] as SecurityCache) == null)
                        {
                            cache = new SecurityCache();
                            HttpRuntime.Cache[_cacheKey] = cache;
                        }
                    }
                }
                return cache;
            }
        }

        public object this[string name]
        {
            get
            {
                _itemsLock.EnterReadLock();
                try
                {
                    object value;
                    return _items.TryGetValue(name, out value) ? value : null;
                }
                finally
                {
                    _itemsLock.ExitReadLock();
                }
            }
            set
            {
                _itemsLock.EnterWriteLock();
                try
                {
                    _items[name] = value;
                }
                finally
                {
                    _itemsLock.ExitWriteLock();
                }
            }
        }

    }
}
