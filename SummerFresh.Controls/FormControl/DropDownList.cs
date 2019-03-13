using SummerFresh.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using System.Web.Mvc;
using System.ComponentModel;
using System.Web.Script.Serialization;
namespace SummerFresh.Controls
{

    /// <summary>
    /// 下拉框
    /// </summary>
    [DisplayName("下拉框")]
    public class DropDownList : FormControlBase, IKeyValueDataSourceControl, IChildren
    {

        public DropDownList():base()
        {
            AppendEmptyOption = true;
            EmptyOptionText = "==请选择==";
            Controls = new List<IControl>();
        }

        [FormField(Editable = false)]
        public IList<IControl> Controls
        {
            get;
            set;
        }

        protected override string TagName
        {
            get { return "select"; }
        }

        public string RelateControlID
        {
            get;
            set;
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

        internal override void AddAttributes()
        {
            if (!RelateControlID.IsNullOrEmpty())
            {
                Attributes["ChildrenControls"] = RelateControlID;
            }
            if (ChangeTiggerSearch)
            {
                Attributes["onchange"] = "$(this).closest('[searchForm]').submit();";
            }
            base.AddAttributes();
        }

        protected override string RenderInner()
        {
            if(DataSource==null)
            {
                throw new CustomException("DropDownList 需要 DataSource");
            }
            string optionTemplate = "<option value=\"{0}\" {1} >{2}</option>";
            StringBuilder content = new StringBuilder();
            IList<SelectListItem> items = DataSource.SelectItems();
            if(items!=null && AppendEmptyOption)
            {
                items.Insert(0, new SelectListItem() { Text = EmptyOptionText, Value = "", Selected = Value.IsNullOrEmpty() });
            }
            if(!Value.IsNullOrEmpty())
            {
                var selected = items.FirstOrDefault(o => o.Value.Equals(Value, StringComparison.CurrentCultureIgnoreCase));
                if (selected != null)
                {
                    selected.Selected = true;
                }
            }
            foreach(var item in items)
            {
                content.AppendLine(optionTemplate.FormatTo(item.Value, item.Selected ? "selected=\"selected\"" : "", item.Text));
            }
            return  content.ToString();
        }

        /// <summary>
        /// 数据源
        /// </summary>
        [DisplayName("数据源")]
        public IKeyValueDataSource DataSource
        {
            get;
            set;
        }

        public void AddChildren(string property, object component)
        {
            DataSource = component as IKeyValueDataSource;
            Controls.Add(component as IControl);
        }
    }
}
