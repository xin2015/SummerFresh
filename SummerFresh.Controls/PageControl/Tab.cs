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
    /// 选项卡组件
    /// </summary>
    [DisplayName("选项卡组件")]
    public class Tab : PageControlBase, IScriptComponent, IKeyValueDataSourceControl
    {
        public Tab()
        {
            TabItems = new List<TabItem>();
            TabItemContainerCss = "tab-box";
        }

        /// <summary>
        /// 初始显示项索引
        /// </summary>
        [DisplayName("初始显示项索引")]
        public int InitIndex { get; set; }

        /// <summary>
        /// 选项卡容器CSS
        /// </summary>
        [DisplayName("选项卡容器CSS")]
        public string TabItemContainerCss { get; set; }

        /// <summary>
        /// 选项卡项
        /// </summary>
        [DisplayName("选项卡项")]
        public IList<TabItem> TabItems { get; set; }

        public override void AddChildren(string property, object component)
        {
            if (property.Equals("TabItems"))
            {
                TabItems.Add(component as TabItem);
            }
            if (property.Equals("DataSource"))
            {
                DataSource = component as IKeyValueDataSource;
            }
            base.AddChildren(property, component);
        }

        public override string Render()
        {
            if (DataSource != null)
            {
                var data = DataSource.SelectItems();
                foreach (var d in data)
                {
                    TabItems.Add(new TabItem()
                    {
                        ID = d.Value,
                        TabName = d.Text,
                        Visiable = true
                    });
                }
            }
            if (TabItems.Count(o => o.Visiable) == 0)
            {
                return string.Empty;
            }
            return base.Render();
        }

        public override string RenderContent()
        {
            if (TabItems.IsNullOrEmpty())
            {
                throw new CustomException("TabItems不能为空");
            }
            var tabBox = new TagBuilder("div");
            tabBox.AddCssClass(TabItemContainerCss);
            var content = string.Empty;
            var span = string.Empty;
            foreach (var item in TabItems.OrderBy(o => o.Rank))
            {
                tabBox.InnerHtml += item.RenderTab();
                content += item.Render();
            }
            return tabBox.ToString() + content;
        }

        public override void Authority(IDictionary<string, Security.UISecurityBehaviour> behaviour)
        {
            foreach (var item in TabItems)
            {
                if (behaviour.Keys.Contains(item.ID))
                {
                    item.Visiable = !behaviour[item.ID].IsInvisible;
                }
            }
        }

        public string PageStartUpScript
        {
            get { return string.Empty; }
        }

        public string PageScriptBlock
        {
            get
            {
                return string.Empty;
            }
        }

        public IKeyValueDataSource DataSource
        {
            get;
            set;
        }


    }

    /// <summary>
    /// 选项卡项
    /// </summary>
    [DisplayName("选项卡项")]
    public class TabItem : SummerFresh.Business.IComponent, ICloneable, IChildren
    {
        public TabItem()
        {
            CssClass = "tab-item";
            Visiable = true;
            Content = new List<PageControlBase>();
            Controls = new List<IControl>();
        }

        [FormField(Editable = false)]
        public IList<IControl> Controls
        {
            get;
            set;
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
        /// 是否显示
        /// </summary>
        public bool Visiable
        {
            get;
            set;
        }

        /// <summary>
        /// 是否延迟加载
        /// </summary>
        [DisplayName("是否延迟加载")]
        public bool LazyLoad { get; set; }

        /// <summary>
        /// 项名称
        /// </summary>
        [DisplayName("项名称")]
        public string TabName { get; set; }

        /// <summary>
        /// 项内容
        /// </summary>
        [DisplayName("项内容")]
        public IList<PageControlBase> Content { get; set; }

        /// <summary>
        /// 项图标
        /// </summary>
        [DisplayName("项图标")]
        public string Icon { get; set; }

        public void AddChildren(string property, object component)
        {
            Content.Add(component as PageControlBase);
            Controls.Add(component as IControl);
        }

        public string RenderTab()
        {
            if (Visiable)
            {
                var tabItem = new TagBuilder("span");
                tabItem.AddCssClass(CssClass);
                tabItem.Attributes["key"] = ID;
                if (!Icon.IsNullOrEmpty())
                {
                    var img = new TagBuilder("img");
                    img.Attributes["src"] = Icon;
                    img.Attributes["alt"] = TabName;
                    tabItem.InnerHtml = img.ToString();
                }
                tabItem.SetInnerText(TabName);
                return tabItem.ToString();
            }
            return string.Empty;
        }

        public string Render()
        {
            if (Visiable)
            {

                if (!Content.IsNullOrEmpty())
                {
                    //当内容中有iframe，则只支持一个
                    if (Content[0] is IFrame)
                    {
                        var c = Content[0] as IFrame;
                        c.Attributes["src1"] = c.Src;
                        c.Attributes["tabItem"] = ID;
                        c.Attributes["id"] = ID;
                        c.Src = "";
                        return c.Render();
                    }
                    else
                    {
                        var div = new TagBuilder("div");
                        div.Attributes["tabItem"] = ID;
                        div.Attributes["id"] = ID;
                        foreach (var c in Content)
                        {
                            if (LazyLoad)
                            {
                                if (c.Attributes.Keys.Contains("componentId"))
                                {
                                    if (!div.Attributes.Keys.Contains("contentId"))
                                    {
                                        div.Attributes["contentId"] = "";
                                    }
                                    div.Attributes["contentId"] += ("|" + c.Attributes["componentId"]);
                                }
                            }
                            else
                            {
                                div.InnerHtml += c.Render();
                            }
                        }
                        if (div.Attributes.Keys.Contains("contentId"))
                        {
                            div.Attributes["contentId"] = div.Attributes["contentId"].Substring(1);
                        }
                        return div.ToString();
                    }
                }
            }
            return string.Empty;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
