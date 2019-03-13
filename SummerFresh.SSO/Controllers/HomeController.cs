using SummerFresh.Security;
using SummerFresh.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SummerFresh.Basic;
using SummerFresh.SSOKit;
using SummerFresh.Data.Attributes;
using SummerFresh.Data;
using SummerFresh.Business;
using SummerFresh.Controls;
namespace SummerFresh.SSO.Controllers
{
    public class HomeController : BaseController
    {
        [CustomUnAuthorize]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string appKey = Request.Params[Authendicator.APP_KEY];
                string returnUrl = Request.Params[Authendicator.RETURN_URL];
                if (ValidateAppKey(appKey, returnUrl))
                {
                    return Redirect(GetReturnUrl(appKey,returnUrl, User.Identity.Name));
                }
                else
                {
                    if (!string.IsNullOrEmpty(appKey) || !string.IsNullOrEmpty(returnUrl))
                    {
                        throw new Exception(string.Format("appKey:{0} 及 returnUrl:{1} 无效", appKey, returnUrl));
                    }
                }
            }
            return View();
        }

        public bool ValidateAppKey(string appKey, string returnUrl)
        {
            if(string.IsNullOrEmpty(appKey) || string.IsNullOrEmpty(returnUrl))
            {
                return false;
            }
            var allSystem = CacheHelper.GetFromCache<IList<SSOSystem>>("All_SSO_System", () => { return Dao.Get().SelectAll<SSOSystem>(); });
            return allSystem.Any(o => o.SystemKey.Equals(appKey, StringComparison.OrdinalIgnoreCase)
                && o.SystemLogOnUrl.Equals(returnUrl, StringComparison.OrdinalIgnoreCase));
        }

        private string GetReturnUrl(string appKey,string returnUrl, string userName)
        {
            string result = string.Empty;
            if (!returnUrl.IsNullOrEmpty())
            {
                var param = new Dictionary<string, object>();
                param.Add(Authendicator.TOKEN_NAME, Authendicator.GetToken(appKey, userName));
                var logger = LogManager.GetLogger("LogOnLogger");
                logger.Info("appkey：{0},username：{1},token:{2}", appKey, userName, param[Authendicator.TOKEN_NAME]);
                result = HttpHelper.BuildUrl(returnUrl, param);
                return result;
            }
            return string.Empty;
        }

        [CustomUnAuthorize]
        [HttpPost]
        public ActionResult Index(string userName, string password)
        {
            if (SecurityFactory.Authenticator.Authenticate(userName, password))
            {
                string appKey = Request.Params[Authendicator.APP_KEY];
                string returnUrl = Request.Params[Authendicator.RETURN_URL];
                FormsAuthentication.SetAuthCookie(userName, true);
                var logger = LogManager.GetLogger("LogOnLogger");
                logger.Info("用户：{0},IP：{1}", userName, Request.UserHostAddress);
                if (ValidateAppKey(appKey, returnUrl))
                {
                    return Redirect(GetReturnUrl(appKey,returnUrl, userName));
                }
                else
                {
                    return RedirectToAction("Default");
                }
            }
            else
            {
                ViewData["ErrorMessage"] = "账号或密码错误！";
                return View();
            }
        }

        [CustomUnAuthorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            SecurityContext.Abandon();
            return RedirectToAction("Index");
        }

        public ActionResult Default()
        {
            return View();
        }

        public JsonResult GetMenu()
        {
            var result = Dao.Get().QueryDictionaries("SummerFresh.Business.Entity.PermissionEntity.Tree");
            result = GetPermissionUrl(result);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        private IList<IDictionary<string, object>> GetPermissionUrl(IList<IDictionary<string, object>> menus)
        {
            var result = new List<IDictionary<string, object>>();
            int count = 0;
            List<IDictionary<string, object>> parentList = new List<IDictionary<string, object>>();
            //foreach (var dict in menus)
            for (int i = 0; i < menus.Count; i++)
            {
                var dict = menus[i];
                //如果存在URL地址，说明是具体的叶节点，判断当前用户有无此节点的访问权限
                string url = dict["pUrl"].ToString();
                if (!string.IsNullOrEmpty(url))
                {
                    string virtualPath;
                    string queryString;

                    int index = url.IndexOf("?");

                    if (index > 0)
                    {
                        virtualPath = url.Substring(0, index);
                        queryString = url.Substring(index);
                    }
                    else
                    {
                        virtualPath = url;
                        queryString = string.Empty;
                    }
                    if (SecurityContext.Provider.HasPermissionOfUrl(virtualPath, queryString))
                    {
                        result.Add(dict);
                    }
                    else
                    {
                        menus.Remove(dict);
                        i--;
                    }
                }
                else
                {
                    //先加入父节点列表
                    parentList.Add(dict);
                }
            }

            ///统一对没有子节点的父节点统一标识再筛选加入到结果中
            parentList.ForEach(c => RemoveParentMenu(c, menus));
            result.AddRange(parentList.Where(c => !c.ContainsKey("remove")));

            return result;
        }

        private void RemoveParentMenu(IDictionary<string, object> dict, IList<IDictionary<string, object>> menus)
        {
            var childPList = menus.Where(o => o["pId"] != null && o["pId"].ToString() == dict["id"].ToString() && o["pUrl"].ToString().IsNullOrEmpty());
            childPList.ForEach(c => RemoveParentMenu(c, menus));
            if (menus.Count(o => o["pId"] != null && o["pId"].ToString() == dict["id"].ToString()) == 0)
            {
                dict["remove"] = true;
            }
        }

        public JsonResult GetDataBySqlId(string sqlId)
        {
            if (DaoFactory.GetSqlSource().Find(sqlId) != null)
            {
                return Json(Dao.Get().QueryDictionaries(sqlId), JsonRequestBehavior.AllowGet);
            }
            throw new Exception("sqlId不存在");
        }

        public JsonResult ExecCommandBySqlId(string sqlId)
        {
            if (DaoFactory.GetSqlSource().Find(sqlId) != null)
            {
                return Json(Dao.Get().ExecuteNonQuery(sqlId) > 0, JsonRequestBehavior.AllowGet);
            }
            throw new Exception("sqlId不存在");
        }


        public JsonResult GetTreeNode()
        {
            string componentId = Request["componentId"];
            if (componentId.IsNullOrEmpty())
            {
                throw new CustomException("componentId不能为空!");
            }
            var tree = CRUDComponentHelper.GetComponent(componentId, System.Web.HttpContext.Current.Request) as Tree;
            if (tree == null)
            {
                throw new CustomException("非tree组件!");
            }
            if (tree.DataSource == null)
            {
                throw new CustomException("请为tree指定DataSource!");
            }
            TreeNode treeNode = null;
            if (!Request["tree_node_id"].IsNullOrEmpty())
            {
                treeNode = new TreeNode()
                {
                    id = Request["tree_node_id"],
                    name = Request["tree_node_name"],
                    pId = Request["tree_node_pId"]
                };
            }
            return Json(tree.DataSource.SelectRootItems(treeNode), JsonRequestBehavior.AllowGet);
        }
    }

    [Table("SYS_SSOSystem")]
    public class SSOSystem
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string SystemName { get; set; }

        public string SystemLogOnUrl { get; set; }

        public string SystemKey { get; set; }

        public string Status { get; set; }
    }
}
