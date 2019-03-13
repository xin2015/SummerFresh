using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Controls
{
    public class ChartField
    {
        public string FieldName { get; set; }

        public string DisplayName { get; set; }

        /// <summary>
        /// 显示在X轴还是Y轴
        /// </summary>
        public AxisType AxisType { get; set; }

        /// <summary>
        /// 轴上的标签显示格式
        /// </summary>
        public string DisplayTextFormat { get; set; }
    }

    public enum AxisType
    { 
        XAxis,
        YAxis,
    }
}
