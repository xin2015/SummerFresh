using SummerFresh.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

namespace SummerFresh.Controls
{

    public class NewChartSeries : SummerFresh.Business.IComponent, IChildren,ISerializeable
    {
        public NewChartSeries()
        { 
            SeriesExtPropLst = new List<NewChartOptionMember>();
            (this as IChildren).Controls=new List<IControl>();
        }

        #region 显式接口实现
        string SummerFresh.Business.IComponent.CssClass
        {
            get;
            set;
        }

        bool SummerFresh.Business.IComponent.Visiable
        {
            get;
            set;
        }

        IList<IControl> IChildren.Controls
        {
            get;
            set;
        }

        #endregion

        [DisplayName("显示模式")]
        public SeriesShowMode ShowMode
        {
            get;
            set;
        }

        public string Render()
        {
            //throw new NotImplementedException();
            return string.Empty;
        }


        [DisplayName("Series名称")]
        public string ID
        {
            get;
            set;
        }

        [DisplayName("Data数据格式")]
        public virtual string Fields { get; set; }

        [DisplayName("图表类型")]
        public virtual ChartType Type { get; set; }

        public string Color { get; set; }

        [DisplayName("y轴索引")]
        public string YAxis { get; set; }

        [DisplayName("排序")]
        public int Rank
        {
            get;
            set;
        }

        [DisplayName("X轴字段")]
        public string XField { get; set; }

        //[DisplayName("数据源")]
        //public IChartDataSource Datasource { get; set; }

        [DisplayName("数据源")]
        public IListDataSource Datasource { get; set; }

        [DisplayName("扩展属性")]
        public List<NewChartOptionMember> SeriesExtPropLst { get; set; }

        /// <summary>
        /// X轴索引
        /// </summary>
        [FormField(Editable = false)]
        public int? XAxis { get; set; }


        public virtual Dictionary<string, object> GetSeriesJson(IList<IDictionary<string, object>> datas)
        {
            if (Datasource != null)
            {
                datas = Datasource.GetList();
                this.Datas = datas;
            }
            List<object> seriesDataLst = new List<object>();
            string[] fieldArr = Fields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in datas)
            {
                if (fieldArr.Length < 1) continue;
                seriesDataLst.Add(GetDataItem(item, fieldArr));
            }

            Dictionary<string, object> series = new Dictionary<string, object>();

            series["name"] = ID;
            series["type"] = Type.ToString().ToLower();
            if (!string.IsNullOrEmpty(Color))
                series["color"] = Color;
            if (!string.IsNullOrEmpty(YAxis))
                series["yAxis"] = YAxis.ConverTo<int>();
            series["data"] = seriesDataLst.ToArray();
            if (XAxis.HasValue)
                series["xAxis"] = XAxis.Value;

            

            return series;
        }

        protected virtual object GetDataItem(IDictionary<string, object> chartDatas, string[] fieldArr)
        {
            if (fieldArr.Length < 1) return null;
            if (fieldArr.Length > 1)
            {
                object[] arr = new object[fieldArr.Length];
                for (int i = 0; i < fieldArr.Length; i++)
                {
                    arr[i] = chartDatas[fieldArr[i]];
                }
                return arr;
            }
            else
            {
                return chartDatas[fieldArr[0]];
            }
        }

        /// <summary>
        /// 添加扩展属性
        /// </summary>
        /// <param name="series"></param>
        public virtual void SetExtProp(Dictionary<string, object> series)
        {
            foreach (var extProp in SeriesExtPropLst)
            {
                string[] memberNameArr = extProp.MemberName.Split('.');
                Dictionary<string, object> parentObj = series;
                for (int i = 0; i < memberNameArr.Length; i++)
                {
                    if (!parentObj.ContainsKey(memberNameArr[i]))
                    {
                        if (i == memberNameArr.Length - 1)
                        {
                            parentObj[memberNameArr[i]] = null;
                        }
                        else
                        {
                            parentObj[memberNameArr[i]] = new Dictionary<string, object>();
                        }
                    }
                    if (i == memberNameArr.Length - 1)
                    {
                        parentObj[memberNameArr[i]] = new JavaScriptSerializer().Deserialize(extProp.MemberJson, typeof(object));
                    }
                    else
                    {
                        parentObj = parentObj[memberNameArr[i]] as Dictionary<string, object>;
                    }
                }
            }
        }

