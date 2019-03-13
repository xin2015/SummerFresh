using SummerFresh.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SummerFresh.Basic;
using SummerFresh.Util;
using System.Reflection;
using SummerFresh.Business;
using System.Web.Script.Serialization;
using SummerFresh.Basic.FastReflection;
using System.Globalization;
namespace SummerFresh.SSO.Controllers
{
    [CustomHandleError]
    [CustomAuthorize]
    public class BaseController : Controller
    {


        public const string DefalutEntityAssembly = "SummerFresh.Business";
        public const string DefaultEntityNameTemplate = "SummerFresh.Business.Entity.{0}Entity";
        public const string DefaultEntityServiceTemplate = "SummerFresh.Business.Service.{0}EntityService";

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            if (behavior == JsonRequestBehavior.DenyGet
                && string.Equals(this.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                return new JsonResult();
            }
            AjaxResultModel result = new AjaxResultModel()
            {
                Data = data
            };
            return new CustomJsonResult() { Data = result, ContentType = contentType, ContentEncoding = contentEncoding, JsonRequestBehavior = behavior };
            //return base.Json(result, contentType, contentEncoding, behavior);
        }


        protected Type GetEntityType(string entityName)
        {
            var type = Assembly.Load(DefalutEntityAssembly).GetType(DefaultEntityNameTemplate.FormatTo(entityName));
            if (type == null)
            {
                throw new ArgumentOutOfRangeException("entityName");
            }
            return type;
        }
    }


    public class CustomJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;
            if (Data != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
                serializer.RegisterConverters(new[] { new DateTimeSerializer() });
                if (MaxJsonLength.HasValue)
                {
                    serializer.MaxJsonLength = MaxJsonLength.Value;
                }
                if (RecursionLimit.HasValue)
                {
                    serializer.RecursionLimit = RecursionLimit.Value;

                }
                response.Write(serializer.Serialize(Data));

            }
        }
    }

    public class DateTimeSerializer : JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }
        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var result = new Dictionary<string, object>();
            FastType ft = FastType.Get(obj.GetType());
            foreach (var p in ft.Getters)
            {
                if (p.Info.GetCustomAttribute<ScriptIgnoreAttribute>(true) != null)
                {
                    continue;
                }
                if (p.Type == typeof(DateTime))
                {
                    var attr = p.Info.GetCustomAttribute<TableFieldAttribute>(true);
                    if (attr != null)
                    {
                        string value = string.Format(CultureInfo.CurrentCulture, attr.DataFormatString, new object[] { p.GetValue(obj) });
                        result.Add(p.Name, value);
                    }
                    else
                    {
                        result.Add(p.Name, p.GetValue(obj).ConverTo<DateTime>().ToString("yyyy年MM月dd日"));
                    }
                }
                else
                {
                    result.Add(p.Name, p.GetValue(obj));
                }
            }
            return result;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return TypeHelper.Types; }
        }
    }
}
