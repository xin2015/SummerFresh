using SummerFresh.Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SummerFresh.Basic;
using SummerFresh.Controls;
using SummerFresh.Data;
using SummerFresh.Business;
using System.Web.Script.Serialization;
using SummerFresh.Util;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using SummerFresh.Business.Service;
using System.Threading;
using System.ComponentModel;
using System.Collections;
using Newtonsoft.Json;
namespace SummerFresh.MVC.Controllers
{

    public class ComponentListModel
    {
        public string ComponentTypeFullName { get; set; }

        public string ComponentTypeName { get; set; }

        public string ComponentImage { get; set; }

        public string ComponentName { get; set; }

        public string CssClass { get; set; }

        public string Type { get; set; }
    }

    public class PageDesignerController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ComponentList()
        {
            string baseTypeName = Request.QueryString["baseType"];
            string parentId = Request.QueryString["parentId"];
            string targetId = Request.QueryString["targetId"];
            string typeName = Request.QueryString["typeName"];
            if (!parentId.IsNullOrEmpty() && !targetId.IsNullOrEmpty() && Request.QueryString["FormViewMode"] != "Insert")
            {
                var component = Dao.Get().QueryEntity<ComponentEntity>("SummerFresh.Business.Entity.PageController.Select");
                if (component != null)
                {
                    typeName = component.ComponentType;
                }
            }
            Type baseType = null;
            List<Type> types = null;
            if (!baseTypeName.IsNullOrEmpty())
            {
                baseType = TypeHelper.GetType(baseTypeName);
            }
            else
            {
                baseType = typeof(IControl);
            }
            if (baseType.IsInterface)
            {
                types = TypeHelper.GetAllImpl(baseType);
            }
            else
            {
                types = TypeHelper.GetAllSubType(baseType);
            }
            var models = new List<ComponentListModel>();
            string tempImage = string.Empty;
            Type filter = null;
            string type = string.Empty;
            foreach (Type t in types)
            {
                if (t.IsAbstract) continue;
                tempImage = "../../Content/images/Controls/SummerFresh.Controls.Control.png";
                if (System.IO.File.Exists(Server.MapPath("~/Content/images/Controls/{0}.png".FormatTo(t.FullName))))
                {
                    tempImage = "../../Content/images/Controls/{0}.png".FormatTo(t.FullName);
                }
                var attr = t.GetCustomAttribute<CategoryAttribute>(true);
                if (attr != null)
                {
                    type = attr.Category;
                }
                else
                {
                    type = (t.FullName.StartsWith("SummerFresh.Controls") ||t.FullName.StartsWith("SummerFresh.Business")) ? "系统" : "自定义";
                }
                models.Add(new ComponentListModel()
                {
                    ComponentImage = tempImage,
                    ComponentTypeFullName = t.FullName,
                    ComponentTypeName = t.Name,
                    ComponentName = t.GetDisplayName(),
                    CssClass = (t.FullName == typeName ? "component-item-selected" : ""),
                    Type = type
                });
            }
            if (models.Count == 1)
            {
                models[0].CssClass = "component-item-selected";
            }
            return View(models.OrderBy(o => o.ComponentName).ToList());
        }

        public ActionResult LayoutManagement(string id)
        {
            return View();
        }


        private class InnerTabItemModel
        {
            public string ID { get; set; }

            public string TabName { get; set; }

            public bool ContentScrolling { get; set; }

            public string ContentSrc { get; set; }
 
        }