        public Tuple<string, NewChartXAxis> GetXAxisData(IList<IDictionary<string, object>> parentDatas, string parentXField)
        {
            if (XField.IsNullOrWhiteSpace()) return null;
            string dsKey;
            NewChartXAxis result = new NewChartXAxis();
            List<IDictionary<string, object>> currDatas = null;
            if (Datasource != null)
            {
                //currDatas = this.Datasource.GetChartDatas().ToList();
                currDatas = this.Datasource.GetList().ToList();
                dsKey = "{0}_{1}".FormatTo(Datasource.ID, XField);
            }
            else
            {
                if (XField.Equals(parentXField)) return null;
                dsKey = "{0}_{1}".FormatTo("parent", XField);
            }
            //object[] xValueArr = currDatas.Where(c => c.XValue != null).Select(c => c.XValue).Distinct().ToArray();
            List<object> xValueLst = new List<object>();
            if (currDatas != null)
                xValueLst = currDatas.Select(c => c[XField]).Distinct().ToList();
            else
            {
                object xValue;
                foreach (IDictionary<string, object> chartData in parentDatas)
                {
                    if (chartData.ContainsKey(XField))
                    {
                        xValue = chartData[XField];
                    }
                    else
                    {
                        xValue = null;
                    }
                    if (!xValueLst.Contains(xValue))
                        xValueLst.Add(xValue);
                }
            }
            if (xValueLst != null && xValueLst.Count > 0)
                result . Categories= xValueLst.ToArray();
            return new Tuple<string, NewChartXAxis>(dsKey, result);
        }

        public void AddChildren(string property, object component)
        {
            if (component is IListDataSource)
            {
                Datasource = component as IListDataSource;
            }
            if (component is NewChartOptionMember)
            {
                SeriesExtPropLst.Add(component as NewChartOptionMember);
            }
        }

        internal IList<IDictionary<string, object>> Datas { get; set; }

