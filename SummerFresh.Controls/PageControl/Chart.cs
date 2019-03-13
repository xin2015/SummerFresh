using SummerFresh.Business;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using System.ComponentModel;

namespace SummerFresh.Controls
{
    [DisplayName("图表组件")]
    public class Chart : NoPagerListControlBase
    {
        public Chart()
        { 
            Theme = "default";
            ChartType = "line";
            ChartName = "default";
            Height = 400;
            XAxisTickInterval = 1;
            SeriesNames = "";
        }
        [DisplayName("图表独立初始化方法")]
        public string ChartInit { get; set; }

        [DisplayName("图表配置名称")]
        public string ChartName { get; set; }

        [DisplayName("主题")]
        public string Theme { get; set; }

        [DisplayName("图表类型")]
        public string ChartType { get; set; }

        [DisplayName("图表标题")]
        public string Title { get; set; }

        [DisplayName("图表子标题")]
        public string SubTitle { get; set; }

        [DisplayName("字段/坐标轴映射")]
        public ChartFieldMappingType ChartFieldMappingType { get; set; }

        [DisplayName("X轴坐标字段名")]
        public string XAxisName { get; set; }

        public string GroupBy { get; set; }

        public int Height { get; set; }

        [DisplayName("X轴刻度线单位间隔")]
        public int XAxisTickInterval { get; set; }

        [DisplayName("Series序列操作类型")]
        public SeriesHandleType SeriesHandleType { get; set; }

        [DisplayName("Series序列名称")]
        public string SeriesNames { get; set; }

        [DisplayName("各个Series的颜色")]
        public string Colors { get; set; }

        [DisplayName("各Series对应坐标轴索引")]
        public string YAxisIndexs { get; set; }

        [DisplayName("单项Data生成方法")]
        public string DataItemBuildFunction { get; set; }

        [DisplayName("HighChartd的Load事件")]
        public string ChartLoadFunction { get; set; }

        [DisplayName("chartOption构造完毕事件")]
        public string ChartOptionFinishFunction { get; set; }


        public override string Render()
        {
            //Attributes["style"] = "height:{0}px;".FormatTo(Height);
            Attributes["height"] = Height.ToString();
            Attributes["style"] = "height:auto;";
            Attributes["ChartType"] = ChartType.ToLower();
            Attributes["ChartName"] = ChartName;
            Attributes["ChartTitle"] = Title;
            Attributes["SubTitle"] = SubTitle;
            Attributes["XAxisName"] = XAxisName;
            Attributes["groupBy"] = GroupBy;
            Attributes["XAxisTickInterval"] = XAxisTickInterval.ToString();
            Attributes["ChartFieldMappingType"] = ChartFieldMappingType.ToString();
            Attributes["SeriesNames"] =SeriesNames.IsNullOrWhiteSpace()?"": ",{0},".FormatTo( SeriesNames.Trim(','));
            Attributes["SeriesHandleType"] = SeriesHandleType.ToString();
            if(!Colors.IsNullOrWhiteSpace())
                Attributes["Colors"] = Colors;
            if(!YAxisIndexs.IsNullOrWhiteSpace())
                Attributes["YAxisIndexs"] = YAxisIndexs;
            if(!DataItemBuildFunction.IsNullOrWhiteSpace())
                Attributes["DataItemBuildFunction"] = DataItemBuildFunction;
            if (!ChartLoadFunction.IsNullOrWhiteSpace())
                Attributes["ChartLoadFunction"] = ChartLoadFunction;
            if (!ChartOptionFinishFunction.IsNullOrWhiteSpace())
                Attributes["ChartOptionFinishFunction"] = ChartOptionFinishFunction;
            if (!ChartInit.IsNullOrWhiteSpace())
                Attributes["ChartInit"] = ChartInit;
            return base.Render();
        }

        public override string RenderContent()
        {
            if (ChartName.IsNullOrEmpty() && DataSource == null)
            {
                throw new CustomException("ChartName或DataSource必须设置一项");
            }
            return base.RenderContent();
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

    public enum ChartFieldMappingType
    {
        [Description("列名作为Series")]//行作为Y轴//行作为X轴，所以行作为Y轴
        RowAsY,
        [Description("行名作为Series")]//行作为X轴
        RowAsX
    }

    public enum SeriesHandleType
    {
        [Description("包含")]
        Contain,
        [Description("排除")]
        Except,
    }

    public enum ChartType
    {
        Line,
        Area,
        AreaRange,
        AreaSpline,
        Column,
        Scatter,
        Bubble,
        Pie,
        Bar,
        Spline,
        AreaSplineRange,
        ColumnRange,
        Boxplot,
        Errorbar,
    }
}
