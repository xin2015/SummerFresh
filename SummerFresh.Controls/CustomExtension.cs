using SummerFresh.Business;
using SummerFresh.Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SummerFresh.Controls
{
    public static class CustomExtension
    {
        public static MvcHtmlString CustomControl(this HtmlHelper html, IComponent component)
        {
            if (component != null)
            {
                var behaviour = SummerFresh.Security.SecurityFactory.Provider.GetUISecurityBehaviours(HttpContext.Current.Request.FilePath, HttpContext.Current.Request.Url.Query);

                if (component is IAuthorityComponent)
                {
                    (component as IAuthorityComponent).Authority(behaviour);
                }
                return MvcHtmlString.Create(component.Render());
            }
            return MvcHtmlString.Create("");
        }

        public static MvcHtmlString CustomControl(this HtmlHelper html, string pageName, string componentId)
        {
            var page = PageBuilder.BuildPage(pageName, HttpContext.Current.Request);
            if (page != null)
            {
                var component = page.FindControl(componentId) as IComponent;
                if (component != null)
                {
                    var behaviour = SummerFresh.Security.SecurityFactory.Provider.GetUISecurityBehaviours(HttpContext.Current.Request.FilePath, HttpContext.Current.Request.Url.Query);
                    if (component is IAuthorityComponent)
                    {
                        (component as IAuthorityComponent).Authority(behaviour);
                    }
                    return MvcHtmlString.Create(component.Render());
                }
            }
            return MvcHtmlString.Create("");
        }

        public static MvcHtmlString LayoutFile(this HtmlHelper html,string layoutName)
        {
            var layout = new LayoutEntityService().Get(layoutName) as LayoutEntity;
            
            return MvcHtmlString.Create("");
        }
    }
}
