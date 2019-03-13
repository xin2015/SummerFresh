using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SummerFresh.Business
{
    /// <summary>
    /// 图表特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple=false,Inherited=true)]
    public class ChartAttribute:Attribute
    {

        /// <summary>
        /// 图表标题
        /// </summary>
        public string ChartTitle { get; set; }

        /// <summary>
        /// 图表副标题
        /// </summary>
        public string ChartSubTitle { get; set; }

        /// <summary>
        /// 图表皮肤
        /// </summary>
        public ChartTheme ChartTheme { get; set; }
    }
}
