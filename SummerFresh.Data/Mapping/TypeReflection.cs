using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using SummerFresh.Basic;
using SummerFresh.Data.Attributes;
using SummerFresh.Basic.FastReflection;

namespace SummerFresh.Data.Mapping
{
    internal class TypeReflection : FastType
    {
        internal new static FastType Get(Type type)
        {
            return Get(type, delegate { return new TypeReflection(type); });
        }

        internal TypeReflection(Type type) : base(type)
        {

        }

        protected override string GetPropertyName(PropertyInfo p)
        {
            object[] attributes = p.GetCustomAttributes(typeof(ColumnAttribute), false);

            if (attributes.Length > 0 && !((ColumnAttribute)attributes[0]).Name.IsNullOrEmpty())
            {
                return ((ColumnAttribute)attributes[0]).Name;
            }
            return p.Name;
        }
    }
}