        public Dictionary<string, object> Serialize()
        {
            Dictionary<string,object> result= this.GetSeriesJson(Datas);
            this.SetExtProp(result);
            return result;
        }
    }

    /// <summary>
    /// 折线
    /// </summary>
    [DisplayName("折线图")]
    public class LineSeries : NewChartSeries
    {
        public LineSeries()
        {
            Type = ChartType.Line;
        }

        [FormField(Editable = false)]
        public override ChartType Type
        {
            get
            {
                return base.Type;
            }
            set
            {
                base.Type = value;
            }
        }
    }

    /// <summary>
    /// 区域
    /// </summary>
    [DisplayName("区域图")]
    public class AreaSeries : NewChartSeries
    {
        public AreaSeries()
        {
            this.Type = ChartType.Area;
        }

        [FormField(Editable = false)]
        public override ChartType Type
        {
            get
            {
                return base.Type;
            }
            set
            {
                base.Type = value;
            }
        }
    }

    /// <summary>
    /// 区域曲线
    /// </summary>
    [DisplayName("区域曲线图")]
    public class AreaSplineSeries : NewChartSeries
    {
        public AreaSplineSeries()
        {
            this.Type = ChartType.AreaSpline;
        }


        [FormField(Editable = false)]
        public override ChartType Type
        {
            get
            {
                return base.Type;
            }
            set
            {
                base.Type = value;
            }
        }
    }

    /// <summary>
    /// 曲线
    /// </summary>
    [DisplayName("曲线图")]
    public class SplineSeries : NewChartSeries
    {
        public SplineSeries()
        {
            this.Type = ChartType.Spline;
        }

        [FormField(Editable = false)]
        public override ChartType Type
        {
            get
            {
                return base.Type;
            }
            set
            {
                base.Type = value;
            }
        }
    }

    /// <summary>
    /// 柱状
    /// </summary>
    [DisplayName("柱状图")]
    public class ColumnSeries : NewChartSeries
    {
        public ColumnSeries()
        {
            this.Type = ChartType.Column;
        }

        [FormField(Editable = false)]
        public override ChartType Type
        {
            get
            {
                return base.Type;
            }
            set
            {
                base.Type = value;
            }
        }
        public string Stack { get; set; }

        public override Dictionary<string, object> GetSeriesJson(IList<IDictionary<string, object>> datas)
        {
            Dictionary<string, object> series = base.GetSeriesJson(datas);
            if (datas.Count > 0 && !Stack.IsNullOrWhiteSpace())
                series["stack"] = datas.First()[Stack];
            return series;
        }

    }

    /// <summary>
    /// 区域范围
    /// </summary>
    [DisplayName("区域范围图")]
    public class AreaRangeSeries : NewChartSeries
    {
        public AreaRangeSeries()
        {
            this.Type = ChartType.AreaRange;
        }

        [FormField(Editable = false)]
        public override ChartType Type
        {
            get
            {
                return base.Type;
            }
            set
            {
                base.Type = value;
            }
        }

        [FormField(Editable = false)]
        public override string Fields
        {
            get
            {
                return base.Fields;
            }
            set
            {
                base.Fields = value;
            }
        }

        public string XField { get; set; }

        public string YLowField { get; set; }

        public string YHighField { get; set; }

        public override Dictionary<string, object> GetSeriesJson(IList<IDictionary<string, object>> datas)
        {
            this.Fields = "{0},{1},{2}".FormatTo(XField, YLowField, YHighField);
            return base.GetSeriesJson(datas);
        }
    }

    /// <summary>
    /// 饼
    /// </summary>
    [DisplayName("饼图")]
    public class PieSeries : NewChartSeries,IComplexable
    {
        public PieSeries()
        {
            this.Type = ChartType.Pie;
        }

        [FormField(Editable = false)]
        public override ChartType Type
        {
            get
            {
                return base.Type;
            }
            set
            {
                base.Type = value;
            }
        }

        [FormField(Editable = false)]
        public override string Fields
        {
            get
            {
                return base.Fields;
            }
            set
            {
                base.Fields = value;
            }
        }

        public string NameField { get; set; }

        public string YField { get; set; }

        public override Dictionary<string, object> GetSeriesJson(IList<IDictionary<string, object>> datas)
        {
            this.Fields = "{0},{1}".FormatTo(NameField, YField);

            if (Datasource != null)
                datas = Datasource.GetList();
            List<object> seriesDataLst = new List<object>();
            string[] fieldArr = Fields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            object dataItem;
            foreach (var item in datas)
            {
                if (fieldArr.Length < 1) continue;
                dataItem = GetDataItem(item, fieldArr);
                if (dataItem != null)
                {
                    if (!string.IsNullOrEmpty(Color))
                        (dataItem as Dictionary<string, object>)["color"] = Color;
                    //seriesDataLst.Add(GetDataItem(item, fieldArr));
                    seriesDataLst.Add(dataItem);
                }
            }

            Dictionary<string, object> series = new Dictionary<string, object>();

            //series["name"] = ID;
            //series["type"] = Type.ToString().ToLower();
            //if (!string.IsNullOrEmpty(Color))
            //    series["color"] = Color;
            //if (!string.IsNullOrEmpty(YAxis))
            //    series["yAxis"] = YAxis.ConverTo<int>();
            series["data"] = seriesDataLst.ToArray();


            return series;
        }

        protected override object GetDataItem(IDictionary<string, object> chartDatas, string[] fieldArr)
        {
            string name = NameField;
            if (Regex.IsMatch(name, "'.*?'"))
            {
                name = name.Trim('\'');
                foreach (var dataCol in chartDatas)
                {
                    if (name.Equals(dataCol.Value) && chartDatas.ContainsKey(YField))
                    {
                        return new Dictionary<string, object>() { { "name", name }, { "y", chartDatas[YField] } };
                    }
                }
                return null;
            }
            else
            {
                return base.GetDataItem(chartDatas, fieldArr);
            }
        }

        public Dictionary<string, object> Combine(System.Collections.IEnumerable list)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result["name"] = this.ID + "pie";
            result["type"] = "pie";
            List<object> datas = new List<object>();
            foreach (var item in list)
            {
                datas.AddRange((item as Dictionary<string,object>)["data"] as object[]);
            }
            if (datas.Count > 0)
                result["data"] = datas.ToArray();
            return result;
        }
    }

    /// <summary>
    /// 条带
    /// </summary>
    [DisplayName("条带图")]
    public class BarSeries : NewChartSeries
    {
        public BarSeries()
        {
            this.Type = ChartType.Bar;
        }

        [FormField(Editable = false)]
        public override ChartType Type
        {
            get
            {
                return base.Type;
            }
            set
            {
                base.Type = value;
            }
        }
        public string Stack { get; set; }

        public override Dictionary<string, object> GetSeriesJson(IList<IDictionary<string, object>> datas)
        {
            Dictionary<string, object> series = base.GetSeriesJson(datas);
            if (datas.Count > 0 && !Stack.IsNullOrWhiteSpace())
                series["stack"] = datas.First()[Stack];
            return series;
        }
    }

    /// <summary>
    /// 散点
    /// </summary>
    [DisplayName("散点图")]
    public class ScatterSeries : NewChartSeries
    {
        public ScatterSeries()
        {
            this.Type = ChartType.Scatter;
        }

        [FormField(Editable = false)]
        public override ChartType Type
        {
            get
            {
                return base.Type;
            }
            set
            {
                base.Type = value;
            }
        }

        [FormField(Editable = false)]
        public override string Fields
        {
            get
            {
                return base.Fields;
            }
            set
            {
                base.Fields = value;
            }
        }

        public string XField { get; set; }

        public string YField { get; set; }

        protected override object GetDataItem(IDictionary<string, object> chartDatas, string[] fieldArr)
        {
            this.Fields = "{0},{1}".FormatTo(XField, YField);
            return base.GetDataItem(chartDatas, fieldArr);
        }
    }

    /// <summary>
    /// 气泡
    /// </summary>
    [DisplayName("气泡图")]
    public class BubbleSeries : NewChartSeries
    {
        public BubbleSeries()
        {
            this.Type = ChartType.Bubble;
        }

        [FormField(Editable = false)]
        public override ChartType Type
        {
            get
            {
                return base.Type;
            }
            set
            {
                base.Type = value;
            }
        }

        [FormField(Editable = false)]
        public override string Fields
        {
            get
            {
                return base.Fields;
            }
            set
            {
                base.Fields = value;
            }
        }

        public string XField { get; set; }

        public string YField { get; set; }

        public string SizeField { get; set; }

        public override Dictionary<string, object> GetSeriesJson(IList<IDictionary<string, object>> datas)
        {
            this.Fields = "{0},{1},{2}".FormatTo(XField, YField, SizeField);
            return base.GetSeriesJson(datas);
        }

    }

    /// <summary>
    /// 箱线
    /// </summary>
    [DisplayName("箱线图")]
    public class BoxplotSeries : NewChartSeries
    {
        public BoxplotSeries()
        {
            this.Type = ChartType.Boxplot;
        }

        [FormField(Editable = false)]
        public override ChartType Type
        {
            get
            {
                return base.Type;
            }
            set
            {
                base.Type = value;
            }
        }

        [FormField(Editable = false)]
        public override string Fields
        {
            get
            {
                return base.Fields;
            }
            set
            {
                base.Fields = value;
            }
        }

        public string Field1 { get; set; }

        public string Field2 { get; set; }

        public string Field3 { get; set; }

        public string Field4 { get; set; }

        public string Field5 { get; set; }

        public override Dictionary<string, object> GetSeriesJson(IList<IDictionary<string, object>> datas)
        {
            this.Fields = "{0},{1},{2},{3},{4}".FormatTo(Field1, Field2, Field3, Field4, Field5);
            return base.GetSeriesJson(datas);
        }
    }

    /// <summary>
    /// 错误
    /// </summary>
    [DisplayName("错误图")]
    public class ErrorbarSeries : NewChartSeries
    {
        public ErrorbarSeries()
        {
            this.Type = ChartType.Errorbar;
        }

        [FormField(Editable = false)]
        public override ChartType Type
        {
            get
            {
                return base.Type;
            }
            set
            {
                base.Type = value;
            }
        }

        [FormField(Editable = false)]
        public override string Fields
        {
            get
            {
                return base.Fields;
            }
            set
            {
                base.Fields = value;
            }
        }

        public string YLowField { get; set; }

        public string YHighField { get; set; }

        public override Dictionary<string, object> GetSeriesJson(IList<IDictionary<string, object>> datas)
        {
            this.Fields = "{0},{1}".FormatTo(YLowField, YHighField);
            return base.GetSeriesJson(datas);
        }
    }


}
