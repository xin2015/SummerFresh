using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SummerFresh.Business;
using SummerFresh.Data;
using SummerFresh.Basic;

namespace SummerFresh.Controls
{
    public static class CustomChartExtenstion
    {

        public static MvcHtmlString CustomChartV3(this HtmlHelper html, ChartConfig config, IDictionary<string, List<IDictionary<string, object>>> entitiesLst)
        {
            return CustomChartInnerV3(config, entitiesLst);
        }

        public static MvcHtmlString CustomChartInnerV3(ChartConfig config, IDictionary<string, List<IDictionary<string, object>>> entitiesLst)
        {
            TagBuilder chart = new TagBuilder("div");
            chart.GenerateId(config.ChartName);
            chart.Attributes["style"] = "style=\"min-width:800px;min-height:300px\"";
            TagBuilder script = new TagBuilder("script");
            TagBuilder theme = new TagBuilder("script");
            theme.Attributes.Add("type", "text/javascript");
            theme.Attributes.Add("src", string.Format("js/themes/{0}.js", config.ChartTheme));

            string jqReady = "$(function () {$(\"#" + config.ChartName + "\").highcharts( {Config});});";
            #region chartJs

            var chartJs = new
            {
                //width = "100%",
                //height = "100%",
                backgroundColor = ColorTranslator.ToHtml(config.BackgroundColor),

            };
            #endregion

            #region credits

            var creditsJs = new 
            {
                enabled=false,
            };

            #endregion

            #region titleJs
            var titleJs = new
            {
                align = ((ChartItemLocation)((byte)config.ChartTitleLocation & (byte)7)).ToString().ToLower(),
                verticalAlign = ((ChartItemLocation)((byte)config.ChartTitleLocation & (byte)56)).ToString().ToLower(),
                x = config.ChartTitlePosition.X,
                y = config.ChartTitlePosition.Y,
                text = config.ShowChartTtile ? config.ChartTitle : null,
            };
            #endregion

            #region subTitleJs
            var subTitleJs = new
            {
                align = ((ChartItemLocation)((byte)config.ChartSubTitleLocation & (byte)7)).ToString().ToLower(),
                verticalAlign = ((ChartItemLocation)((byte)config.ChartSubTitleLocation & (byte)56)).ToString().ToLower(),
                x = config.ChartSubTitltePosition.X,
                y = config.ChartSubTitltePosition.Y,
                text = config.ShowChartSubTitle ? config.ChartSubTitle : null,
            };
            #endregion

            #region legendJs
            var legendJs = new
            {
                align = ((ChartItemLocation)((byte)config.LegendLocation & (byte)7)).ToString().ToLower(),
                verticalAlign = ((ChartItemLocation)((byte)config.LegendLocation & (byte)56)).ToString().ToLower(),
                x = config.LegendPosition.X,
                y = config.LegendPosition.Y,
                enabled = config.ShowLegend,
                //labelFormat = config.LegendFormat,
                title = new
                {
                    text = config.LegendTitle,
                }
            };
            #endregion

            #region tooltipJs
            var tooltipJs = new
            {
                pointFormat = string.IsNullOrWhiteSpace( config.ToolTipPointFormatter)?" function(){ return \"<span style=\"color:{series.color}\">\u25CF</span> {series.name}: <b>{point.y}</b><br/>\" }":config.ToolTipPointFormatter,
                //formatter=
                enabled = config.ShowToolTip,
                crosshairs = new bool[] { config.ToolTipHorizontalHair, config.ToolTipVerticalHair },
            };
            #endregion


            #region xAxis、yAxis、series

            List<object> xAxisLst = new List<object>();
            List<object> yAxisLst = new List<object>();
            List<object> seriesLst = new List<object>();


            foreach (ChartXAxisConfig xAxis in config.XAxisLst)
            {
                if (!entitiesLst.ContainsKey(xAxis.DataSourceName))
                    throw new Exception("数据集名称不匹配");
                IList<IDictionary<string, object>> datas = entitiesLst[xAxis.DataSourceName];
                List<object> xDatas = new List<object>();
                object xData;
                foreach (Dictionary<string, object> item in datas)
                {
                    xData = xAxis.FieldViewFormatter == null ?
                            item[xAxis.FieldName] :
                            xAxis.FieldViewFormatter(item[xAxis.FieldName]);
                    if (xDatas.Contains(xData)) continue;
                    xDatas.Add( xData);
                }
                xAxisLst.Add(new
                {
                    categories = xDatas.ToArray(),
                    title = xAxis.AxisTitlt,
                });
            }

            foreach (ChartYAxisConfig yAxis in config.YAxisLst)
            {
                yAxisLst.Add(new
                {
                    title = new
                    {
                        text = yAxis.AxisTitle,
                    },
                    max = yAxis.Max == decimal.MinValue ? "undefine" : yAxis.Max.ToString(),
                    min = yAxis.Min == decimal.MinValue ? "undefine" : yAxis.Min.ToString(),
                });
            }

            List<ChartSeriesConfig> pieLst = config.SeriesConfig.Where(c => c.ChartType == ChartType.Pie).ToList();
            if (pieLst.Count > 0)
            {
                List<object> pieDatas = new List<object>();
                foreach (ChartSeriesConfig series in pieLst)
                {
                    if (!entitiesLst.ContainsKey(series.DataSourceName))
                        throw new Exception("数据集名称不匹配");
                    pieDatas.Add(new { name = series.SeriesName, x = entitiesLst[series.DataSourceName].FirstOrDefault()[series.DataFieldName] });
                }
                seriesLst.Add(new
                {
                    data = pieDatas.ToArray(),
                    type = ChartType.Pie.ToString().ToLower(),
                });
            }
            foreach (ChartSeriesConfig series in config.SeriesConfig)
            {
                if (series.ChartType == ChartType.Pie) continue;
                if (!entitiesLst.ContainsKey(series.DataSourceName))
                    throw new Exception("数据集名称不匹配");
                IList<IDictionary<string, object>> datas = entitiesLst[series.DataSourceName];
                ///Series里面单分组
                if (string.IsNullOrWhiteSpace(series.SeriesStackFileName))
                {
                    List<object> seriesData = new List<object>();
                    datas.ForEach(item =>
                    {
                        if (item[series.SeriesFieldName].ToString() == series.SeriesName)
                        {
                            seriesData.Add(series.DataValueFormatter == null ?
                                item[series.DataFieldName] :
                                series.DataValueFormatter(item[series.DataFieldName]));
                        }
                    });
                    //bool useSeriesStack=string.IsNullOrWhiteSpace( series.SeriesStackFileName);
                    seriesLst.Add(new
                    {
                        name = series.SeriesName,
                        data = seriesData.ToArray(),
                        type = series.ChartType.ToString().ToLower(),
                        color = series.SeriesColor == Color.Empty ? "undefine" : ColorTranslator.ToHtml(series.SeriesColor),
                        //SeriesStack = series.SeriesStackFileName == null ? "undefine" : entitiesLst[0][seriesStackAttr.FiledName],
                    });
                }
                ///Series里面多分组
                else
                {
                    Dictionary<object, IList<object>> grouppingData = new Dictionary<object, IList<object>>();
                    datas.ForEach(item =>
                    {
                        if (!item.ContainsKey(series.SeriesStackFileName))
                            throw new Exception("数据项缺少字段");
                        if (!grouppingData.ContainsKey(item[series.SeriesStackFileName]))
                            grouppingData[item[series.SeriesStackFileName]] = new List<object>();
                        grouppingData[item[series.SeriesStackFileName]].Add
                            (
                                series.DataValueFormatter == null ?
                                item[series.DataFieldName] :
                                series.DataValueFormatter(item[series.DataFieldName])
                            );

                    });
                    grouppingData.ForEach(kvp =>
                    {
                        seriesLst.Add(new
                        {
                            name = series.SeriesName,
                            data = kvp.Value.ToArray(),
                            type = series.ChartType.ToString().ToLower(),
                            color = series.SeriesColor == Color.Empty ? "undefine" : ColorTranslator.ToHtml(series.SeriesColor),
                            stack = series.SeriesStackValueFormatter == null ? kvp.Key : series.SeriesStackValueFormatter(kvp.Key),
                        });
                    });
                }
            }

            #endregion

            StringBuilder result = new StringBuilder();
            result.Append(chart.ToString());
            if (config.ChartTheme != ChartTheme.Default)
                result.Append(theme.ToString());
            var chartOption = new
            {
                chart = chartJs,
                title = titleJs,
                subTitle = subTitleJs,
                legend = legendJs,
                tooltip = tooltipJs,
                credits=creditsJs,
                xAxis = xAxisLst.ToArray(),
                yAxis = yAxisLst.ToArray(),
                series = seriesLst.ToArray(),
            };
            string chartOptionStr = Newtonsoft.Json.JsonConvert.SerializeObject(chartOption);
            chartOptionStr = chartOptionStr.Replace("\"undefine\"", "null");


            jqReady = jqReady.Replace("{Config}", chartOptionStr);
            script.InnerHtml = jqReady;
            //result.Append(chart.ToString());

            result.Append(script.ToString());
            return MvcHtmlString.Create(result.ToString());
        }
    }
}
