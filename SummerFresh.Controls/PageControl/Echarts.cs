using SummerFresh.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Basic;

namespace SummerFresh.Controls
{
    public class Echarts : NoPagerListControlBase
    {
        public Echarts()
        {
            IsZoom = true;
            EChartType = SummerFresh.Controls.EChartType.Scatter;
            SeriesType = SummerFresh.Controls.SeriesType.ColumnNameAsSeries;
            Height = 400;
        }

        [DisplayName("图表独立初始化方法")]
        public string ChartInit { get; set; }

        [DisplayName("配置名称")]
        public string ChartName { get; set; }

        [DisplayName("图表标题")]
        public string ChartTitle { get; set; }

        [DisplayName("副标题")]
        public string ChartSubTitle { get; set; }

        [DisplayName("启用缩放")]
        public bool IsZoom { get; set; }

        [DisplayName("图表类型")]
        public EChartType EChartType { get; set; }

        [DisplayName("Tip内容格式")]
        public string TipFormatter { get; set; }

        [DisplayName("地图名称")]
        public string MapName { get; set; }

        [DisplayName("地点字段")]
        public string PositionField { get; set; }

        [DisplayName("划分Series的字段名")]
        public string SeriesFieldNames { get; set; }

        //[DisplayName("各个Series的颜色")]
        //public string Color { get; set; }

        [DisplayName("Series划分类型")]
        public SeriesType SeriesType { get; set; }

        [DisplayName("高度")]
        public int Height { get; set; }

        [DisplayName("点击地图事件")]
        public string OnClickListener { get; set; }

        //[DisplayName("散点图的标记图形")]
        //public string Symbol { get; set; }

        //[DisplayName("散点图的标记大小")]
        //public string SymbolSize { get; set; }

        [DisplayName("chartOption构造完毕事件")]
        public string ChartOptionFinishFunction { get; set; }

        public override string Render()
        {
            Attributes["style"] = "height:{0}px;".FormatTo(Height);
            Attributes["ChartName"] = ChartName;
            Attributes["ChartTitle"] = ChartTitle;
            Attributes["ChartSubTitle"] = ChartSubTitle;
            Attributes["IsZoom"] = IsZoom.ToString().ToLower();
            Attributes["EChartType"] = EChartType.ToString();
            Attributes["TipFormatter"] = TipFormatter;
            Attributes["MapName"] = MapName;
            Attributes["PositionField"] = PositionField;
            Attributes["SeriesFieldNames"] = SeriesFieldNames;
            //Attributes["Color"] = Color;
            Attributes["SeriesType"] = SeriesType.ToString();
            Attributes["onClickListener"] = OnClickListener;
            //if(!Symbol.IsNullOrWhiteSpace())
            //    Attributes["Symbol"] = Symbol;
            //if(!SymbolSize.IsNullOrWhiteSpace())
            //    Attributes["SymbolSize"] = SymbolSize;
            if(!ChartOptionFinishFunction.IsNullOrWhiteSpace())
                Attributes["ChartOptionFinishFunction"] = ChartOptionFinishFunction;
            if(!ChartInit.IsNullOrWhiteSpace())
                Attributes["ChartInit"] = ChartInit;
            return base.Render();
        }

        public override void AddChildren(string property, object component)
        {
            if (property.Equals("DataSource", StringComparison.CurrentCultureIgnoreCase))
            {
                DataSource = component as IListDataSource;
            }
            base.AddChildren(property, component);
        }
    }

    public enum EChartType
    { 
        Map,
        Scatter,
        EffectScatter,
    }

    public enum SeriesType
    {
        [Description("按列名分Series")]
        ColumnNameAsSeries,
        [Description("按列值分Series")]
        ColumnValueAsSeries,
    }
}
