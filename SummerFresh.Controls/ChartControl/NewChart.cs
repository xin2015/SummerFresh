using SummerFresh.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SummerFresh.Basic;
using System.Text.RegularExpressions;
using SummerFresh.Basic.FastReflection;
using System.Collections;

namespace SummerFresh.Controls
{
    [DisplayName("新图表控件")]
    public class NewChart : NoPagerListControlBase, IChildren,ICloneable
    {
        private static readonly string FIELD_REGEX = "{\\w+}";

        public NewChart()
        {
            Height = 400;
            Series = new List<NewChartSeries>();
            ChartOptionMembers = new List<NewChartOptionMember>();
            YAxis = new List<NewChartYAxis>();
            XAxis = new List<NewChartXAxis>();
            NoDataTip = "暂无数据";
            TipColor = "#f00";
        }

        [ChartOption("chart_renderTo")]
        [FormField(Editable=false)]
        public string RenderTo { get; set; }

        [DisplayName("图表独立初始化方法")]
        [Description("使用此方法则不依靠控件自动组织ChartOption机制，独立自主构造ChartOption，独立建立HighCharts")]
        public string ChartInit { get; set; }

        [DisplayName("图表标题")]
        [ChartOption("title_text")]
        public string Title { get; set; }

        [DisplayName("图表子标题")]
        [ChartOption("subtitle_text")]
        public string SubTitle { get; set; }

        [DisplayName("高度")]
        public int Height { get; set; }

        [ChartOption]
        [DisplayName("分组字段")]
        public string GroupBy { get; set; }

        [DisplayName("X轴字段")]
        public string XField { get; set; }

        [DisplayName("默认图表类型")]
        public ChartType ChartType { get; set; }

        [DisplayName("自动生成Series")]
        public bool AutoGenerateSeries { get; set; }

        [DisplayName("自动生成方式")]
        [Description("排除数据源中某些字段自动生成的Series，或仅使用指定字段自动生成Series")]
        public SeriesHandleType SeriesHandleType { get; set; }

        [DisplayName("参与字段")]
        [Description("自动生成Series操作中，参与到排除或包含的所有字段名")]
        public string SeriesNames { get; set; }

        [ChartOption("lang_noData")]
        [DisplayName("无数据提示语")]
        [Description("需要引入no-data-to-display.js才会生效")]
        public string NoDataTip { get; set; }

        [ChartOption("noData_style_color")]
        [DisplayName("提示语颜色")]
        [Description("无数据时提示语的颜色")]
        public string TipColor { get; set; }

        #region 事件

        [DisplayName("Series构造完成事件")]
        public string OnSeriesFinish { get; set; }

        [DisplayName("Load事件")]
        [Description("HighCharts的Load事件")]
        public string OnLoad { get; set; }

        [DisplayName("chartOption构造完成事件")]
        public string OnChartOptionFinish { get; set; }

        [DisplayName("数据项绑定事件")]
        public string OnDataItemBind { get; set; }

        #endregion

        #region 子项集合

        [ChartOption]
        public List<NewChartSeries> Series { get; set; }

        [DisplayName("Y轴")]
        [ChartOption("yAxis")]
        public List<NewChartYAxis> YAxis { get; set; }

        [DisplayName("X轴")]
        [ChartOption("xAxis")]
        [FormField(Editable=false)]
        public List<NewChartXAxis> XAxis { get; set; }

        [DisplayName("扩展属性")]
        public List<NewChartOptionMember> ChartOptionMembers { get; set; }


        #endregion

