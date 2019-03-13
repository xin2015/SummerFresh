using SummerFresh.Basic.FastReflection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerFresh.Basic
{
    public static class ObjectExtension
    {
        public static T ConverTo<T>(this object value)
        {
            return (T)value.ConventToType(typeof(T));
        }

        public static object ConventToType(this object value, Type type)
        {
            if (null == value || value is DBNull ||
                (typeof(string) != type && (value is string) && string.IsNullOrEmpty((string)value)))
            {
                return type.IsValueType ? Activator.CreateInstance(type) : null;
            }
            Type valueType = value.GetType();
            if (valueType == type || type.IsAssignableFrom(valueType))
            {
                return value;
            }
            if (type.IsPrimitive)
            {
                try
                {
                    return System.Convert.ChangeType(value, type);
                }
                catch { }
            }
            TypeConverter converter = TypeDescriptor.GetConverter(type);
            if (null != converter)
            {
                if (converter.CanConvertFrom(valueType))
                {
                    return converter.ConvertFrom(value);
                }
                else if (type.IsValueType)
                {
                    return converter.ConvertFrom(value.ToString());
                }
            }
            converter = TypeDescriptor.GetConverter(valueType);
            if (null != converter && converter.CanConvertTo(type))
            {
                return converter.ConvertTo(value, type);
            }
            if (typeof(string).Equals(type))
            {
                return value.ToString();
            }
            throw new InvalidCastException(
                string.Format("Can Not Convert Type '{0}' To '{1}'",
                                value.GetType().FullName, type.FullName));
        }

        public static IDictionary<string, object> ToDictionary(this object value)
        {
            if (value == null) return null;
            IDictionary<string, object> dictionary = null;
            if (value.GetType() == typeof(Dictionary<string, object>))
            {
                dictionary = value as Dictionary<string, object>;
            }
            else
            {
                dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                FastType fastType = FastType.Get(value.GetType());
                fastType.Getters.ForEach(getter =>
                {
                    if (getter.Type.IsValueType || getter.Type == typeof(string))
                    {
                        string name = getter.Name;
                        if (!name.IsNullOrEmpty())
                        {
                            object val = getter.GetValue(value);
                            dictionary.Add(name, val);
                        }
                    }
                });
            }
            return dictionary;
        }
    }
}