        public ActionResult Pagemanagement(string id)
        {
            string type = Request.QueryString["type"];
            string parentName = Request.QueryString["parentName"];
            var tab = new Tab();
            List<InnerTabItemModel> tabItemList = new List<InnerTabItemModel>();
            if (!id.Equals("null"))
            {
                tabItemList.Add(new InnerTabItemModel() { ID = "PageInfo", TabName = "基本信息",ContentSrc="/Entity/Edit/Page/" + id ,ContentScrolling=true });
            }
            if (type == "0")
            {
                string iframeSrc = "/Entity/List/Page?PageType=0";
                if (!id.Equals("null"))
                {
                    iframeSrc = "/Entity/List/Page?ParentId=" + id;
                }
                tabItemList.Add(new InnerTabItemModel() { ID = "ChildrenInfo", TabName = "子级信息", ContentSrc = iframeSrc });
            }
            else
            {
                tabItemList.Add(new InnerTabItemModel() { ID = "ComponentInfo", TabName = "组件管理", ContentSrc = "/Entity/List/Component?PageId=" + id + "&ParentId=" + id + "&parentName=" + parentName });
                tabItemList.Add(new InnerTabItemModel() { ID = "ExtFileInfo", TabName = "外置文件", ContentSrc = "/Entity/List/PageFile?PageId=" + id });
                tabItemList.Add(new InnerTabItemModel { ID = "Design", TabName = "设计", ContentSrc = "/Page/PreView/" + id + "?PageId=" + id + "&parentName=" + parentName });
                tabItemList.Add(new InnerTabItemModel { ID = "PreView", TabName = "预览", ContentSrc = "/Page/" + parentName });
                tabItemList.Add(new InnerTabItemModel { ID = "Other", TabName = "其它", ContentSrc = "/PageDesigner/ImportExport/" + id });
            }
            TabItem tabItem;
            foreach (var item in tabItemList)
            {
                tabItem = new TabItem { ID=item.ID,Visiable=true,TabName=item.TabName };
                tabItem.Content.Add(new IFrame() { Scrolling=item.ContentScrolling, Src=item.ContentSrc,ID=item.ID+"Iframe" });
                tab.TabItems.Add(tabItem);
            }
            return View(tab);
        }

        [HttpPost]
        public JsonResult ComponentDelete()
        {
            bool result = false;
            string componentId = Request["componentId"];
            if (!componentId.IsNullOrEmpty())
            {
                var service = new ComponentEntityService();
                result = service.Delete(componentId) > 0;
            }
            return Json(result);
        }

        public ActionResult ComponentEditor()
        {
            var model = new FormModel();
            string componentId = Request.QueryString["componentId"];
            string typeName = Request.QueryString["typeName"];
            string pageId = Request.QueryString["pageId"];
            string parentName = Request.QueryString["parentName"];
            string parentId = Request.QueryString["parentId"];
            string targetId = Request.QueryString["targetId"];
            Type type = null;
            object instance = null;
            ComponentEntity component = null;

            if (componentId.IsNullOrEmpty())
            {
                if (!parentId.IsNullOrEmpty() && !targetId.IsNullOrEmpty() && Request.QueryString["FormViewMode"] != "Insert")
                {
                    component = Dao.Get().QueryEntity<ComponentEntity>("SummerFresh.Business.Entity.PageController.Select");
                }
            }
            else
            {
                component = Dao.Get().Select<ComponentEntity>(componentId);
            }
            if (component != null)
            {
                componentId = component.ComponentId;
                pageId = component.PageId;
                parentId = component.ParentId;
                if (typeName.IsNullOrEmpty() || typeName.Equals(component.ComponentType))
                {
                    type = TypeHelper.GetType(component.ComponentType);
                    if (!component.ComponentXML.IsNullOrEmpty())
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        serializer.RegisterConverters(new[] { new SummerFresh.Business.ComponentConverter() });
                        instance = serializer.Deserialize(component.ComponentXML, type);
                        if (instance is IControl)
                        {
                            (instance as IControl).ID = component.ComponentName;
                        }
                    }
                    typeName = type.FullName;
                    model.Tab = EntityComponentHelper.GetTabComponent(instance, componentId, pageId);
                }
            }
            if (typeName.IsNullOrEmpty())
            {
                throw new CustomException("需要typeName或componentId参数");
            }
            type = TypeHelper.GetType(typeName);
            if (parentId.IsNullOrEmpty())
            {
                parentId = pageId;
            }

            if (instance == null)
            {
                instance = Activator.CreateInstance(type);
                if (instance is IControl && !parentName.IsNullOrEmpty())
                {
                    (instance as IControl).ID = parentName + type.Name;
                }
            }
            model.EntityName = type.FullName;
            model.Form = EntityComponentHelper.GetFormComponent(type);
            model.Form.PostUrl = "/PageDesigner/ComponentEditor?typeName={0}&componentId={1}&pageId={2}&parentId={3}&targetId={4}".FormatTo(typeName, componentId, pageId, parentId, targetId);
            model.Form.Data = instance;
            if (model.Tab != null)
            {
                if (model.Tab.TabItems.Count == 1)
                {
                    model.Tab = null;
                }
                else
                {
                    model.Tab.TabItems[0].Content.Add(model.Form);
                }
            }
            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public JsonResult ComponentEditor(IControl entity)
        {
            string componentId = Request.QueryString["componentId"];
            string typeName = Request.QueryString["typeName"];
            string pageId = Request.QueryString["pageId"];
            string parentId = Request.QueryString["parentId"];
            string targetId = Request.QueryString["targetId"];
            componentId = CRUDComponentHelper.SaveComponent(entity, componentId, pageId, parentId, targetId);
            return Json(componentId);
        }

