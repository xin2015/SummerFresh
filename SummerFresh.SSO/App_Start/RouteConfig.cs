using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SummerFresh.SSO
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "EntityRefreshTable",
                url: "Entity/RefreshTable",
                defaults: new { controller = "Entity", action = "RefreshTable" }
            );
            routes.MapRoute(
                name: "EntityDelete",
                url: "Entity/Delete/{entityName}/{id}",
                defaults: new { controller = "Entity", action = "Delete" }
            );
            routes.MapRoute(
                name: "EntityInsert",
                url: "Entity/Edit/{entityName}/{id}",
                defaults: new { controller = "Entity", action = "Edit" }
            );
            routes.MapRoute(
                name: "EntityCommon",
                url: "Entity/{action}/{entityName}",
                defaults: new { controller = "Entity", action = "Edit" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}