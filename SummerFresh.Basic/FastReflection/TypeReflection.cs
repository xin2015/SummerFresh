using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SummerFresh.Basic.FastReflection
{
    public class TypeReflection
    {
        private Type type;
        private PropertyInfo[] _setters;
        private PropertyInfo[] _getters;
        private Dictionary<PropertyInfo, PropertyAccessor> accessors = new Dictionary<PropertyInfo, PropertyAccessor>();

        internal TypeReflection(Type type)
        {
            this.type = type;
            CreatePropertyAccessors();
        }

        public PropertyInfo[] Getters
        {
            get { return _getters; }
        }

        public PropertyInfo[] Setters
        {
            get { return _setters; }
        }

        public void SetValue(object instance,PropertyInfo prop,object value)
        {
            accessors[prop].SetValue(instance, value);
        }

        public object GetValue(object instance,PropertyInfo prop)
        {
            return accessors[prop].GetValue(instance);
        }

        private void CreatePropertyAccessors()
        {
            List<PropertyInfo> setters = new List<PropertyInfo>();
            List<PropertyInfo> getters = new List<PropertyInfo>();

            foreach (PropertyInfo prop in GetProperties(type))
            {
                if (prop.CanRead)
                {
                    getters.Add(prop);
                }

                if (prop.CanWrite)
                {
                    setters.Add(prop);
                }

                accessors[prop] = new PropertyAccessor(prop);
            }

            _setters = setters.ToArray();
            _getters = getters.ToArray();
        }

        public static IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        }
    }
}
