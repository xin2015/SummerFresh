using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SummerFresh.Business
{
    /// <summary>
    /// 图表字段特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public class ChartFieldAttribute : Attribute
    {
        public ChartFieldAttribute()
        {
            YAxisMax = decimal.MinValue;
            YAxisMin = decimal.MinValue;
            SeriesColor = Color.Empty;
        }

        public ChartFieldTypeEnum FieldType { get; set; }

        public string FiledName { get; set; }

        #region Data

        /// <summary>
        /// 值格式
        /// </summary>
        public string DataValueFormat { get; set; }

        /// <summary>
        /// ToolTip中显示的格式
        /// </summary>
        public string DataToolTipFormat { get; set; }

        /// <summary>
        /// Data的名字
        /// </summary>
        public string DataName { get; set; }

        #endregion

        #region Series

        public string SeriesDisplayName { get; set; }

        /// <summary>
        /// 分类颜色
        /// </summary>
        public Color SeriesColor { get; set; }

        /// <summary>
        /// 图表类型
        /// </summary>
        public ChartType SeriesChartType { get; set; }

        /// <summary>
        /// 判定Data是同一分组
        /// </summary>
        public Func<object, bool> IsGroupValue { get; set; }


        #endregion

        #region Category

        public string CategoryDisplayName { get; set; }

        /// <summary>
        /// 在ToolTip中显示的格式
        /// </summary>
        public string CategoryToolTipFormat { get; set; }

        /// <summary>
        /// 类别值格式
        /// </summary>
        public string CategoryValueFormat { get; set; }

        #endregion

        #region SeriesStack


        #endregion

        #region YAxis

        /// <summary>
        /// Y轴最大值
        /// </summary>
        public decimal YAxisMax { get; set; }

        /// <summary>
        /// Y轴最小值
        /// </summary>
        public decimal YAxisMin { get; set; }

        /// <summary>
        /// Y轴显示名称
        /// </summary>
        public string YAxisDisPalyName { get; set; }

        #endregion


        //此类型毋需DisplayName
        //public string SeriesStack{get;}

    }
}