        /// <summary>
        /// 商标 默认隐藏
        /// </summary>
        [ChartOption("credits_enabled")]
        [FormField(Editable = false)]
        public bool Credits { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        private object Datas;

        //[DisplayName("数据源")]
        //public IChartDataSource ChartDatasource { get; set; }

        public override string Render()
        {
            if (!Visiable)
                return string.Empty;
            Attributes["height"] = Height.ToString();
            Attributes["style"] = "height:auto;";
            Attributes["groupby"] = GroupBy;
            if(!this.ChartInit.IsNullOrWhiteSpace())
                Attributes["ChartInit"] = this.ChartInit;
            if (!this.OnChartOptionFinish.IsNullOrWhiteSpace())
                Attributes["OnChartOptionFinish"] = this.OnChartOptionFinish;
            if (!this.OnDataItemBind.IsNullOrWhiteSpace())
                Attributes["OnDataItemBind"] = this.OnDataItemBind;
            if (!this.OnLoad.IsNullOrWhiteSpace())
                Attributes["OnLoad"] = this.OnLoad;
            if (!this.OnSeriesFinish.IsNullOrWhiteSpace())
                Attributes["OnSeriesFinish"] = this.OnSeriesFinish;
            return base.Render();
        }

        public IList<IDictionary<string,object>> CreateChartOption(List<IDictionary<string, object>> datas=null)
        {
            if (datas == null)
            {
                var _data = this.GetData();
                datas = _data == null ? new List<IDictionary<string, object>>() : _data.ToList();
                
               if (GroupBy.IsNullOrEmpty())
               {
                   NewChart groupingChart = this.Clone() as NewChart;
                   groupingChart.RenderTo ="default"+ groupingChart.ID;
                   return groupingChart.CreateChartOption(datas);
               }
               else
               {
                   var groupList= datas.GroupBy(c => c[GroupBy]);
                   NewChart groupChart;
                   List<IDictionary<string, object>> result = new List<IDictionary<string, object>>();
                   foreach (var groupItem in groupList)
                   {
                       groupChart = this.Clone() as NewChart;
                       groupChart.RenderTo = groupItem.Key + groupChart.ID;
                       result.AddRange(groupChart.CreateChartOption(groupItem.ToList()));
                   }
                   return result;
               }
            }
            else
            {
                this.Datas = datas;
                ///Series
                if (this.AutoGenerateSeries)
                {
                    this.GenerateSeries(datas);
                }
                this.Series.ForEach(series => { series.Datas = datas; });
                ///X轴
                GenerateXAxis(datas);

                return new List<IDictionary<string, object>>() { Serialize() };
            }

        }

        /// <summary>
        /// 自动生成Series
        /// </summary>
        /// <param name="datas"></param>
        private void GenerateSeries(List<IDictionary<string, object>> datas)
        {

            List<string> seriesNameLst = new List<string>();
            if (!datas.IsNullOrEmpty())
            {
               seriesNameLst.AddRange( datas.FirstOrDefault().Keys.ToList());//现有
            }
            List<string> exceptLst = new List<string>() { XField };//排除
            exceptLst.AddRange(Series.Select(c => c.ID));
            if (!GroupBy.IsNullOrWhiteSpace())
                exceptLst.Add(GroupBy);
            if (SeriesHandleType == SummerFresh.Controls.SeriesHandleType.Except)
                exceptLst.AddRange(SeriesNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            else if (!SeriesNames.IsNullOrWhiteSpace())
            {
                seriesNameLst.Clear();
                seriesNameLst.AddRange(SeriesNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }
            seriesNameLst.ForEach(seriesName =>
            {
                if (exceptLst.Contains(seriesName)) return;
                Series.Add(new NewChartSeries() {
                    ID =seriesName,
                    Fields = seriesName,
                    Type = this.ChartType,
                });
            });
        }

        /// <summary>
        /// 生成X轴
        /// </summary>
        /// <param name="datas"></param>
        private void GenerateXAxis(List<IDictionary<string, object>> datas)
        {
            List<string> xAxisNameLst = new List<string>();
            if (!XField.IsNullOrWhiteSpace())
            {
                object[] xValueArr = datas.Select(c => c[XField]).Distinct().ToArray();
                if (xValueArr != null && xValueArr.Length > 0)
                {
                    xAxisNameLst.Add("parent_{0}".FormatTo(XField));
                    this.XAxis.Add(new NewChartXAxis(){ Categories= xValueArr } );
                }
            }
            Tuple<string, NewChartXAxis> tmpXAxis;
            foreach (NewChartSeries series in this.Series)
            {
                tmpXAxis = series.GetXAxisData(datas, XField);
                if (tmpXAxis != null)
                {
                    if (!xAxisNameLst.Contains(tmpXAxis.Item1))
                    {
                        series.XAxis = XAxis.Count;
                        xAxisNameLst.Add(tmpXAxis.Item1);
                        XAxis.Add( tmpXAxis.Item2 );
                    }
                    else
                    {
                        series.XAxis = xAxisNameLst.IndexOf(tmpXAxis.Item1);
                    }
                }
            }
        }


        public Dictionary<string, object> Serialize()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            this.Convert();
            FastProperty[] propArr = FastType.Get(this.GetType()).Getters;
            ChartOptionAttribute attr;
            string propName;
            foreach (var propInfo in propArr)
            {
                attr = propInfo.Info.GetCustomAttribute<ChartOptionAttribute>(true);
                if (attr == null ) continue;
                propName = attr.Name.IsNullOrWhiteSpace() ? propInfo.Name.ToLower() : attr.Name;
                if (propInfo.Type.IsGenericType)
                {
                    List<IDictionary<string, object>> jsonList = new List<IDictionary<string, object>>();
                    IEnumerable serializeEnumer = propInfo.GetValue(this) as IEnumerable;
                    Dictionary<Type,List<Dictionary<string,object>>> complexDic=new Dictionary<Type,List<Dictionary<string,object>>>();
                    foreach (var item in serializeEnumer)
                    {
                        if (item is IComplexable)
                        {
                            if(!complexDic.ContainsKey(item.GetType()))
                                complexDic[item.GetType()]=new List<Dictionary<string,object>>();
                            complexDic[item.GetType()].Add((item as ISerializeable).Serialize());
                        }
                        else
                            jsonList.Add((item as ISerializeable).Serialize());
                    }
                    if (complexDic.Count > 0)
                    {
                        foreach (var kvp in complexDic)
                        {
                            jsonList.Add((Activator.CreateInstance(kvp.Key) as IComplexable).Combine(kvp.Value));
                        }
                    }
                    if(jsonList.Count>0)
                        SetPropValue(result, propName.Split('_').ToList(), jsonList);
                }
                else
                    SetPropValue(result, propName.Split('_').ToList(), propInfo.GetValue(this));
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            foreach (NewChartOptionMember item in ChartOptionMembers)
            {
                SetPropValue(result, item.MemberName.Split('.').ToList(), serializer.Deserialize<object>(item.MemberJson));
            }

            Dictionary<string, object> datasource = new Dictionary<string, object>();
            if (this.Datas != null) datasource["default"] = this.Datas;
            foreach (NewChartSeries series in this.Series)
            {
                if (series.Datasource != null)
                    datasource[series.Datasource.ID] = series.Datas;
            }
            result["datasource"] = datasource;
            return result;
        }

        private void SetPropValue(Dictionary<string, object> parentInstance, List<string> propLst, object value)
        {
            if (parentInstance == null || propLst == null) return;
            for (int i = 0; i < propLst.Count; i++)
            {
                if (i == propLst.Count - 1)
                    parentInstance[propLst[i]] = value;//.GetValue(this);
                else
                {
                    string realKey = Regex.Replace(propLst[i], @".*?\[\d+\]", "");
                    bool keyIsArray = Regex.IsMatch(propLst[i], @".*?\[\d+\]");
                    if (keyIsArray ||
                        (parentInstance.ContainsKey(realKey) &&
                         parentInstance[realKey] is List<IDictionary<string, object>>))
                    {
                        if (!parentInstance.ContainsKey(realKey))
                            parentInstance[realKey] = new List<IDictionary<string, object>>();
                        ///成员 list ，key 单
                        ///成员 list ，key []
                        ///成员 dic  , key []

                        if (parentInstance[realKey] is List<IDictionary<string, object>> && !keyIsArray)
                        {
                            List<string> propSubLst = new List<string>();
                            for (int propIndex = i + 1; propIndex < propLst.Count; propIndex++)
                            {
                                propSubLst.Add(propLst[propIndex]);
                            }
                            List<IDictionary<string, object>> tempList =
                                parentInstance[realKey] as List<IDictionary<string, object>>;
                            foreach (var item in tempList)
                            {
                                SetPropValue(item as Dictionary<string, object>, propSubLst, value);
                            }
                        }
                        else if (parentInstance[realKey] is List<IDictionary<string, object>> && keyIsArray)
                        {
                            int listIndex = System.Convert.ToInt32(Regex.Match(propLst[i], @"(?<=*?\[)\d+(?=\])").Value);
                            parentInstance = (parentInstance[realKey] as List<Dictionary<string, object>>)[listIndex];
                        }
                        else if (parentInstance[realKey] is Dictionary<string, object> && keyIsArray)
                        {
                            int listIndex = System.Convert.ToInt32(Regex.Match(propLst[i], @"(?<=*?\[)\d+(?=\])").Value);
                            List<Dictionary<string, object>> tempList = new List<Dictionary<string, object>>();
                            tempList.Add(parentInstance[realKey] as Dictionary<string, object>);
                            while (listIndex < tempList.Count - 1)
                            {
                                tempList.Add(new Dictionary<string, object>());
                            }
                            parentInstance =
                                (parentInstance[realKey] as List<IDictionary<string, object>>)[listIndex] as
                                    Dictionary<string, object>;
                        }
                    }
                    else
                    {
                        if (!parentInstance.ContainsKey(propLst[i]))
                            parentInstance[propLst[i]] = new Dictionary<string, object>();
                        parentInstance = parentInstance[propLst[i]] as Dictionary<string, object>;
                    }
                }
            }
        }

        private void Convert()
        {
            if (this.DataSource != null)
            {
                this.Title = this.TryConvert(Title).ToString();
                this.SubTitle = this.TryConvert(SubTitle).ToString();
                foreach (NewChartOptionMember item in ChartOptionMembers)
                {
                    item.MemberJson = this.TryConvert( item.MemberJson).ToString();
                }
            }
        }

        private string TryConvert(string propValue)
        {
            string result=propValue;
            if (this.DataSource != null)
            {
                try
                {

                    if(Regex.IsMatch(propValue,FIELD_REGEX))
                    {
                        MatchCollection mc = Regex.Matches(propValue, FIELD_REGEX);
                        for (int i = 0; i < mc.Count; i++)
                        { 
                            result= result.Replace(mc[i].Value, (this.Datas as List<IDictionary<string,object>>).FirstOrDefault()[mc[i].Value.Trim('{').Trim('}')].ToString()   );
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                }
            }
            return result;
        }

        public override void AddChildren(string property, object component)
        {
            base.AddChildren(property, component);
            if (component is IListDataSource)
            {
                this.DataSource = component as IListDataSource;
            }
            else if (component is NewChartSeries)
            {
                this.Series.Add(component as NewChartSeries);
            }
            else if (component is NewChartOptionMember)
            {
                this.ChartOptionMembers.Add(component as NewChartOptionMember);
            }
            else if (component is NewChartYAxis)
            {
                this.YAxis.Add(component as NewChartYAxis);
            }
        }
    }

    public enum SeriesShowMode
    {
        [Description("总是显示")]
        AlwayShow,
        [Description("总是隐藏")]
        AlwayHidden,
        [Description("无数时隐藏")]
        NoneDataHidden
    }

}
