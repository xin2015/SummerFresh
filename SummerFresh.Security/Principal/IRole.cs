using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SummerFresh.Security.Principal
{
    /// <summary>
    /// 表示一个系统角色
    /// </summary>
    [TypeConverter(typeof(RoleConverter))]
    public interface IRole
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 显示名称
        /// </summary>
        string Name { get; }
    }

    public class RoleConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return typeof (string).Equals(destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return ((IRole) value).Id;
        }
    }
}