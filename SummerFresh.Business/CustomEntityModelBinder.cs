using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SummerFresh.Basic;
using SummerFresh.Basic.FastReflection;
using System.Threading;
namespace SummerFresh.Business
{
    [ValidateInput(false)]
    public class CustomEntityModelBinder:IModelBinder
    {
        public const string EntityFullNameHiddenName = "ENTITY_TYPE_FULL_NAME";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request.Form;
            string entityTypeFullName = request[EntityFullNameHiddenName];
            if(entityTypeFullName.IsNullOrEmpty())
            {
                throw new Exception("Missing Entity Type Full Name!");
            }
            var type = TypeHelper.GetType(entityTypeFullName);
            object entity = Activator.CreateInstance(type);
            var pis = FastType.Get(type).Setters;
            foreach(var p in pis)
            {
                if(request.AllKeys.Contains(p.Name,StringComparer.Create(Thread.CurrentThread.CurrentCulture,true)))
                {
                    var requestValue = request[p.Name];
                    p.SetValue(entity, requestValue.ConventToType(p.Info.PropertyType));
                }
            }
            return entity;
        }
    }
}
