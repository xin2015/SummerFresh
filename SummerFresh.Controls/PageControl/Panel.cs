using SummerFresh.Business;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel;
using SummerFresh.Basic;
using System.Web.Script.Serialization;

namespace SummerFresh.Controls
{
    [DisplayName("面板组件")]
    public class Panel:PageControlBase,IChildren
    {
        public Panel()
        {
            Content = new List<PageControlBase>();
            HeaderButtons = new List<Link>();
            CssClass = "panels";
            Height = "100%";
            Width = "100%";
        }

        [DisplayName("标题Html")]
        public string HeaderHTML { get; set; }

        [DisplayName("底部Html")]
        public string FooterHTML { get; set; }

        [DisplayName("头部按钮")]
        public List<Link> HeaderButtons { get; set; }

        [DisplayName("面板风格")]
        public PanelStyle PanelStyle { get; set; }

        [DisplayName("内部组件")]
        public IList<PageControlBase> Content { get; set; }

        [DisplayName("宽度")]
        public string Width { get; set; }

        [DisplayName("高度")]
        public string Height { get; set; }

        public override string RenderContent()
        {
            //outer panels panels-primary info success....
            TagBuilder tagHeader = null; //panels-heading
            TagBuilder tagBody = null;   //panels-body
            TagBuilder tagFooter = null; //panels-footer

            //btn-panels-heading
            //<a class="btn btn-xs btn-default" href="##" role="button"><i class="Hui-iconfont"></i></a>

            if (!HeaderHTML.IsNullOrEmpty() || (HeaderButtons!=null&&HeaderButtons.Count>0))
            {
                tagHeader = new TagBuilder("div");
                tagHeader.Attributes["id"] = ID + "-heading";
                tagHeader.Attributes["class"] = "panels-heading";
                if(!HeaderHTML.IsNullOrEmpty())
                    tagHeader.AppendInnerHtml(HeaderHTML);
                if (HeaderButtons != null && HeaderButtons.Count > 0)
                {
                    TagBuilder btnDiv = new TagBuilder("div");
                    btnDiv.Attributes["id"] = ID+"-heading-btn";
                    btnDiv.Attributes["class"] = "btn-panels-heading";
                    HeaderButtons = HeaderButtons.OrderBy(c => c.Rank).ToList();
                    foreach (Link button in HeaderButtons)
                    {
                        button.CssClass = "btn btn-xs btn-default";
                        btnDiv.AppendInnerHtml(button.Render());
                    }
                    tagHeader.AppendInnerHtml(btnDiv.ToString());
                }
            }
            if (Content != null)
            {
                tagBody = new TagBuilder("div");
                tagBody.Attributes["id"] = ID + "-body";
                tagBody.Attributes["class"] = "panels-body";
                Content = Content.OrderBy(c => c.Rank).ToList();
                foreach (PageControlBase control in Content)
                {
                    tagBody.AppendInnerHtml(control.Render());
                }
            }
            if (!FooterHTML.IsNullOrWhiteSpace())
            {
                tagFooter = new TagBuilder("div");
                tagFooter.Attributes["id"] = ID + "-footer";
                tagFooter.Attributes["class"] = "panels-footer";
                tagFooter.AppendInnerHtml(FooterHTML);
            }
            TagBuilder[] tagArray = { tagHeader, tagBody, tagFooter };
            string result = "";
            foreach (TagBuilder tag in tagArray)
            {
                if(tag!=null)
                    result += tag.ToString();
            }
            return result;
        }

        public override string Render()
        {
            string styleStr="width:{0};height:{1};".FormatTo(Width,Height);
            if (!this.Attributes.ContainsKey("style"))
            {
                this.Attributes["style"] = styleStr;
            }
            else
            {
                this.Attributes["style"] += styleStr;
            }
            CssClass +=" panels-" + PanelStyle.ToString().ToLower();
            return base.Render();
        }

        public override void AddChildren(string property, object component)
        {
            switch (property)
            {
                case "HeaderButtons":
                    HeaderButtons.Add(component as Link);
                    break;
                case "Content":
                    Content.Add(component as PageControlBase);
                    break;
                default:
                    break;
            }
            base.AddChildren(property, component);
        }
    }

    public enum PanelStyle
    {
        Default,
        Info,
        Warning,
        Danger,
        Primary,
        Success,
    }
}
