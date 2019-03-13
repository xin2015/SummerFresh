using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Mvc;
using SummerFresh.Data;
using SummerFresh.Data.Attributes;
using SummerFresh.Util;
using SummerFresh.Controls;
using System.Reflection;
using SummerFresh.Business.Entity;
using System.Text;
using SummerFresh.Security;
using System.Web.Security;
using SummerFresh.Business;
using SummerFresh.Basic;
using SummerFresh.Business.Service;
using System.Threading.Tasks;

namespace SummerFresh.MVC.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HMenumIndex()
        {
            return View();
        }
        public ActionResult IpeChart() 
        {
            return View();
        }
        public ActionResult IpeThree() 
        {
            return View();
        }

        public ActionResult GetJson()
        {
            string url = "http://202.104.69.204:8087/Home/GetAllDcStationInfo";
            HttpClient client = new HttpClient();

            var task = client.GetAsync(new Uri(url));
            task.Result.EnsureSuccessStatusCode();
            HttpResponseMessage response = task.Result;
            var result = response.Content.ReadAsStringAsync();
            string responseBodyAsText = result.Result;
            return Json(responseBodyAsText, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetMenu()
        {
            var result = Dao.Get().QueryDictionaries("SummerFresh.Business.Entity.PermissionEntity.Tree");
            result = GetPermissionUrl(result);
            return Json(result, JsonRequestBehavior.AllowGet);
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

        public JsonResult ExecCRUDService(string sqlId)
        {
            var crudService = new CRUDService();
            var temp = sqlId.Split('.');
            string crudName = temp[0];
            string action = temp[1];
            crudService.CRUDName = crudName;
            string key = Request[crudService.KeyFieldName];
            IDictionary<string, object> formData = new Dictionary<string, object>();
            foreach (var k in Request.Form.AllKeys)
            {
                formData.Add(k, Request.Form[k]);
            }
            object result = null;
            switch (action)
            {
                case "Insert":
                    crudService.Insert(formData);
                    result = formData[crudService.KeyFieldName];
                    break;
                case "Update":
                    result = crudService.Update(key, formData);
                    break;
                case "Delete":
                    result = crudService.Delete(key);
                    break;
                case "Select":
                    result = crudService.GetList();
                    break;
                case "Get":
                    result = crudService.Get(key);
                    break;
            }
            return Json(result);
        }

        public JsonResult GetChartData()
        {
            string componentId = Request["componentId"];
            if (componentId.IsNullOrEmpty())
            {
                throw new CustomException("componentId不能为空!");
            }
            var chart = CRUDComponentHelper.GetComponent(componentId, System.Web.HttpContext.Current.Request) as NoPagerListControlBase;
            if (chart == null)
            {
                throw new CustomException("非NoPagerListControlBase组件!");
            }
            if (chart.DataSource == null)
            {
                throw new CustomException("需要指定DataSource!");
            }
            return Json(chart.GetData(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetChartOption()
        {
            string componentId = Request["componentId"];
            if (componentId.IsNullOrEmpty())
            {
                throw new CustomException("newChartId不能为空!");
            }
            var newChart = CRUDComponentHelper.GetComponent(componentId, System.Web.HttpContext.Current.Request) as NewChart;
            if (newChart == null)
            {
                throw new CustomException("非NewChart组件!");
            }
            return Json(newChart.CreateChartOption(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTreeNode()
        {
            string componentId = Request["componentId"];
            if (componentId.IsNullOrEmpty())
            {
                throw new CustomException("componentId不能为空!");
            }
            var tree = CRUDComponentHelper.GetComponent(componentId,System.Web.HttpContext.Current.Request) as Tree;
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

        private IList<IDictionary<string, object>> GetPermissionUrl(IList<IDictionary<string, object>> menus)
        {
            var result = new List<IDictionary<string, object>>();
            int count = 0;
            List<IDictionary<string, object>> parentList = new List<IDictionary<string, object>>();
            //foreach (var dict in menus)
            for(int i=0;i<menus.Count;i++)
            {
                var dict=menus[i];
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
            result = result.OrderBy(c => c["Rank"]).ToList();
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

        [CustomUnAuthorize]
        public ActionResult LogOn()
        {
            return View();
        }

        [CustomUnAuthorize]
        [HttpPost]
        public ActionResult LogOn(string userName, string password)
        {
            if (SecurityFactory.Authenticator.Authenticate(userName, password))
            {
                FormsAuthentication.SetAuthCookie(userName, false);
                var logger = LogManager.GetLogger("LogOnLogger");
                logger.Info("用户：{0},IP：{1}",userName,Request.UserHostAddress);
                string returnUrl = Request.QueryString["ReturnUrl"];
                if (!returnUrl.IsNullOrEmpty())
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewData["ErrorMessage"] = "账号或密码错误！";
                return View();
            }
        }

        [CustomUnAuthorize]
        //public ActionResult SSOLogOn()
        //{
        //    if(Authendicator.Authendicate())
        //    {
        //        FormsAuthentication.SetAuthCookie(Authendicator.UserName, false);
        //        var logger = LogManager.GetLogger("LogOnLogger");
        //        logger.Info("用户：{0},IP：{1}，来自SSO", Authendicator.UserName, Request.UserHostAddress);
        //        return RedirectToAction("Index", "Home");
        //    }
        //    return Redirect(Authendicator.GetSSOUrl());
        //}
        //public ActionResult SSOLogOut()
        //{
        //    return Redirect(Authendicator.GetSSOLogOutUrl());
        //}

        public JsonResult ResetPassword(string userId)
        {
            int effectCount = Dao.Get().ExecuteNonQuery("Security.ResetPassword", new { Password = SecurityFactory.Provider.EncryptPassword("111"), UserId = userId });
            if (effectCount < 0)
                throw new Exception("密码重置失败");
            return Json( true);
            
        }

        public ActionResult LogOut()
        {
            SecurityContext.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("LogOn");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ChangePassword(FormCollection form)
        {
            string password = form["OldPassword"];
            string newPassword = form["NewPassword"];
            string confirmPassword = form["ConfirmPassword"];
            
            if(!newPassword.Equals(confirmPassword, StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("新密码与确认密码不一致！");
            }
            if (SecurityFactory.Authenticator.Authenticate(SecurityContext.User.LoginId, password))
            {
                newPassword = SecurityFactory.Provider.EncryptPassword(newPassword);
                int effectCount = Dao.Get().ExecuteNonQuery("Security.ResetPassword", new { Password = newPassword, UserId = SecurityContext.User.UserId });
                if(effectCount>0)
                {
                    return Json(true);
                }
            }
            else
            {
                throw new Exception("原始密码错误！");
            }
            return Json(false);
        }

        [CustomUnAuthorize]
        public ActionResult Page403()
        {
            return View();
        }

        [CustomUnAuthorize]
        public ActionResult Page404()
        {
            return View();
        }

        [CustomUnAuthorize]
        public ActionResult Page500()
        {
            return View();
        }

        public ActionResult TableDemo()
        {
            return View();
        }

        public ActionResult ButtonDemo()
        {
            return View();
        }

        public ActionResult TreeForm()
        {
            return View();
        }
    }
}
