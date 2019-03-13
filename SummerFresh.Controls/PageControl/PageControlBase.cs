using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using SummerFresh.Business;
using System.Web.Mvc;
using System.ComponentModel;
namespace SummerFresh.Controls
{
    public abstract class PageControlBase : SummerFresh.Business.IComponent, IAuthorityComponent, IChildren, ICloneable, IAttributeAccessor
    {
        public PageControlBase()
        {
            Visiable = true;
            Attributes = new Dictionary<string, string>();
            Controls = new List<IControl>();
        }

        /// <summary>
        /// 控件ID（每页唯一）
        /// </summary>
        [Description("（每页唯一）")]
        [Validator("required")]
        public string ID
        {
            get;
            set;
        }

        /// <summary>
        /// 排序值
        /// </summary>
        [DisplayName("排序值")]
        public int Rank { get; set; }

        /// <summary>
        /// CSS类
        /// </summary>
        public string CssClass
        {
            get;
            set;
        }

        /// <summary>
        /// 是否自动计算高度
        /// </summary>
        [DisplayName("是否自动计算高度")]
        public bool AutoHeight { get; set; }

        /// <summary>
        /// 自动计算高度偏移
        /// </summary>
        [DisplayName("自动计算高度偏移")]
        public int AutoHeightOffset { get; set; }

        public IDictionary<string, string> Attributes { get; private set; }

        [FormField(Editable=false)]
        public IList<IControl> Controls
        {
            get;
            set;
        }

        public virtual void AddChildren(string property,object component)
        {
            Controls.Add(component as IControl);
        }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Visiable
        {
            get;
            set;
        }

        /// <summary>
        /// 扩展属性
        /// </summary>
        [DisplayName("扩展属性")]
        public string AttributeString
        {
            get;
            set;
        }

        public virtual string Render()
        {
            if (Visiable)
            {
                TagBuilder div = new TagBuilder("div");
                div.Attributes["id"] = ID;
                div.Attributes["widget"] = this.GetType().Name;
                if (AutoHeight)
                {
                    div.Attributes["autoHeight"] = "true";
                    div.Attributes["autoHeightOffset"] = AutoHeightOffset.ToString();
                }
                if (this is ITargetId)
                {
                    Attributes["targetId"] = (this as ITargetId).TargetId;
                }
                if (!AttributeString.IsNullOrEmpty())
                {
                    string[] kvs = AttributeString.Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var kv in kvs)
                    {
                        string[] keyValue = kv.Split(':');
                        Attributes[keyValue[0]] = keyValue[1];
                    }
                }
                foreach (var key in Attributes.Keys)
                {
                    div.Attributes[key] = Attributes[key];
                }
                if (!CssClass.IsNullOrEmpty())
                {
                    div.AddCssClass(CssClass);
                }
                try
                {
                    div.InnerHtml = RenderContent();
                }
                catch (Exception ex)
                {
                    var errorTipSpan = new TagBuilder("div");
                    errorTipSpan.Attributes["style"] = "color:red;padding:20px;border:1px solid red;";
                    string stackTraceInfo = ex.StackTrace.Replace("在", "<br />在");
                    errorTipSpan.InnerHtml = ex.Message + stackTraceInfo;
                    div.InnerHtml = errorTipSpan.ToString();
                }
                return div.ToString();
            }
            return string.Empty;
        }

        public virtual string RenderContent()
        {
            return string.Empty;
        }

        public virtual void Authority(IDictionary<string, Security.UISecurityBehaviour> behaviour)
        {

        }

        public virtual object Clone()
        {
            var result = this.MemberwiseClone() as PageControlBase;
            result.Attributes = new Dictionary<string, string>();
            foreach(var attr in Attributes)
            {
                result.Attributes.Add(attr.Key, attr.Value);
            }
            return result;
        }
    }
}
