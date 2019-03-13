using SummerFresh.Controls;
using SummerFresh.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using SummerFresh.Basic;
using SummerFresh.Business.Entity;
using SummerFresh.Business;
using SummerFresh.Business.Service;
using System.IO;
using System.Text;
using System.Globalization;
using SummerFresh.Environment;
using System.Transactions;

namespace SummerFresh.SSO.Controllers
{
    public class EntityController : BaseController
    {
        public ActionResult Insert(string entityName)
        {
            var type = GetEntityType(entityName);
            if (type == null)
            {
                return HttpNotFound();
            }
            var model = new FormModel();
            model.EntityName = entityName;
            model.Form = EntityComponentHelper.GetFormComponent(type);
            model.Form.Data = null;
            model.Form.PostUrl = "/Entity/Insert";
            return View("Edit", model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public JsonResult Insert(CustomEntity entity)
        {
            var service = EntityComponentHelper.GetEntityService(entity.GetType());
            service.Insert(entity);
            return Json(true);
        }

        public ActionResult Edit(string entityName, string id)
        {
            var type = GetEntityType(entityName);
            if (type == null)
            {
                throw new PageNotFoundException();
            }
            var service = EntityComponentHelper.GetEntityService(type);
            service.EntityType = type;
            var model = new FormModel();
            model.EntityName = entityName;
            model.Form = EntityComponentHelper.GetFormComponent(type);
            model.Form.PostUrl = "/Entity/Edit";
            model.Form.Data = service.Get(id);
            if (model.Form.Data == null)
            {
                throw new PageNotFoundException();
            }
            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public JsonResult Edit(CustomEntity entity)
        {
            var service = EntityComponentHelper.GetEntityService(entity.GetType());
            service.Update(entity);
            return Json(true);
        }

        [HttpPost]
        public JsonResult Delete(string entityName, string id)
        {
            bool result = false;
            var type = GetEntityType(entityName);
            var service = EntityComponentHelper.GetEntityService(type);
            using (TransactionScope tran = new TransactionScope())
            {
                if (id.Contains(',') || id.Contains(';'))
                {
                    var ids = id.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (service.BatchDelete(ids) > 0)
                    {
                        result = true;
                    }
                }
                else
                {
                    if (service.Delete(id) > 0)
                    {
                        result = true;
                    }
                }
                tran.Complete();
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult BatchDelete(string entityName, string id)
        {
            var type = GetEntityType(entityName);
            var service = EntityComponentHelper.GetEntityService(type);
            var ids = id.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            using (TransactionScope tran = new TransactionScope())
            {
                if (service.BatchDelete(ids) > 0)
                {
                    return Json(true);
                }
                tran.Complete();
            }
            return Json(false);
        }
        public ActionResult List(string entityName)
        {
            var type = GetEntityType(entityName);
            var model = new ListModel();
            model.Table = EntityComponentHelper.GetTableComponent(type);
            model.SearchConfig = EntityComponentHelper.GetSearchComponent(type);
            model.ToolbarConfig = EntityComponentHelper.GetToolbarComponent(type);
            if (type == typeof(ComponentEntity))
            {
                var btn1 = model.ToolbarConfig.Buttons.FirstOrDefault(o => o.ID == "btnGetFromDs");
                var btn2 = model.ToolbarConfig.Buttons.FirstOrDefault(o => o.ID == "btnMergeColumn");
                var visiable = !Request.QueryString["baseType"].IsNullOrEmpty() && Request.QueryString["baseType"].Equals("SummerFresh.Controls.TableColumn");
                btn1.Visiable = visiable;
                btn2.Visiable = visiable;
            }
            model.SearchConfig.TargetId = model.Table.ID;
            model.ToolbarConfig.TargetId = model.Table.ID;
            return View(model);
        }


        public ActionResult TreeList(string entityName)
        {
            var type = GetEntityType(entityName);
            var model = new TreeListModel();
            model.Table = EntityComponentHelper.GetTableComponent(type);
            model.SearchConfig = EntityComponentHelper.GetSearchComponent(type);
            model.ToolbarConfig = EntityComponentHelper.GetToolbarComponent(type);
            model.SearchConfig.TargetId = model.Table.ID;
            model.ToolbarConfig.TargetId = model.Table.ID;
            var attr = type.GetCustomAttribute<TreeAttribute>(true);
            if (attr != null)
            {
                model.TreeDataSource = attr.SqlId;
                if (!attr.IdParameterName.IsNullOrEmpty())
                {
                    model.IdParameterName = attr.IdParameterName;
                }
            }
            if (model.TreeDataSource.IsNullOrEmpty())
            {
                model.TreeDataSource = "sqlid:" + type.FullName + ".Tree";
            }
            return View(model);
        }
    }

    public class ListModel
    {
        public Table Table { get; set; }

        public Search SearchConfig { get; set; }

        public Toolbar ToolbarConfig { get; set; }
    }

    public class TreeListModel : ListModel
    {
        public TreeListModel()
        {
            IdParameterName = "TreeId";
        }
        public string TreeDataSource { get; set; }

        public string IdParameterName { get; set; }
    }

    public class FormModel
    {
        public string EntityName { get; set; }

        public Form Form { get; set; }

        public Tab Tab { get; set; }
    }
}
