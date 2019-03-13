using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SummerFresh.Business;

namespace SummerFresh.Controls
{
    public class ChartConfig
    {
        public string ChartName { get; set; }

        public string ChartTitle { get; set; }

        /// <summary>
        /// 图表副标题
        /// </summary>
        public string ChartSubTitle { get; set; }

        public Color BackgroundColor { get; set; }

        public ChartTheme ChartTheme { get; set; }

        /// <summary>
        /// 是否显示Chart标题
        /// </summary>
        public bool ShowChartTtile { get; set; }

        /// <summary>
        /// 是否显示Chart副标题
        /// </summary>
        public bool ShowChartSubTitle { get; set; }

        /// <summary>
        /// 图表标题显示位置
        /// </summary>
        public ChartItemLocation ChartTitleLocation { get; set; }

        /// <summary>
        /// 图表子标题显示位置
        /// </summary>
        public ChartItemLocation ChartSubTitleLocation { get; set; }

        /// <summary>
        /// 图表标题显示坐标
        /// </summary>
        public Point ChartTitlePosition { get; set; }

        /// <summary>
        /// 副标题显示坐标
        /// </summary>
        public Point ChartSubTitltePosition { get; set; }

        /// <summary>
        /// 图示显示
        /// </summary>
        public bool ShowLegend { get; set; }

        /// <summary>
        /// 图示显示位置
        /// </summary>
        public ChartItemLocation LegendLocation { get; set; }

        /// <summary>
        /// 图示显示坐标
        /// </summary>
        public Point LegendPosition { get; set; }

        /// <summary>
        /// 图示显示格式
        /// </summary>
        public string LegendFormat { get; set; }

        /// <summary>
        /// 图示标题
        /// </summary>
        public string LegendTitle { get; set; }

        /// <summary>
        /// 显示tooltip
        /// </summary>
        public bool ShowToolTip { get; set; }

        /// <summary>
        /// 水平准线
        /// </summary>
        public bool ToolTipVerticalHair { get; set; }

        /// <summary>
        /// 垂直准线
        /// </summary>
        public bool ToolTipHorizontalHair { get; set; }

        /// <summary>
        ///  ToolTip提示框中该点的HTML代码
        /// </summary>
        public string ToolTipPointFormatter { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public IListDataSource ChartDataSource { get; set; }

        public Dictionary<ChartFieldTypeEnum, List<ChartFieldAttribute>> AllTypeFielsCollections { get; set; }


        public List<ChartXAxisConfig> XAxisLst { get; set; }

        public List<ChartYAxisConfig> YAxisLst { get; set; }

        public List<ChartSeriesConfig> SeriesConfig { get; set; }
    }
}
