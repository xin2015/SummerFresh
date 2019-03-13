using System;
using System.Collections.Generic;
using System.Threading;


namespace SummerFresh.Basic.FastReflection
{
    public abstract class FastReflectionCache<TKey, TValue> : IFastReflectionCache<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> m_cache = new Dictionary<TKey, TValue>();
#if SILVERLIGHT
        public TValue Get(TKey key)
        {
            TValue value = default(TValue);

            bool cacheHit = false;
            lock(m_cache)
            {
                cacheHit = this.m_cache.TryGetValue(key, out value);
            }

            if (cacheHit)
            {
                return value;
            }
            lock(m_cache)
            {
                if (this.m_cache.TryGetValue(key, out value))
                {
                    return value;
                }

                value = this.Create(key);
                this.m_cache[key] = value;    
            }
            
            return value;
        }
#else
        private readonly ReaderWriterLockSlim m_rwLock = new ReaderWriterLockSlim();

        public TValue Get(TKey key)
        {
            TValue value;

            m_rwLock.EnterUpgradeableReadLock();
            try
            {
                if (!m_cache.TryGetValue(key, out value))
                {
                    this.m_rwLock.EnterWriteLock();
                    try
                    {
                        value = this.Create(key);
                        this.m_cache[key] = value;
                    }
                    finally
                    {
                        this.m_rwLock.ExitWriteLock();
                    }
                }
            }
            finally
            {
                m_rwLock.ExitUpgradeableReadLock();
            }
            return value;
        }
#endif

        protected abstract TValue Create(TKey key);
    }
}
