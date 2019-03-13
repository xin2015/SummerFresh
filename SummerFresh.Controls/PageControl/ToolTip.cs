using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SummerFresh.Basic;

namespace SummerFresh.Controls
{
    public class ToolTip:PageControlBase
    {
        public ToolTip()
        {
            CssClass = "alert";
            Attributes["role"] = "alert";
        }

        //[DisplayName("提示Html")]
        //public string TipHtml { get; set; }

        //[DisplayName("提示类型")]
        //public ToolTipType ToolTipType { get; set; }

        //[DisplayName("提示图标")]
        //public string Icon { get; set; }

        //[DisplayName("是否关闭")]
        //public bool ShowClose { get; set; }

        //public override string RenderContent()
        //{
        //    //TagBuilder result = new TagBuilder("div");
        //    //result.Attributes["class"] = ToolTipType.ToString();
        //    //result.Attributes["style"] = "background:#0000ff;color:#000099";
        //    Attributes["ToolTipType"] = ToolTipType.ToString();
        //    StringBuilder htmlStrBuilder = new StringBuilder();
        //    if(!Icon.IsNullOrWhiteSpace())
        //    {
        //        TagBuilder tagIcon = new TagBuilder("img");
        //        tagIcon.Attributes["src"] = Icon;
        //        htmlStrBuilder.AppendLine(Icon.ToString());
        //    }
        //    htmlStrBuilder.AppendLine(TipHtml);
        //    if(ShowClose)
        //    {
        //        TagBuilder tagButton = new TagBuilder("img");
        //        tagButton.Attributes["src"]="";
        //        htmlStrBuilder.AppendLine(tagButton.ToString());
        //    }
        //    //result.SetInnerText(htmlStrBuilder.ToString());
        //    //return  result.ToString();
        //    return htmlStrBuilder.ToString();
        //}

        [DisplayName("标题")]
        public string Title { get; set; }

        [DisplayName("内容")]
        public string Content { get; set; }

        [DisplayName("提示类型")]
        public ToolTipType ToolTipType { get; set; }

        public override string Render()
        {
            CssClass+=" alert-"+ToolTipType.ToString().ToLower();
            return base.Render();
        }

        public override string RenderContent()
        {
            return "<strong>{0}</strong>{1}".FormatTo(Title,Content);
            //return base.RenderContent();
        }
    }

    public enum ToolTipType
    {
        Info,
        Warning,
        Danger,
        Success,
    }
}
