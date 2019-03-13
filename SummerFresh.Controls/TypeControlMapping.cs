using SummerFresh.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Controls
{
    public static class TypeControlMapping
    {
        private static IDictionary<Type, Type> typeControlMappingDict = new Dictionary<Type, Type>();

        private static object lockKey = new object();
        static TypeControlMapping()
        {
            typeControlMappingDict.Add(typeof(System.Int16), typeof(TextBox));
            typeControlMappingDict.Add(typeof(System.Int32), typeof(TextBox));
            typeControlMappingDict.Add(typeof(System.Int64), typeof(TextBox));
            typeControlMappingDict.Add(typeof(System.Decimal), typeof(TextBox));
            typeControlMappingDict.Add(typeof(System.Boolean), typeof(DropDownList));
            typeControlMappingDict.Add(typeof(System.DateTime), typeof(DatePicker));
            typeControlMappingDict.Add(typeof(System.String), typeof(TextBox));
        }

        public static void RegisterTypeControl(Type type,Type controlType)
        {
            lock(lockKey)
            {
                typeControlMappingDict[type] = controlType;
            }
        }
        public static FormControlBase GetTypeControl(Type type)
        {
            if(typeControlMappingDict.Keys.Contains(type))
            {
                return Activator.CreateInstance(typeControlMappingDict[type]) as FormControlBase;
            }
            return new TextBox();
        }
    }
}
