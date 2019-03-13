using SummerFresh.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using System.ComponentModel;
using System.Web.Script.Serialization;
namespace SummerFresh.Controls
{
    /// <summary>
    /// 工具栏组件
    /// </summary>
    [DisplayName("工具栏组件")]
    public class Toolbar : PageControlBase,ITargetId,IScriptComponent,IAutoGenerate
    {
        public Toolbar()
        {
            Buttons = new List<Button>();
            CssClass = "toolbar";
        }

        /// <summary>
        /// 工具栏按钮
        /// </summary>
        [DisplayName("工具栏按钮")]
        public IList<Button> Buttons { get; set; }

        /// <summary>
        /// 目标控件ID
        /// </summary>
        [DisplayName("目标控件ID")]
        public string TargetId { get; set; }

        public void SetTarget(IList<IControl> components)
        {

        }

        public override void AddChildren(string property, object component)
        {
            Buttons.Add(component as Button);
            base.AddChildren(property, component);
        }

        public override string Render()
        {
            if (Buttons.Count(o=>o.Visiable)==0)
            {
                return string.Empty;
            }
            return base.Render();
        }

        public override string RenderContent()
        {
            if (Buttons.IsNullOrEmpty())
            {
                throw new CustomException("Buttons不能为空");
            }
            StringBuilder sb = new StringBuilder();
            foreach (var button in Buttons.OrderBy(o=>o.Rank))
            {
                sb.AppendLine(button.Render());
            }
            return sb.ToString();
        }

        public override void Authority(IDictionary<string, Security.UISecurityBehaviour> behaviour)
        {
            foreach (var button in Buttons)
            {
                if (behaviour.Keys.Contains(button.ID))
                {
                    button.Visiable = !behaviour[button.ID].IsInvisible;
                }
            }
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
                if (Buttons.IsNullOrEmpty() || Buttons.Count(o=>o.Visiable)==0)
                {
                    return string.Empty;
                }
                StringBuilder result = new StringBuilder();
                foreach (var button in Buttons)
                {
                    result.AppendLine(button.PageScriptBlock);
                }
                return result.ToString();
            }
        }

        public IControl Generate(IList<Business.Entity.DataFieldEntity> fields, string moduleName)
        {
            ID = "{0}Toolbar".FormatTo(moduleName);
            TargetId = "{0}Table".FormatTo(moduleName);
            this.Rank = 2;
            foreach (var btn in ControlDefaultSetting.GetDefaultToolbarButton())
            {
                this.Buttons.Add(btn);
            }
            return this;
        }
    }
}
