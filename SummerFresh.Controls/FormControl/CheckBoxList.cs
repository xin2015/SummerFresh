using SummerFresh.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SummerFresh.Basic;
using System.Threading;
using System.ComponentModel;
using System.Web.Script.Serialization;
namespace SummerFresh.Controls
{

    /// <summary>
    /// 复选框集合
    /// </summary>
    [DisplayName("复选框集合")]
    public class CheckBoxList : FormControlBase, IKeyValueDataSourceControl, IChildren
    {
        public CheckBoxList()
        {
            Controls = new List<IControl>();
            CssClass = "";
        }

        protected override string TagName
        {
            get { return "div"; }
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

        internal override void AddAttributes()
        {
            this.CssClass = "";
            base.AddAttributes();
            Attributes.Remove("name");
        }

        protected override string RenderInner()
        {
            if (DataSource == null)
            {
                throw new CustomException("CheckBoxList 需要 DataSource");
            }
            StringBuilder content = new StringBuilder();
            IList<SelectListItem> items = DataSource.SelectItems();
            if (!Value.IsNullOrEmpty())
            {
                items.ForEach((o) =>
                {
                    if (Value.Split(',').Contains(o.Value, StringComparer.Create(Thread.CurrentThread.CurrentCulture, true)))
                    {
                        o.Selected = true;
                    }
                });
            }
            items.ForEach(item =>
            {
                var checkbox = new CheckBox() { Value = item.Value, Checked = item.Selected, ID = ID + "_" + item.Value, Name = Name, Text = item.Text };
                content.AppendLine(checkbox.Render());
            });
            return content.ToString();
        }

        [FormField(Editable = false)]
        public IList<IControl> Controls
        {
            get;
            set;
        }
    }
}
