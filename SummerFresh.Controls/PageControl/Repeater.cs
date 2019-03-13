using SummerFresh.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using Antlr3.ST;
using System.Web.Mvc;
using System.Web;
using System.Web.Script.Serialization;
using System.ComponentModel;
namespace SummerFresh.Controls
{
    public class Repeater : ListControlBase
    {
        public Repeater()
        {
            AllowPaging = true;
            PagerPosition = PagerPosition.Bottom;
            Pager = new Pager();
            ModelName = "model";
            SubContainerTag = SummerFresh.Controls.SubContainerTag.none;
        }

        [DisplayName("单项显示html模板")]
        [HtmlEncode]
        [FormField(ControlType = ControlType.TextArea)]
        public string ItemTemplate { get; set; }

        [DisplayName("子容器标签")]
        public SubContainerTag SubContainerTag { get; set; }

        [DisplayName("子容器CssClass")]
        public string SubContainerCssClass { get; set; }

        [FormField(ControlType = ControlType.TextArea)]
        [DisplayName("无数据时显示html模板")]
        public string EmptyTemplate { get; set; }

        [FormField(ControlType = ControlType.TextArea)]
        [DisplayName("分隔区域html模板")]
        public string SeperatorTemplate { get; set; }

        public string GroupBy { get; set; }

        [DisplayName("实体类型")]
        public string ModelName { get; set; }

        [DisplayName("排序字段")]
        public string SortExpression { get; set; }

        public override string RenderContent()
        {
            IList<IDictionary<string, object>> entities = GetData();
            if (entities.IsNullOrEmpty())
            {
                return EmptyTemplate;
            }
            StringBuilder result = new StringBuilder();
            if (AllowPaging)
            {
                Pager.TargetId = ID;
                if (PagerPosition == PagerPosition.Top || PagerPosition == PagerPosition.Both)
                {
                    result.AppendLine(Pager.Render());
                }
            }
            entities.ForEach((entity) =>
            {
                StringTemplate query = new StringTemplate(ItemTemplate);
                query.SetAttribute(ModelName, entity);
                result.AppendLine(query.ToString());
                if(entities.IndexOf( entity)!=entities.Count-1)
                    result.AppendLine(SeperatorTemplate);
            });
            if (AllowPaging)
            {
                if (PagerPosition == PagerPosition.Bottom || PagerPosition == PagerPosition.Both)
                {
                    result.AppendLine(Pager.Render());
                }
            }
            if (SubContainerTag!= SubContainerTag.none)
            {
                var subContainer = new TagBuilder(SubContainerTag.ToString());
                if (!SubContainerCssClass.IsNullOrEmpty())
                {
                    subContainer.AddCssClass(SubContainerCssClass);
                }
                subContainer.InnerHtml = result.ToString();
                return subContainer.ToString();
            }
            return result.ToString();
        }

        public override void AddChildren(string property, object component)
        {
            if (property.Equals("Pager"))
            {
                Pager = component as Pager;
            }
            if (property.Equals("DataSource"))
            {
                DataSource = component as IListDataSource;
            }
            base.AddChildren(property, component);
        }
    }

    public enum SubContainerTag
    {
        none,
        div,
        ul,
        ol
    }
}
