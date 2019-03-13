using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Basic.FastReflection
{
    public class TypeReflectionCache : FastReflectionCache<Type,TypeReflection>
    {
        private static TypeReflectionCache _cache = new TypeReflectionCache();

        public static TypeReflection GetReflection(Type type)
        {
            return _cache.Get(type);
        }
        protected override TypeReflection Create(Type key)
        {
            return new TypeReflection(key);
        }
    }
}
