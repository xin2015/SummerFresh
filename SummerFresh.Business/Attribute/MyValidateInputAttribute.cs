using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SummerFresh.Basic;
using System.Reflection;
using System.Web.Util;

namespace SummerFresh.Business
{
    public class MyValidateInputAttribute : ActionFilterAttribute
    {
        public string ParameterName { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var entityName = string.Empty;
            if (filterContext.RouteData.Values.ContainsKey(ParameterName))
            {
                entityName = filterContext.RouteData.Values[ParameterName].ToString();
            }
            else if (!System.Web.HttpContext.Current.Request[ParameterName].IsNullOrEmpty())
            {
                entityName = System.Web.HttpContext.Current.Request[ParameterName];
            }
            if (!entityName.IsNullOrEmpty())
            {
                var type = TypeHelper.GetType(entityName);
                if (type == null)
                {
                    type = Assembly.Load("SummerFresh.Business").GetType("SummerFresh.Business.Entity.{0}Entity".FormatTo(entityName));
                }
                if (type != null)
                {
                    var attr = type.GetCustomAttribute<UnValidateInputeClassAttribute>(true);
                    if (attr != null)
                    {
                        return;
                    }
                }
            }
            var request = System.Web.HttpContext.Current.Request;
            var method = request.GetType().GetMethod("ValidateHttpValueCollection", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);// RequestValidationSource.Form
            if (method != null)
            {
                try
                {
                    method.Invoke(request, new object[] { request.Form, RequestValidationSource.Form });
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        throw ex.InnerException;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }
    }
}
