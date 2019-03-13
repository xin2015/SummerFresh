using SummerFresh.Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    public class Layout : IScriptComponent
    {
        private Page _page;

        public Layout(Page page, LayoutEntity layout)
        {
            _page = page;
            ID = layout.LayoutId;
            ExtFiles = layout.ExtFiles;
            LayoutHtml = layout.LayoutHtml;
            PageStartUpScript = layout.LayoutStartUpScript;
            PageStyle = layout.LayoutPageStyle;

        }
        public string ID
        {
            get;
            set;
        }


        public Page Page { get { return _page; } }

        public IList<ExternalFileEntity> ExtFiles { get; set; }

        public string PageStartUpScript { get; set; }

        public string PageScriptBlock { get; set; }

        public string PageStyle { get; set; }

        public string LayoutHtml { get; set; }

        public void Render()
        {
            foreach (var extCssFile in ExtFiles.Where(o => o.FileType == "CSS").OrderBy(o => o.Rank))
            {
                Page.RegisterExtCssFile(extCssFile.FileName, extCssFile.FilePath);
            }
            foreach (var extJsFile in ExtFiles.Where(o => o.FileType == "JS").OrderBy(o => o.Rank))
            {
                Page.RegisterExtJsFile(extJsFile.FileName, extJsFile.FilePath);
            }
            Page.AddMetaData("contentType", "http-equiv", "Content-Type");
            Page.AddMetaData("contentType", "content", "text/html; charset=utf-8");
            Page.AddMetaData("chartSet", "charset", "utf-8");
            Page.AddMetaData("viewportMeta", "name", "viewport");
            Page.AddMetaData("viewportMeta", "content", "width=device-width, initial-scale=1.0");
            Page.RegisterStartUpScript("layoutStartUpScript", PageStartUpScript);
            Page.RegisterScriptBlock("layoutScriptBlock", PageScriptBlock);
            Page.RegisterStyleContent("layoutStyle", PageStyle);
        }
    }
}
