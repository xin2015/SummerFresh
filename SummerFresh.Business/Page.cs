using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SummerFresh.Basic;
using SummerFresh.Business.Entity;
using System.Text.RegularExpressions;
using SummerFresh.Data.Attributes;
using System.ComponentModel;
using SummerFresh.Environment;
using System.Web.Script.Serialization;
using SummerFresh.Util;
namespace SummerFresh.Business
{
    public class Page : IChildren, IControl
    {
        private HttpRequest _request;
        private IList<TagBuilder> links;
        private IList<TagBuilder> scripts;
        private IDictionary<string, TagBuilder> metas;
        private TagBuilder script;
        private TagBuilder style;
        private IList<string> keys;
        private StringBuilder startUpScript;
        private PageEntity PageEntity;
        public Page(HttpRequest request, PageEntity pageEntity)
        {
            PageEntity = pageEntity;
            _request = request;
            ID = pageEntity.PageName;
            Title = pageEntity.PageTitle;
            ExtFiles = pageEntity.ExtFiles;
            PageStartUpScript = pageEntity.PageStartUpScript;
            PageScriptBlock = pageEntity.PageScriptBlock;
            PageStyle = pageEntity.PageStyle;
            Children = new Dictionary<string, IList<IComponent>>();
            var componentEntities = pageEntity.Components;
            IDictionary<ComponentEntity, IControl> eTc = new Dictionary<ComponentEntity, IControl>();
            foreach (var component in componentEntities)
            {

                var type = TypeHelper.GetType(component.ComponentType);
                if (!component.ComponentXML.IsNullOrEmpty())
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    serializer.RegisterConverters(new[] { new ComponentConverter() });
                    var instance = serializer.Deserialize(component.ComponentXML, type);
                    eTc.Add(component, instance as IControl);
                }
            }
            foreach (var e in eTc.Keys)
            {
                if (e.ParentId.Equals(pageEntity.PageId))
                {
                    if (!Children.Keys.Contains(e.TargetId))
                    {
                        var components = new List<IComponent>();
                        Children.Add(e.TargetId, components);
                    }
                    var tempComponent = eTc[e] as IAttributeAccessor;
                    tempComponent.Attributes["componentId"] = e.ComponentId;
                    Children[e.TargetId].Add(tempComponent as IComponent);
                    GetChildren(eTc, e);
                }
            }
        }

        private static void GetChildren(IDictionary<ComponentEntity, IControl> eTc, ComponentEntity e)
        {
            var target = eTc[e] as IChildren;
            if (target != null)
            {
                foreach (var ee in eTc.Keys)
                {
                    if (ee.ParentId.Equals(e.ComponentId))
                    {
                        var tempComponent = eTc[ee] as IAttributeAccessor;
                        if (tempComponent != null)
                        {
                            tempComponent.Attributes["componentId"] = ee.ComponentId;
                        }
                        target.AddChildren(ee.TargetId, eTc[ee]);
                        GetChildren(eTc, ee);
                    }
                }
            }
        }

        public void Reset()
        {
            links = new List<TagBuilder>();
            scripts = new List<TagBuilder>();
            metas = new Dictionary<string, TagBuilder>();
            script = new TagBuilder("script");
            script.Attributes.Add("type", "text/javascript");
            style = new TagBuilder("style");
            style.Attributes.Add("type", "text/css");
            keys = new List<string>();
            startUpScript = new StringBuilder();
        }
        public string ID
        {
            get;
            set;
        }
        public string Title { get; set; }

        public string PageStartUpScript { get; set; }

        public string PageScriptBlock { get; set; }

        public string PageStyle { get; set; }

        public HttpRequest Request
        {
            get
            {
                return _request ?? (_request = HttpContext.Current.Request);
            }
        }

        public IList<ExternalFileEntity> ExtFiles { get; set; }

        public IDictionary<string, IList<IComponent>> Children { get; set; }

