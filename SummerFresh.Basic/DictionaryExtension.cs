using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Basic
{
    public static class DictionaryExtension
    {
        public static object ToEntity(this IDictionary<string, object> source, Type type)
        {
            if (source.IsNullOrEmpty()) return null;
            FastReflection.FastType fastType = FastReflection.FastType.Get(type);
            var instance = Activator.CreateInstance(type);
            foreach (var p in fastType.Setters)
            {
                if (p.Name.IsNullOrEmpty()) continue;
                if (source.Keys.Contains(p.Name))
                {
                    p.SetValue(instance, source[p.Name].ConventToType(p.Type));
                }
            }
            return instance;
        }

        public static T ToEntity<T>(this IDictionary<string, object> source) where T : class,new()
        {
            return ToEntity(source, typeof(T)) as T;
        }

        public static List<T> ToEntities<T>(this IList<IDictionary<string, object>> source) where T : class,new()
        {
            var list = new List<T>();
            source.ForEach(o =>
            {
                list.Add(o.ToEntity<T>());
            });
            return list;
        }

        public static void AddRange(this IDictionary<string, object> source, IDictionary<string, object> target)
        {
            if (source == null || target.IsNullOrEmpty()) return;
            foreach (var item in target)
            {
                if (!source.Keys.Contains(item.Key, StringComparer.OrdinalIgnoreCase))
                {
                    source.Add(item);
                }
            }
        }

        public static T TryGetValue<T>(this IDictionary<string, T> source, string key) where T : class
        {
            T value;
            if (source.TryGetValue(key, out value))
            {
                return value;
            }
            return null;
        }
    }
}
