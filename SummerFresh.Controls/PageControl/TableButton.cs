using SummerFresh.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SummerFresh.Basic;
using System.Globalization;
namespace SummerFresh.Controls
{
    public class TableButton : SummerFresh.Business.IComponent
    {
        public TableButton()
        {
            Visiable = true;
            Target = TableButtonTarget.Self;
        }
        public string ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string CssClass
        {
            get;
            set;
        }

        public string Href { get; set; }

        public TableButtonTarget Target { get; set; }

        public bool Visiable
        {
            get;
            set;
        }

        public int Rank { get; set; }


        [DisplayName("按钮点击事件")]
        [FormField(ControlType = ControlType.TextArea)]
        public string OnClick { get; set; }

        public TableButtonType TableButtonType { get; set; }

        [DisplayName("数据字段")]
        public string DataFields { get; set; }


        public IDictionary<string, object> RowData { get; set; }

        public string Render()
        {
            if (RowData.Keys.Contains(ID) && !RowData[ID].ToString().IsNullOrEmpty())
            {
                if (RowData[ID].ToString().Equals("0") || RowData[ID].ToString().Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    return string.Empty;
                }
            }
            var a = new TagBuilder("a");
            a.AddCssClass(CssClass);
            a.Attributes["trButton"] = ID;
            a.InnerHtml = RenderInner();
            if (!OnClick.IsNullOrEmpty())
            {
                a.Attributes.Add("onclick", FormatAttribute(OnClick));
            }
            if (!Href.IsNullOrEmpty())
            {
                a.Attributes.Add("href", FormatAttribute(Href));
            }
            else
            {
                a.Attributes.Add("href", "javascript:void(0);");
            }
            a.Attributes.Add("target", "_{0}".FormatTo(Target.ToString().ToLower()));
            return a.ToString();
        }

        public virtual string RenderInner()
        {
            return Name;
        }

        private string FormatAttribute(string formatString)
        {
            var buttonClick = formatString;
            if (!DataFields.IsNullOrEmpty())
            {
                var dataFields = DataFields.Split(',');
                object[] param = new object[dataFields.Length];
                for (int i = 0; i < dataFields.Length; i++)
                {
                    param[i] = RowData[dataFields[i]];
                }
                buttonClick = string.Format(CultureInfo.CurrentCulture, buttonClick, param);
            }
            return buttonClick;
        }
    }

}
