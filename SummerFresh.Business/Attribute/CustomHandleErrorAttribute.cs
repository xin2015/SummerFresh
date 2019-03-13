using SummerFresh.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace SummerFresh.Business
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (!(filterContext.Exception is CustomException))
            {
                //预料之外的异常，记录日志
                LogHelper.Error(filterContext.Exception.Message, filterContext.Exception);
            }
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                JsonResult json = new JsonResult();
                AjaxResultModel result = new AjaxResultModel()
                {
                    Result = false,
                    //ErrorMessage = "出错的地址是：" + filterContext.RouteData.Values["controller"].ToString() + "/" + filterContext.RouteData.Values["action"].ToString() + "\n异常信息为：" + filterContext.Exception.Message
                    ErrorMessage = filterContext.Exception.Message
                };
                json.Data = result;
                json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                filterContext.Result = json;
            }
            else
            {
                if (filterContext.Exception is PageNotFoundException)
                {
                    filterContext.ExceptionHandled = true;
                    filterContext.HttpContext.Response.Clear();
                    filterContext.HttpContext.Response.StatusCode = 404;
                    filterContext.Result = new HttpNotFoundResult();
                }
                else if (filterContext.Exception is UnAuthorizedException)
                {
                    filterContext.ExceptionHandled = true;
                    filterContext.HttpContext.Response.Clear();
                    filterContext.HttpContext.Response.StatusCode = 403;
                    filterContext.Result = new HttpUnauthorizedResult();
                }
                else
                {
                    base.OnException(filterContext);
                }
            }
        }
    }
}
