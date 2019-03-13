using SummerFresh.Security;
using SummerFresh.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SummerFresh.MVC
{
    [AttributeUsage(AttributeTargets.All, Inherited = true)]
    public class ProfileFilterAttribute : ActionFilterAttribute
    {
        private Stopwatch st;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            st = Stopwatch.StartNew();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            st.Stop();
            string controller = filterContext.RouteData.Values["controller"].ToString();
            string action = filterContext.RouteData.Values["action"].ToString();
            int elapsad = st.Elapsed.Milliseconds;
            var logger = LogManager.GetLogger("PageAccessLogger");
            string userName = string.Empty;
            if (filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                userName = SecurityContext.User.LoginId;
            }
            logger.Info("用户：{0}，Url:{1}，Form：{2}，耗时：{3}", userName,filterContext.RequestContext.HttpContext.Request.Path,GetFormInfo(filterContext.RequestContext.HttpContext.Request), elapsad);
        }

        private string GetFormInfo(HttpRequestBase request)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in request.Form.AllKeys)
            {
                sb.AppendFormat("{0}={1}\n", key, request.Form[key]);
            }
            return sb.ToString();
        }
    }
}