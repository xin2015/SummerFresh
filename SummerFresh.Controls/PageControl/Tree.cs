using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using SummerFresh.Business;
using System.Web.Script.Serialization;
namespace SummerFresh.Controls
{
    public class Tree : PageControlBase, ICascadeDataSourceControl, IScriptComponent, IChildren
    {
        public Tree()
        {
            TreeCssClass = "ztree";
            ShowCheck = false;
        }

        public bool ShowCheck { get; set; }

        public string TreeCssClass { get; set; }

        public override string Render()
        {
            Attributes["showcheck"] = ShowCheck.ToString().ToLower();
            return base.Render();
        }

        public override string RenderContent()
        {
            if (DataSource == null)
            {
                throw new CustomException("需要为Tree设置数据源");
            }
            return "<ul id=\"{0}-ul\" class=\"{1}\"></ul>".FormatTo(ID, TreeCssClass);
        }

        public ICascadeDataSource DataSource
        {
            get;
            set;
        }

        public string PageStartUpScript
        {
            get
            {
                return string.Empty;
            }
        }

        public string PageScriptBlock
        {
            get
            {
                return string.Empty;
            }
        }

        public override void AddChildren(string property, object component)
        {
            DataSource = component as ICascadeDataSource;
            base.AddChildren(property, component);
        }
    }
}
