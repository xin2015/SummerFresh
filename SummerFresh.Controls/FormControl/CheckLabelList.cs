using SummerFresh.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SummerFresh.Basic;
using System.ComponentModel;

namespace SummerFresh.Controls
{
    ///这个可以看成 Segument
    public class CheckLabelList : FormControlBase, IKeyValueDataSourceControl, IChildren
    { 
        public CheckLabelList()
            : base()
        {
            AppendEmptyOption = true;
            EmptyOptionText = "全部";
            Controls = new List<IControl>();
            ChangeTiggerSearch = true;
        }

        [FormField(Editable = false)]
        public IList<IControl> Controls
        {
            get;
            set;
        }
        protected override string TagName
        {
            get { return "div"; }
        }

        /// <summary>
        /// 附加空选项
        /// </summary>
        [DisplayName("附加空选项")]
        public bool AppendEmptyOption { get; set; }

        /// <summary>
        /// 空选项文本
        /// </summary>
        [DisplayName("空选项文本")]
        public string EmptyOptionText { get; set; }

        public IKeyValueDataSource DataSource
        {
            get;
            set;
        }

        public LayoutType LayoutType { get; set; }

        public string OnLabelClick { get; set; }

        public void AddChildren(string property, object component)
        {
            DataSource = component as IKeyValueDataSource;
            Controls.Add(component as IControl);
        }

        internal override void AddAttributes()
        {
            Attributes.Remove("UserName");
            this.CssClass = " btn-group";
            Attributes["widget"] = "checklabel";
            if (!OnLabelClick.IsNullOrWhiteSpace())
                Attributes["onlabelclick"] = OnLabelClick;
            base.AddAttributes();
        }

        protected override string RenderInner()
        {
            if (DataSource == null)
            {
                throw new CustomException("CheckLabelList 需要 DataSource"); 
            }
            if (Name.IsNullOrWhiteSpace())
                Name = ID;
            StringBuilder result = new StringBuilder();
            IList<SelectListItem> items = DataSource.SelectItems();
            if (items != null && AppendEmptyOption)
            {
                items.Insert(0, new SelectListItem() { Text = EmptyOptionText, Value = "", Selected = Value.IsNullOrEmpty() });
            }
            string selected = string.Empty;
            for (int i = 0; i < items.Count; i++)
            {
                selected = items[i].Value == Value ? "active" : "";
                if (i == 0)
                {
                    result.AppendLine("<a href=\"#\" class=\"btn btn-default {3}\" value=\"{1}\">{0}<input name=\"{2}\" value=\"{4}\" type=\"hidden\"></a>".FormatTo(items[i].Text, items[i].Value, Name, selected, Value));
                }
                else
                {
                    result.AppendLine("<a href=\"#\" class=\"btn btn-default {2} \" value=\"{1}\">{0}</a>".FormatTo(items[i].Text, items[i].Value, selected));
                }
            }
            return result.ToString();
        }
    }

    //public enum LayoutType
    //{
    //    Block,
    //    Inline,
    //}
}