        public JsonResult MergeTableColumn()
        {
            var ids = Request["ids"].Split(',');
            var components = new EntityDataSource(typeof(ComponentEntity)).TableSource.GetAll().ToEntities<ComponentEntity>();
            var mergeColumns = components.Where(o => ids.Contains(o.ComponentId, StringComparer.Create(Thread.CurrentThread.CurrentCulture, true))).OrderBy(o => o.Rank);
            var c = mergeColumns.FirstOrDefault();
            int i = 1;
            string newColumnId = "columnMerge{0}";
            var columns = components.Where(o => o.ParentId.Equals(c.ParentId, StringComparison.CurrentCultureIgnoreCase) && o.TargetId.Equals(c.TargetId, StringComparison.CurrentCultureIgnoreCase));
            while (columns.Count(o => o.ComponentName.Equals(newColumnId.FormatTo(i), StringComparison.CurrentCultureIgnoreCase)) > 0)
            {
                i++;
            }
            var tempColumn = new TableColumn()
            {
                ColumnName = "合并列{0}".FormatTo(i),
                FieldName = "MergeColumn{0}".FormatTo(i),
                ID = newColumnId.FormatTo(i),
                Visiable = true,
                Rank = i
            };
            var newParentId = CRUDComponentHelper.SaveComponent(tempColumn, "", c.PageId, c.ParentId, c.TargetId);
            var service = new ComponentEntityService();
            int j = 1;
            foreach (var m in mergeColumns)
            {
                m.ParentId = newParentId;
                m.TargetId = "Children";
                m.Rank = i * 10 + (j++);
                service.Update(m);
            }
            return Json(true);
        }

