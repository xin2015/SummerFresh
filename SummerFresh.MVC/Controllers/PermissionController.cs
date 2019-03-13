using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using SummerFresh.Business.Entity;
using SummerFresh.Controls;
using SummerFresh.Data;
using SummerFresh.Basic;
using SummerFresh.Business;
using SummerFresh.Business.Service;
using SummerFresh.Security;

namespace SummerFresh.MVC.Controllers
{
    public class PermissionController : BaseController
    {
        public ActionResult NgList()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        public JsonResult Get()
        {
            string sqlId = "Permission.Select";
            var dict = Dao.Get().QueryDictionaries(sqlId);
            if (SecurityContext.User.LoginId.Equals(Environment.Env.Resolve("SystemManager")))
            {
                return Json(dict, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var returnValue = new List<IDictionary<string, object>>();
                foreach (var d in dict)
                {
                    if (d["PermissionType"].ToString().Equals("1"))
                    {
                        returnValue.Add(d);
                    }
                    else
                    {
                        if (SecurityContext.User.HasPermission(d["PermissionCode"].ToString()))
                        {
                            var rule = SecurityFactory.Provider.GetPermissionRule(d["PermissionCode"].ToString());
                            if(!rule.IsNullOrEmpty())
                            {
                                var rules = dict.Where(o => o["ParentId"].ToString().Equals(d["PermissionId"].ToString(), StringComparison.CurrentCultureIgnoreCase));
                                int order = rules.FirstOrDefault(o => o["RuleContent"].ToString().Equals(rule, StringComparison.CurrentCultureIgnoreCase))["Rank"].ConverTo<int>();
                                foreach(var r in rules)
                                {
                                    if(r["Rank"].ConverTo<int>()>=order)
                                    {
                                        returnValue.Add(r);
                                    }
                                }
                            }
                            returnValue.Add(d);
                        }
                    }
                }
                returnValue = returnValue.OrderBy(o => o["PermissionType"]).OrderBy(o => o["Rank"]).ToList();
                return Json(returnValue, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
