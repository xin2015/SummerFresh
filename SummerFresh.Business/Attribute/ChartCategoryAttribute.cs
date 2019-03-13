using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    /// <summary>
    /// 图表分类特性
    /// </summary>
    public class ChartCategoryAttribute:Attribute
    {
        /// <summary>
        /// 格式化模板，空则不格式化
        /// </summary>
        public string Formatter { get; set; }
    }
}
