using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using SummerFresh.Util;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Script.Serialization;
namespace SummerFresh.Business
{
    /// <summary>
    /// 枚举数据源
    /// </summary>
    [DisplayName("枚举数据源")]
    public class EnumDataSource : KeyValueDataSourceBase
    {
        /// <summary>
        /// 枚举类型全称
        /// </summary>
        [DisplayName("枚举类型全称")]
        [FunctionDataSource(typeof(AllEnumDataSource))]
        [FormField(ControlType=ControlType.DropDownList)]
        public string EnumTypeFullName { get; set; }

        private Type _enumType;

        [ScriptIgnore]
        public Type EnumType 
        {
            get
            {
                if (_enumType == null)
                {
                    if (!string.IsNullOrEmpty(EnumTypeFullName))
                    {
                        _enumType = TypeHelper.GetType(EnumTypeFullName);
                    }
                }
                return _enumType;
            }
            set
            {
                _enumType = value;
            }
        }

        /// <summary>
        /// 枚举绑定值类型
        /// </summary>
        [DisplayName("绑定值类型")]
        public EnumValueType EnumValueType { get; set; }
        public override IList<SelectListItem> SelectItems()
        {
            var items = new List<SelectListItem>();
            FieldInfo[] fieldinfos = EnumType.GetFields();
            string tempValue = string.Empty;
            string tempText = string.Empty;
            foreach (FieldInfo field in fieldinfos)
            {
                if (field.FieldType.IsEnum)
                {
                    tempText = field.GetDescription();
                    if (EnumValueType == EnumValueType.Value)
                    {
                        tempValue = ((int)Enum.Parse(EnumType, field.Name)).ToString();
                    }
                    else
                    {
                        tempValue = field.Name;
                    }
                    items.Add(new SelectListItem() { Value = tempValue, Text = tempText });
                }
            }
            return items;
        }
    }

    /// <summary>
    /// 枚举值类型
    /// </summary>
    public enum EnumValueType
    {
        /// <summary>
        /// 值
        /// </summary>
        [Description("值")]
        Value,
        /// <summary>
        /// 编码
        /// </summary>
        [Description("编码")]
        Code
    }
}
