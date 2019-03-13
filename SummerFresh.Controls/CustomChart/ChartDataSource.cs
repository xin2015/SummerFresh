using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Controls
{
    public class ChartDataSource
    {
        /// <summary>
        /// 用于标识数据源的名称
        /// </summary>
        public string Name { get; set; }

        public ChartType ChartType { get; set; }
        
        //数据类型需要斟酌
        public IList<IDictionary<string, object>> Data { get; set; }

        public IList<ChartField> ChartField { get; set; }

        /// <summary>
        /// Y轴的最大值
        /// </summary>
        public decimal Max { get; set; }

        /// <summary>
        /// Y轴的最小值
        /// </summary>
        public decimal Min { get; set; }

    }

    public enum ChartType
    {
        /// <summary>
        /// 
        /// </summary>
        Line,
        Spline,
        Area,
        AreaSpline,
        Column,
        Bar,
        Pie,
        Scatter,
    }
}
