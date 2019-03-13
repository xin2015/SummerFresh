using SummerFresh.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using System.Globalization;
using SummerFresh.Environment;
using System.Web.Mvc;
using System.ComponentModel;
namespace SummerFresh.Controls
{
    [Serializable]
    public abstract class FormControlBase : IFormControl, ICloneable
    {
        private TagBuilder _tagBuilder;
        public FormControlBase()
        {
            Enable = true;
            Visiable = true;
            _tagBuilder = new TagBuilder(TagName);
            CssClass = "form-control";
        }

        [Validator("required")]
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

        public string Label { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [DisplayName("填写说明")]
        public string Description { get; set; }

        public bool Enable
        {
            get;
            set;
        }

        [DisplayName("自动触发搜索")]
        public bool ChangeTiggerSearch { get; set; }

        /// <summary>
        /// HTML容器
        /// </summary>
        [DisplayName("HTML容器")]
        public string ContainerTemplate { get; set; }

        public int Rank { get; set; }

        public IDictionary<string, string> Attributes
        {
            get
            {
                return _tagBuilder.Attributes;
            }
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

        /// <summary>
        /// 独占一行
        /// </summary>
        [DisplayName("独占一行")]
        public bool ColSpan { get; set; }

        public string CssClass { get; set; }

        public string Validator { get; set; }


        public string Value
        {
            get;
            set;
        }

        public bool Visiable { get; set; }

        protected abstract string TagName { get; }

        protected void SetInnerText(string innerText)
        {
            _tagBuilder.SetInnerText(innerText);
        }

        protected virtual bool SelfClosing
        {
            get
            {
                return false;
            }
        }
        public virtual string Render()
        {
            if (Visiable)
            {
                AddAttributes();
                _tagBuilder.InnerHtml = RenderInner();
                string result = string.Empty;
                if (SelfClosing)
                {
                    result = _tagBuilder.ToString(TagRenderMode.SelfClosing);
                }
                else
                {
                    result = _tagBuilder.ToString();
                }
                result += RenderAfter();
                if (ContainerTemplate.IsNullOrEmpty())
                {
                    return result;
                }
                else
                {
                    return ContainerTemplate.FormatTo(ID, Label, result, Description);
                }
            }
            return string.Empty;
        }

        protected virtual string RenderAfter()
        {
            return string.Empty;
        }

        protected virtual string RenderInner()
        {
            return string.Empty;
        }

        internal virtual void AddAttributes()
        {
            Attributes["id"] = ID;
            Attributes["name"] = Name;
            if (!Enable)
            {
                Attributes["disabled"] = "disabled";
            }
            if (!CssClass.IsNullOrEmpty())
            {
                Attributes["class"] = CssClass;
            }
            if (!Validator.IsNullOrEmpty())
            {
                Attributes["validator"] = Validator;
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
            if(ChangeTiggerSearch)
            {
                Attributes["ChangeTiggerSearch"] = "true";
            }
        }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