        public JsonResult GetTableColumn(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(false);
            }
            string pageId = Request["pageId"];
            var component = CRUDComponentHelper.GetComponent(id, System.Web.HttpContext.Current.Request);
            if (component is Table)
            {
                var table = component as Table;
                if (table.DataSource == null)
                {
                    throw new CustomException("请先给Table设置数据源");
                }
                var data = table.GetData();
                if (data.IsNullOrEmpty())
                {
                    throw new CustomException("数据源返回0行数据");
                }
                table.GenerateTableColumn(data);
                string parentId = string.Empty;
                foreach (var column in table.Columns)
                {
                    CRUDComponentHelper.RecSave(pageId, id, column, "Columns");
                }
                return Json(true);
            }
            throw new CustomException("当前只支持Table组件");
        }


        public JsonResult CopyComponent(string sid, string componentId, string gid, string gpId)
        {
            if (sid.IsNullOrWhiteSpace())
            {
                throw new CustomException("缺少页面名称");
            }
            if (componentId.IsNullOrWhiteSpace())
            {
                throw new CustomException("缺少控件名称");
            }
            PageEntity pageEntity = new PageEntityService().Get(sid) as PageEntity;
            string[] comIdArr = componentId.Split(',');
            Dictionary<ComponentEntity, List<ComponentEntity>> pcMapping = new Dictionary<ComponentEntity, List<ComponentEntity>>();
            List<ComponentEntity> compLst = pageEntity.Components.Where(c => comIdArr.Contains(c.ComponentName)).ToList();
            foreach (var component in compLst)
            {
                GetChileComponent(pageEntity.Components, component, pcMapping);
            }
            string newComponentId;
            CustomEntityService service = new CustomEntityService() { EntityType = typeof(ComponentEntity) };
            compLst.ForEach(c => c.ParentId = gid);
            foreach (var item in pcMapping)
            {
                newComponentId = Guid.NewGuid().ToString();
                item.Key.ComponentId = newComponentId;
                item.Key.ComponentName = item.Key.ComponentName + "_Copy";
                item.Key.PageId = gpId;
                service.Insert(item.Key);
                foreach (var child in item.Value)
                {
                    child.ParentId = newComponentId;
                    if (!pcMapping.ContainsKey(child))
                    {
                        child.ComponentId = Guid.NewGuid().ToString();
                        child.ComponentName = child.ComponentName + "_Copy";
                        child.PageId = gpId;
                        service.Insert(child);
                    }
                }
            }
            return Json(true);
        }

        private void GetChileComponent(List<ComponentEntity> componentLst, ComponentEntity parent, Dictionary<ComponentEntity, List<ComponentEntity>> pcMapping)
        {
            List<ComponentEntity> childs = componentLst.Where(c => c.ParentId == parent.ComponentId).ToList();
            if (childs.Count == 0)
                return;
            pcMapping[parent] = childs;
            childs.ForEach(c => GetChileComponent(componentLst, c, pcMapping));
        }

        public ActionResult ImportExport(string id)
        {

            var pageEntity = PageService.Get(id);
            ViewData["Page"] = pageEntity;
            return View();
        }

        private PageEntityService _pageService;
        private PageEntityService PageService
        {
            get
            {
                return _pageService ?? (_pageService = new PageEntityService() { EntityType = typeof(PageEntity) });
            }
        }

        public FileResult Export(string id)
        {
            var pageEntity = PageService.Get(id) as PageEntity;
            string contentType = "text/xml";

            using (MemoryStream ms = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(List<PageEntity>));
                string fileName = string.Format("{0}-{1}.xml", pageEntity.PageName, DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                fileName = HttpUtility.UrlEncode(fileName, Encoding.UTF8);
                serializer.Serialize(ms, new List<PageEntity>() { pageEntity });
                return File(ms.GetBuffer(), contentType, fileName);
            }
        }

        public FileResult ExportSystem()
        {
            List<Type> typeList = new List<Type>() {
                typeof(DataDictionaryEntity),
                typeof(DataDictionaryItemEntity),
                typeof(CRUDEntity),
                typeof(PageEntity),
                typeof(ExternalFileEntity),
                typeof(LayoutEntity),
                typeof(PageFileEntity),
                typeof(LayoutFileEntity),
                typeof(ComponentEntity),
                typeof(PermissionEntity),
            };

            string contentType = "text/xml";
            using (MemoryStream ms=new MemoryStream())
            {

                Dictionary<string, object> datas = new Dictionary<string, object>();
                CustomEntityService service;
                foreach (Type type in typeList)
                {
                    service = new CustomEntityService() { EntityType=type };
                    datas[type.Name + "Array"] = service.DataSource.GetList();
                }
                var serialize = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
                string str = serialize.Serialize(datas);
                byte[] byteArr = Encoding.Default.GetBytes(str);
                ms.Write(byteArr, 0, byteArr.Length);
                string fileName = string.Format("{0}-{1}.json", "SummerFresh", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));

                fileName = HttpUtility.UrlEncode(fileName, Encoding.UTF8);
                return File(ms.GetBuffer(), contentType, fileName);

            }

        }

        public FileResult ExportLayout(string id)
        {
            LayoutEntityService layoutService = new LayoutEntityService();
            LayoutEntity layoutEntity = layoutService.Get(id) as LayoutEntity;
            string contentType = "text/xml";
            using (MemoryStream ms = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(List<LayoutEntity>));
                string fileName = "{0}-{1}.xml".FormatTo(layoutEntity.LayoutName, DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                fileName = HttpUtility.UrlEncode(fileName, Encoding.UTF8);
                serializer.Serialize(ms, new List<LayoutEntity>() { layoutEntity });
                return File(ms.GetBuffer(), contentType, fileName);
            }
        }

        public ActionResult ImportXMLFile()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ImportLayout()
        {
            if (Request.Files.Count <= 0 || Request.Files[0].InputStream.Length <= 0) return Json(false);

            var serializer = new XmlSerializer(typeof(List<LayoutEntity>));
            var layouts = serializer.Deserialize(Request.Files[0].InputStream) as List<LayoutEntity>;

            layouts.ForEach(layout => ImportLayout(layout));
            return Json(true);
        }

        private void ImportLayout(LayoutEntity layout)
        {
            if (layout == null) return;
            LayoutEntityService layoutService = new LayoutEntityService();
            CustomEntityService layoutFileSerivce = new CustomEntityService() { EntityType = typeof(LayoutFileEntity) };
            CustomEntityService extFileService = new CustomEntityService() { EntityType = typeof(ExternalFileEntity) };

            List<LayoutEntity> layoutLst = layoutService.DataSource.GetList().ToEntities<LayoutEntity>().ToList();
            List<ExternalFileEntity> extFileLst = extFileService.DataSource.GetList().ToEntities<ExternalFileEntity>().ToList();
            List<LayoutFileEntity> layoutFileLst = layoutFileSerivce.DataSource.GetList().ToEntities<LayoutFileEntity>().ToList();

            ///新增布局文件
            if (layoutLst.Count(c => c.LayoutId == layout.LayoutId) <= 0)
                layoutService.Insert(layout);

            if (layout.ExtFiles != null)
            {
                //处理关联外置文件
                layout.ExtFiles.ForEach(extFile =>
                {
                    ///新增外置文件
                    if (extFileLst.Count(c => c.FileId == extFile.FileId) < 1)
                        extFileService.Insert(extFile);

                    ///新增关联
                    if (layoutFileLst.Count(c => c.LayoutId == layout.LayoutId && c.FileId == extFile.FileId) < 1)
                    {
                        layoutFileSerivce.Insert(new LayoutFileEntity()
                        {
                            FileId = extFile.FileId,
                            LayoutId = layout.LayoutId,
                            LayoutFileId = Guid.NewGuid().ToString(),
                        });
                    }
                });

            }
        }

        private void InnerImportPages(List<PageEntity> pages)
        {
            var componentService = new ComponentEntityService() { EntityType = typeof(ComponentEntity) };
            var extendFileService = new CustomEntityService() { EntityType = typeof(ExternalFileEntity) };
            var pageFileService = new CustomEntityService() { EntityType = typeof(PageFileEntity) };
            var allExtFiles = extendFileService.DataSource.GetList().ToEntities<ExternalFileEntity>();
            foreach (var page in pages)
            {
                PageService.Delete(page.PageId);
                PageService.Insert(page);

                foreach (var c in page.Components)
                {
                    componentService.Insert(c);
                }
                if (page.ExtFiles != null)
                {
                    page.ExtFiles.ForEach(extFile =>
                    {
                        //外置文件导入
                        if (allExtFiles.Count(o => o.FileId.Equals(extFile.FileId, StringComparison.OrdinalIgnoreCase)) == 0)
                        {
                            extendFileService.Insert(extFile);
                        }
                        //PageFile新增
                        PageFileEntity pfe = new PageFileEntity()
                        {
                            FileId = extFile.FileId,
                            PageFileId = Guid.NewGuid().ToString(),
                            PageId = page.PageId,
                        };
                        pageFileService.Insert(pfe);
                    });
                }

                if (page.Layout != null)
                    ImportLayout(page.Layout);
            }
        }

        public JsonResult ImportSystem()
        {
            if (Request.Files.Count <= 0 || Request.Files[0].InputStream.Length <= 0) return Json(false);

            var serializer = new JavaScriptSerializer() { MaxJsonLength=int.MaxValue };
            Stream s = Request.Files[0].InputStream;
           

            byte[] buffer = new byte[s.Length];
            s.Read(buffer, 0, buffer.Length);
            string json = Encoding.Default.GetString(buffer);
            Dictionary<string, object> data = serializer.Deserialize<Dictionary<string, object>>(json);

            List<Tuple<string, Type>> loopInfo = new List<Tuple<string, Type>>() 
            {
                new Tuple<string,Type>("DictionaryId",typeof(DataDictionaryEntity)),
                new Tuple<string,Type>("DictionaryItemId",typeof(DataDictionaryItemEntity)),
                new Tuple<string,Type>("CRUDId",typeof(CRUDEntity)),
                new Tuple<string,Type>("PageId",typeof(PageEntity)),
                new Tuple<string,Type>("LayoutId",typeof(LayoutEntity)),
                new Tuple<string,Type>("LayoutFileId",typeof(LayoutFileEntity)),
                new Tuple<string,Type>("PageFileId",typeof(PageFileEntity)),
                new Tuple<string,Type> ("ComponentId",typeof(ComponentEntity)),
                new Tuple<string,Type>("FileId",typeof(ExternalFileEntity)),
                new Tuple<string,Type>("PermissionId",typeof(PermissionEntity)),
            };
            string typeName;
            CustomEntityService service;
            bool result = true;
            foreach (var info in loopInfo)
            {
                typeName = info.Item2.Name;
                string key=typeName + "Array";
                if (!data.ContainsKey(key))
                {
                    continue;
                }
                IEnumerable enumerable = data[key] as IEnumerable;
                service = new CustomEntityService() { EntityType=info.Item2};

                foreach (var item in service.DataSource.GetList())
                {
                    try
                    {
                        service.Delete(item[info.Item1]);
                    }
                    catch (Exception ex)
                    {
                        result = false;
                    }
                }
                foreach (var dataItem in enumerable)
                {
                    try
                    {
                        service.Insert((dataItem as Dictionary<string, object>).ToEntity(info.Item2) as CustomEntity);
                    }
                    catch (Exception ex)
                    {
                        result = false;
                    }
                }
            }

            return Json(true);
        }


        public FileResult BatchExport()
        {
            DateTime lastUpdateTime = Request["LastUpdateTime"].IsNullOrEmpty() ? DateTime.Parse("2010-01-01 00:00:00") : Request["LastUpdateTime"].ConverTo<DateTime>();
            var pageEntities = PageService.GetByLastUpdateTime(lastUpdateTime);
            if (!pageEntities.IsNullOrEmpty())
            {
                var pageEntity = pageEntities.FirstOrDefault();
                string contentType = "text/xml";
                using (MemoryStream ms = new MemoryStream())
                {
                    var serializer = new XmlSerializer(typeof(List<PageEntity>));
                    string fileName = string.Format("{0}.xml", lastUpdateTime.ToString("yyyy-MM-dd-HH-mm-ss"));
                    fileName = HttpUtility.UrlEncode(fileName, Encoding.UTF8);
                    serializer.Serialize(ms, pageEntities);
                    return File(ms.GetBuffer(), contentType, fileName);
                }
            }
            throw new CustomException("没有可导出的页面！");
        }

        public JsonResult Import()
        {
            if (Request.Files.Count > 0)
            {
                if (Request.Files[0].InputStream.Length > 0)
                {
                    var serializer = new XmlSerializer(typeof(List<PageEntity>));
                    var pages = serializer.Deserialize(Request.Files[0].InputStream) as List<PageEntity>;
                    InnerImportPages(pages);
                    return Json(true);
                }
            }
            return Json(false);
        }

        public JsonResult Copy(string id)
        {
            if (!id.IsNullOrEmpty())
            {
                var pageEntity = PageService.Get(id) as PageEntity;
                var page = PageBuilder.BuildPage(pageEntity.PageId, System.Web.HttpContext.Current.Request);
                pageEntity.PageId = Guid.NewGuid().ToString();
                int i = 1;
                string originalName = pageEntity.PageName;
                pageEntity.PageName = "{0}_Copy_{1}".FormatTo(originalName, i);
                while (PageService.IfExist(pageEntity.PageName))
                {
                    pageEntity.PageName = "{0}_Copy_{1}".FormatTo(originalName, ++i);
                }
                pageEntity.PageTitle = "{0}_复制_{1}".FormatTo(pageEntity.PageTitle, i);
                PageService.Insert(pageEntity);
                foreach (var key in page.Children.Keys)
                {
                    foreach (var control in page.Children[key])
                    {
                        CRUDComponentHelper.RecSave(pageEntity.PageId, pageEntity.PageId, control, key);
                    }
                }
                CustomEntityService extFileService = new CustomEntityService() { EntityType = typeof(PageFileEntity) };
                foreach (var extFile in page.ExtFiles)
                {

                    extFileService.Insert(new PageFileEntity()
                    {
                        FileId = extFile.FileId,
                        PageFileId = Guid.NewGuid().ToString(),
                        PageId = pageEntity.PageId,
                    });
                }
                return Json(true);
            }
            return Json(false);
        }

        public JsonResult CopyLayout(string id)
        {
            var service = new LayoutEntityService();
            LayoutEntity layout = service.Get(id) as LayoutEntity;
            LayoutEntity newLayout = new LayoutEntity()
            {
                LayoutId = Guid.NewGuid().ToString(),
                LastUpdateTime = DateTime.Now,
                LayoutHtml = layout.LayoutHtml,
                LayoutName = layout.LayoutName + "_复制",
                LayoutStartUpScript = layout.LayoutStartUpScript
            };
            service.Insert(newLayout);
            var fileService = new CustomEntityService() { EntityType = typeof(LayoutFileEntity) };
            foreach (var extFile in layout.ExtFiles)
            {
                var layoutFile = new LayoutFileEntity()
                {
                    FileId = extFile.FileId,
                    LayoutFileId = Guid.NewGuid().ToString(),
                    LayoutId = newLayout.LayoutId
                };
                fileService.Insert(layoutFile);
            }
            return Json(true);
        }

        public JsonResult CleanCache(string id)
        {
            return Json(CacheHelper.Remove(NamingCenter.GetCacheKey(CacheType.PAGE_CONFIG, id)));
        }
    }
}
