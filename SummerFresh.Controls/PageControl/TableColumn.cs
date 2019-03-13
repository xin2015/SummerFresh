using SummerFresh.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SummerFresh.Basic;
using System.Web;
using System.Globalization;
namespace SummerFresh.Controls
{
    /// <summary>
    /// 表格列
    /// </summary>
    [DisplayName("表格列")]
    public class TableColumn : SummerFresh.Business.IComponent, ICloneable, IChildren
    {
        public TableColumn()
        {
            Children = new List<TableColumn>();
            TextAlign = CellTextAlign.Center;
            HtmlEncode = false;
            Visiable = true;
            ID = "column";
            Controls = new List<IControl>();
            ShowLength = 0;
        }

        [FormField(Editable = false)]
        public IList<IControl> Controls
        {
            get;
            set;
        }

        [Validator("required")]
        public string ID
        {
            get;
            set;
        }

        public string CssClass
        {
            get;
            set;
        }

        public bool Visiable
        {
            get;
            set;
        }

        public int Rank { get; set; }

        [Validator("required")]
        public string FieldName { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        [DisplayName("列名")]
        public string ColumnName { get; set; }

        /// <summary>
        /// 显示字符长度
        /// </summary>
        [DisplayName("显示字符长度")]
        public int ShowLength { get; set; }

        /// <summary>
        /// 列宽
        /// </summary>
        [DisplayName("列宽")]
        public string ColumnWidth { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        [DisplayName("默认值")]
        public string DefaultValue { get; set; }

        /// <summary>
        /// 列转换器
        /// </summary>
        [DisplayName("列转换器")]
        public IFieldConverter ColumnConverter { get; set; }


        [FormField(Editable = false)]
        public int Colspan { get; set; }

        /// <summary>
        /// 水平对齐方式
        /// </summary>
        [DisplayName("水平对齐方式")]
        public CellTextAlign TextAlign { get; set; }

        /// <summary>
        /// 输出格式
        /// </summary>
        [DisplayName("输出格式")]
        public string DataFormatString { get; set; }

        /// <summary>
        /// 输出字段
        /// </summary>
        [DisplayName("输出字段")]
        public string DataFormatFields { get; set; }

        /// <summary>
        /// 是否HTML编码输出
        /// </summary>
        [DisplayName("是否HTML编码输出")]
        public bool HtmlEncode { get; set; }

        public string ReferenceField { get; set; }

        /// <summary>
        /// 是否排序列
        /// </summary>
        [Description("是否排序列")]
        public bool Sortable { get; set; }

        /// <summary>
        /// 是否主键列
        /// </summary>
        [DisplayName("是否主健列")]
        public bool IsKey { get; set; }

        /// <summary>
        /// 子列
        /// </summary>
        [DisplayName("子列")]
        public IList<TableColumn> Children { get; set; }

        public void AddChildren(string property, object component)
        {
            if (property.Equals("Children"))
            {
                Children.Add(component as TableColumn);
            }
            if (property.Equals("ColumnConverter"))
            {
                ColumnConverter = component as IFieldConverter;
            }
            Controls.Add(component as IControl);
        }

        public IDictionary<string, object> RowData { get; set; }

        public string RenderHeader(int headerDeep)
        {
            var th = new TagBuilder("th");
            if (!ColumnWidth.IsNullOrEmpty())
            {
                th.Attributes.Add("style", "width:" + ColumnWidth);
            }
            if (Sortable)
            {
                th.InnerHtml = "<a href=\"javascript:void(0);\" sortField=\"" + FieldName + "\" >" + ColumnName + "</a>";
            }
            else
            {
                th.InnerHtml = ColumnName;
            }
            if (Children.IsNullOrEmpty())
            {
                th.Attributes.Add("rowspan", headerDeep.ToString());
            }
            else
            {
                th.Attributes.Add("colspan", Colspan.ToString());
            }
            return th.ToString();
        }

        public virtual object GetValue()
        {
            if (!RowData.Keys.Contains(FieldName) || RowData[FieldName] == null || RowData[FieldName].ToString().IsNullOrEmpty())
            {
                RowData[FieldName] = DefaultValue.IsNullOrEmpty() ? string.Empty : DefaultValue;
            }
            object result = RowData[FieldName];
            if (ColumnConverter != null)
            {
                result = ColumnConverter.Converter(FieldName, result, RowData);
            }
            else
            {
                if (!DataFormatString.IsNullOrEmpty() && !HtmlEncode)
                {
                    if (!DataFormatFields.IsNullOrEmpty())
                    {
                        var dataFields = DataFormatFields.Split(',');
                        object[] param = new object[dataFields.Length];
                        for (int i = 0; i < dataFields.Length; i++)
                        {
                            param[i] = RowData[dataFields[i]];
                        }
                        result = string.Format(CultureInfo.CurrentCulture, DataFormatString, param);
                    }
                    else
                    {
                        result = string.Format(CultureInfo.CurrentCulture, DataFormatString, new object[] { result });
                    }
                }
            }
            return result;
        }

        public virtual string Render()
        {
            var td = new TagBuilder("td");
            var returnValue = GetValue();
            string innerHtml = string.Empty;
            if (returnValue != null)
            {
                if (returnValue is CustomTd)
                {
                    var temp = returnValue as CustomTd;
                    foreach (var a in temp.Attribute)
                    {
                        td.Attributes.Add(a.Key, a.Value);
                    }
                    innerHtml = temp.Value.ToString();
                }
                else
                {
                    innerHtml = returnValue.ToString();
                }
            }
            if (ShowLength != 0 && innerHtml.Length > ShowLength)
            {
                td.Attributes.Add("title", innerHtml);
                innerHtml = innerHtml.Substring(ShowLength, "..");
            }
            td.InnerHtml = HtmlEncode ? HttpUtility.HtmlEncode(innerHtml) : innerHtml;
            if (TextAlign != CellTextAlign.Center)
            {
                td.Attributes.Add("align",TextAlign.ToString().ToLower());
            }
            if (!ColumnWidth.IsNullOrEmpty())
            {
                td.Attributes.Add("width", ColumnWidth);
            }
            return td.ToString();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public interface IAutoGenerateColumn
    {
        void GenerateColumn(IList<TableColumn> Columns, string[] keys);
    }
}
