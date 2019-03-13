using SummerFresh.Business;
using SummerFresh.Business.Entity;
using SummerFresh.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SummerFresh.Basic;
using System.Web.Script.Serialization;
using SummerFresh.Controls;
namespace SummerFresh.MVC.Controllers
{
    public class PageController : BaseController
    {
        //
        // GET: /Page/

        public string Index(string id, string componentId = "")
        {
            var page = PageBuilder.BuildPage(id, System.Web.HttpContext.Current.Request);
            page.Prepare();
            if (!componentId.IsNullOrEmpty())
            {
                var c = page.FindControl(componentId) as IComponent;
                if (c != null)
                {
                    return c.Render();
                }
            }
            return page.Render();
        }

        public JsonResult Component(string componentId)
        {
            string id = Request["childrenId"]; //为了下拉联动特殊处理
            var c = CRUDComponentHelper.GetComponent(componentId, System.Web.HttpContext.Current.Request) as IComponent;
            if (c != null)
            {
                if (!id.IsNullOrEmpty())
                {
                    //为了下拉联动特殊处理
                    if (c is Search)
                    {
                        var f = c as Search;
                        var t = f.Fields.FirstOrDefault(o => o.ID.Equals(id, StringComparison.CurrentCultureIgnoreCase));
                        return Json(t.Render(), JsonRequestBehavior.AllowGet);
                    }
                    if (c is Form)
                    {
                        var f = c as Form;
                        var t = f.Fields.FirstOrDefault(o => o.ID.Equals(id, StringComparison.CurrentCultureIgnoreCase));
                        return Json(t.Render(), JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(c.Render(), JsonRequestBehavior.AllowGet);
            }
            throw new CustomException("component not exist!");
        }

        public JsonResult GetTableColumn(string componentId)
        {
            var c = CRUDComponentHelper.GetComponent(componentId, System.Web.HttpContext.Current.Request) as Table;
            if (c == null)
            {
                return Json(false);
            }
            else
            {
                var result = new List<TreeNode>();
                result.Add(new TreeNode() { id = "Root", name = "表格列", pId = "", open = true });
                foreach (var column in c.Columns)
                {
                    result.Add(new TreeNode() { id = column.FieldName, name = column.ColumnName, pId = "Root" });
                    RecAdd(column, result);
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        private void RecAdd(TableColumn c, List<TreeNode> result)
        {
            if (c.Children.IsNullOrEmpty()) return;
            foreach (var column in c.Children)
            {
                result.Add(new TreeNode() { id = column.FieldName, name = column.ColumnName, pId = c.FieldName });
                RecAdd(column, result);
            }
        }

        public string PreView(string id)
        {
            var page = PageBuilder.BuildPage(id, System.Web.HttpContext.Current.Request);
            page.RegisterStartUpScript("Design", "summerFresh.widget.design();");
            page.Prepare();
            return page.Render();
        }
    }
}
