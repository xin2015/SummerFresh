using SummerFresh.Business.Entity;
using SummerFresh.Controls;
using SummerFresh.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SummerFresh.Basic;
using SummerFresh.Business;
using SummerFresh.Basic.FastReflection;
using System.Collections;
using SummerFresh.Data.Mapping;
namespace SummerFresh.MVC.Controllers
{
    public class CRUDController : BaseController
    {
        [HttpPost]
        public JsonResult Create(string id)
        {
            string tableName = id;
            string moduleName = id.Replace("_", "");
            AddCRUD(tableName, moduleName);
            GetCRUD("", tableName, moduleName);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //[ValidateInput(false)]
        [HttpPost]
        public JsonResult Save()
        {
            string componentId = Request["componentId"];
            IDictionary<string, object> formData = new Dictionary<string, object>();
            foreach (var key in Request.Form.AllKeys)
            {
                formData.Add(key, Request.Form[key]);
            }
            var form = CRUDComponentHelper.GetComponent(componentId, System.Web.HttpContext.Current.Request) as Form;
            var service = form.FormService;
            if (service == null)
            {
                throw new CustomException("Form必须指定FormService");
            }
            int effectCount = 0;
            bool isInsert = true;
            if (Request["FormViewMode"].IsNullOrEmpty())
            {
                isInsert = Request[service.KeyFieldName].IsNullOrEmpty();
            }
            else
            {
                if (Request["FormViewMode"].Equals("Edit", StringComparison.CurrentCultureIgnoreCase))
                {
                    isInsert = false;
                }
            }
            if (isInsert)
            {
                effectCount = service.Insert(formData);
            }
            else
            {
                effectCount = service.Update(Request[service.KeyFieldName], formData);
            }
            return Json(effectCount > 0);
        }


        public JsonResult Delete()
        {
            string componentId = Request["componentId"];
            var table = CRUDComponentHelper.GetComponent(componentId, System.Web.HttpContext.Current.Request) as SummerFresh.Controls.Table;
            var service = table.DataSource as IFormService;
            if (service == null)
            {
                throw new CustomException("Table数据源非CRUDService！");
            }
            string key = Request[service.KeyFieldName];
            return Json(service.Delete(key) > 0, JsonRequestBehavior.AllowGet);
        }

        public void AddCRUD(string tableName, string moduleName)
        {
            var service = new CRUDEntityService();
            if (service.Get(moduleName) != null)
            {
                throw new CustomException("CRUDName已存在！");
            }
            var generator = SQLSyntaxGeneratorFactory.GetGenerator(tableName);
            var entity = new CRUDEntity()
            {
                CRUDName = moduleName,
                DefaultSortExpression = generator.GetDefaultSortExpression(),
                InsertSQL = generator.GetInsertSQL(),
                SelectSQL = generator.GetSelectSQL(),
                TableName = generator.Mapping.Table.Name,
                CRUDId = Guid.NewGuid().ToString()
            };
            if (!generator.Mapping.Table.Keys.IsNullOrEmpty())
            {
                entity.PKName = generator.Mapping.Table.Keys.First().Name;
                entity.UpdateSQL = generator.GetUpdateSQL();
                entity.DeleteSQL = generator.GetDeleteSQL();
                entity.GetOneSQL = generator.GetSelectOneSQL();
                entity.ValueFieldName = entity.PKName;
            }
            var parent = generator.Mapping.Table.Columns.FirstOrDefault(o =>
            {
                if (o.Name.IndexOf("ParentId") >= 0) { return true; }
                return false;
            });
            if (parent != null)
            {
                entity.ParentFieldName = parent.Name;
            }
            var title = generator.Mapping.Table.Columns.FirstOrDefault(o =>
            {
                if (o.Name.IndexOf("Name") >= 0) { return true; }
                return false;
            });
            if (title != null)
            {
                entity.TitleFieldName = title.Name;
            }

            service.Insert(entity);
        }

        public void GetCRUD(string parentId, string tableName, string moduleName)
        {
            IList<DataFieldEntity> fields = Dao.Get().QueryEntities<DataFieldEntity>("SummerFresh.Business.Entity.DataFieldEntity.Select", new { TreeNodeText = tableName });
            var listpageEntity = new PageEntity()
            {
                PageId = Guid.NewGuid().ToString(),
                PageName = moduleName + "List",
                PageTitle = moduleName,
                ParentId = parentId,
                LayoutId = "ffd20d14-0b22-4956-b0b3-4eebceca091b",
                PageType = "1"
            };
            var editpageEntity = new PageEntity()
            {
                PageId = Guid.NewGuid().ToString(),
                PageName = moduleName + "Edit",
                PageTitle = moduleName,
                ParentId = parentId,
                LayoutId = "ffd20d14-0b22-4956-b0b3-4eebceca091b",
                PageType = "1"
            };
            var service = new PageEntityService();
            if (service.IfExist(listpageEntity.PageName) || service.IfExist(editpageEntity.PageName))
            {
                throw new CustomException("PageName已存在");
            }
            IDictionary<PageEntity, List<Type>> pageControlMapping = new Dictionary<PageEntity, List<Type>>();
            var listTypes = new List<Type>() { typeof(SummerFresh.Controls.Table), typeof(Toolbar), typeof(Search) };
            var formTypes = new List<Type>() { typeof(Form) };
            pageControlMapping.Add(listpageEntity, listTypes);
            pageControlMapping.Add(editpageEntity, formTypes);
            foreach (var key in pageControlMapping.Keys)
            {
                service.Insert(key);
                foreach (var t in pageControlMapping[key])
                {
                    var i = Activator.CreateInstance(t) as IAutoGenerate;
                    if (i != null)
                    {
                        var instance = i.Generate(fields, moduleName);
                        CRUDComponentHelper.RecSave(key.PageId, key.PageId, instance, "Right");
                    }
                }
            }
        }


        public JsonResult RebuildCRUD()
        {
            string crudId = Request["CRUDId"];
            var service = new CRUDEntityService();
            CRUDEntity entity = service.Get(crudId) as CRUDEntity;
            if (entity == null)
            {
                throw new Exception("CRUD不存在");
            }
            if (entity.TableName.IsNullOrEmpty())
            {
                throw new Exception("TableName不存在，无法重生成");
            }
            var generator = SQLSyntaxGeneratorFactory.GetGenerator(entity.TableName);
            entity.SelectSQL = generator.GetSelectSQL();
            entity.InsertSQL = generator.GetInsertSQL();
            entity.UpdateSQL = generator.GetUpdateSQL();
            entity.DeleteSQL = generator.GetDeleteSQL();
            entity.GetOneSQL = generator.GetSelectOneSQL();
            entity.DefaultSortExpression = generator.GetDefaultSortExpression();
            entity.LastUpdateTime = DateTime.Now;
            service.Update(entity);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