        IList<IControl> _controls;
        public IList<IControl> Controls
        {
            get
            {
                if (!Children.IsNullOrEmpty())
                {
                    var returnValue = new List<IComponent>();
                    foreach (var key in Children.Keys)
                    {
                        returnValue.AddRange(Children[key]);
                    }
                    _controls = returnValue.ToList<IControl>();
                }
                return _controls;
            }
            set
            {
                _controls = value;
            }
        }

        public void AddChildren(string property, object component)
        {

        }

        public IControl FindControl(string controlId)
        {
            return RecGet(this, controlId);
        }

        private IControl RecGet(IControl c, string componentId)
        {
            if (c == null) return null;
            if (!c.ID.IsNullOrEmpty() && c.ID.Equals(componentId, StringComparison.CurrentCultureIgnoreCase))
            {
                return c;
            }
            if (c is IChildren)
            {

                foreach (var cc in (c as IChildren).Controls)
                {
                    var result = RecGet(cc, componentId);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }

        private Layout _layout;
        public Layout Layout
        {
            get
            {
                return _layout ?? (_layout = new Layout(this, PageEntity.Layout));
            }
        }

        public IPageService PageService { get; set; }

        /// <summary>
        /// 注册外置CSS文件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cssFile"></param>
        public void RegisterExtCssFile(string key, string cssFile)
        {
            key = "CssFile-{0}".FormatTo(key);
            if (!keys.Contains(key))
            {
                keys.Add(key);
                var linkTag = new TagBuilder("link");
                linkTag.Attributes.Add("href", cssFile);
                linkTag.Attributes.Add("rel", "stylesheet");
                links.Add(linkTag);
            }
        }

        /// <summary>
        /// 注册外置JS文件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="jsFile"></param>
        public void RegisterExtJsFile(string key, string jsFile)
        {
            key = "JsFile-{0}".FormatTo(key);
            if (!keys.Contains(key))
            {
                keys.Add(key);
                var scriptTag = new TagBuilder("script");
                scriptTag.Attributes.Add("src", jsFile);
                scripts.Add(scriptTag);
            }
        }

        /// <summary>
        /// 注册启动脚本
        /// </summary>
        /// <param name="key"></param>
        /// <param name="script"></param>
        public void RegisterStartUpScript(string key, string script)
        {
            key = "StartUpScript-{0}".FormatTo(key);
            if (!keys.Contains(key))
            {
                keys.Add(key);
                startUpScript.AppendLine(script);
            }
        }

        /// <summary>
        /// 注册脚本语句块
        /// </summary>
        /// <param name="key"></param>
        /// <param name="scriptContent"></param>
        public void RegisterScriptBlock(string key, string scriptContent)
        {
            key = "ScriptContent-{0}".FormatTo(key);
            if (!keys.Contains(key))
            {
                keys.Add(key);
                script.AppendInnerHtml(scriptContent);
            }
        }

        /// <summary>
        /// 注册样式语句块
        /// </summary>
        /// <param name="key"></param>
        /// <param name="styleContent"></param>
        public void RegisterStyleContent(string key, string styleContent)
        {
            key = "CssContent-{0}".FormatTo(key);
            if (!keys.Contains(key))
            {
                keys.Add(key);
                style.AppendInnerHtml(styleContent);
            }
        }

        public void AddMetaData(string metaKey, string key, string value)
        {
            TagBuilder meta;
            if (metas.Keys.Contains(metaKey))
            {
                meta = metas[metaKey];
            }
            else
            {
                meta = new TagBuilder("meta");
                metas.Add(metaKey, meta);
            }
            meta.Attributes[key] = value;
        }
        public void Prepare()
        {
            Layout.Render();
            foreach (var extCssFile in ExtFiles.Where(o => o.FileType == "CSS").OrderBy(o => o.Rank))
            {
                this.RegisterExtCssFile(extCssFile.FileName, extCssFile.FilePath);
            }
            foreach (var extJsFile in ExtFiles.Where(o => o.FileType == "JS").OrderBy(o => o.Rank))
            {
                this.RegisterExtJsFile(extJsFile.FileName, extJsFile.FilePath);
            }
            this.RegisterStartUpScript("PageStartUpScript", PageStartUpScript);
            this.RegisterScriptBlock("PageScriptBlock", PageScriptBlock);
            this.RegisterStyleContent("PageStyle", PageStyle);
            var behaviour = SummerFresh.Security.SecurityFactory.Provider.GetUISecurityBehaviours(Request.FilePath, Request.Url.Query);
            if (!Children.IsNullOrEmpty())
            {
                foreach (var c in Controls)
                {
                    if (c is IAuthorityComponent)
                    {
                        (c as IAuthorityComponent).Authority(behaviour);
                    }
                    if (c is ITargetId)
                    {
                        var targetControl = c as ITargetId;
                        if (!targetControl.TargetId.IsNullOrEmpty())
                        {
                            var tempControl = new List<IControl>();
                            foreach (var id in targetControl.TargetId.Split(','))
                            {
                                tempControl.Add(FindControl(id));
                            }
                            targetControl.SetTarget(tempControl);
                        }
                    }
                }
            }
        }

        public string Render()
        {
            string documentType = "<!DOCTYPE html>\n";
            TagBuilder html = new TagBuilder("html");
            TagBuilder header = new TagBuilder("head");
            TagBuilder title = new TagBuilder("title");
            TagBuilder body = new TagBuilder("body");
            body.AddCssClass("easyui-layout");
            body.AppendInnerHtml(Renderbody());
            title.SetInnerText(Title);
            foreach (var key in metas.Keys)
            {
                header.AppendInnerHtml(metas[key].ToString(TagRenderMode.SelfClosing));
            }
            header.AppendInnerHtml(title.ToString());
            foreach (var cssFile in links)
            {
                header.AppendInnerHtml(cssFile.ToString(TagRenderMode.SelfClosing));
            }
            if (!style.InnerHtml.IsNullOrEmpty())
            {
                header.InnerHtml += style.ToString();
            }
            foreach (var jsFile in scripts)
            {
                body.AppendInnerHtml(jsFile.ToString());
            }
            script.AppendInnerHtml("$(function(){\n" + startUpScript.ToString() + "\n});");
            body.AppendInnerHtml(script.ToString());
            html.AppendInnerHtml(header.ToString());
            html.AppendInnerHtml(body.ToString());
            return documentType + html.ToString();
        }

        private string Renderbody()
        {
            StringBuilder result = new StringBuilder();
            var layoutTemplate = HttpUtility.HtmlDecode(Layout.LayoutHtml);
            var matchs = Regex.Split(layoutTemplate, @"\$(?<Variable>.+?)\$", RegexOptions.Compiled);
            foreach (var match in matchs)
            {
                if (Children.Keys.Contains(match))
                {
                    foreach (var c in Children[match].OrderBy(o => o.Rank))
                    {
                        result.AppendLine(c.Render());
                    }
                }
                else
                {
                    result.Append(match);
                }
            }
            return result.ToString();
        }
    }


    public static class TagBuilderExtension
    {
        public static void AppendInnerHtml(this TagBuilder tag, string html)
        {
            tag.InnerHtml += "\n{0}".FormatTo(html);
        }
    }

    public static class PageBuilder
    {
        public static Page BuildPage(string id, HttpRequest request)
        {

            var pageEntity = new PageEntityService().Get(id) as PageEntity;
            if (pageEntity == null)
            {
                throw new CustomException("您正请求的页面不存在或已经被删除！");
            }
            Page result = null;
            if (!SysConfig.SystemStatus.Equals("Develop", StringComparison.CurrentCultureIgnoreCase))
            {
                string key = NamingCenter.GetCacheKey(CacheType.PAGE_CONFIG, pageEntity.PageName);
                result = CacheHelper.GetFromCache<Page>(key, () =>
                {
                    return new SummerFresh.Business.Page(request, pageEntity);
                });
            }
            else
            {
                result = new SummerFresh.Business.Page(request, pageEntity);
            }
            result.Reset();
            return result;
        }
    }
}
