using SummerFresh.Business.Service;
using SummerFresh.Data;
using SummerFresh.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Serialization;
using SummerFresh.Basic;
using System.Threading;
namespace SummerFresh.Business.Entity
{

    [UnValidateInputeClass]
    [Serializable]
    [EntityService(typeof(PageEntityService))]
    [Table("App_Page")]
    public class PageEntity : CustomEntity
    {
        [TableField(IsShow = false)]
        [PrimaryKey]
        public string PageId { get; set; }

        [DisplayName("页面地址")]
        [Validator("required,length[1,50]")]
        public string PageName { get; set; }

        [TitleField]
        [DisplayName("页面标题")]
        [Validator("required,unique")]
        public string PageTitle { get; set; }

        [DisplayName("页面服务类")]
        [TypeDataSource(typeof(IPageService))]
        [FormField(ControlType = ControlType.DropDownList)]
        public string PageService { get; set; }

        [TableField(IsShow = false)]
        [DisplayName("页面CSS")]
        [TabItem]
        [FormField(ControlType = ControlType.TextArea)]
        public string PageStyle { get; set; }

        [TableField(IsShow = false)]
        [DisplayName("页面起始JS")]
        [TabItem]
        [FormField(ControlType = ControlType.TextArea)]
        public string PageStartUpScript { get; set; }

        [TableField(IsShow = false)]
        [DisplayName("页面JS方法定义")]
        [TabItem]
        [FormField(ControlType = ControlType.TextArea)]
        public string PageScriptBlock { get; set; }

        [DisplayName("页面布局类型")]
        [CustomEntityDataSource(typeof(LayoutEntity))]
        [FormField(ControlType = ControlType.DropDownList)]
        public string LayoutId { get; set; }

        [FunctionDataSource(typeof(CustomPageDataSource))]
        [FormField(ControlType = ControlType.DropDownList)]
        [SearchField]
        [DisplayName("页面所属父级")]
        public string ParentId { get; set; }

        [DictionaryDataSource("PageType")]
        [FormField(ControlType = ControlType.DropDownList)]
        [DisplayName("页面类型")]
        public string PageType { get; set; }

        [DisplayName("最后更新时间")]
        [FormField(Editable = false)]
        public DateTime LastUpdateTime { get; set; }

        [FormField(Editable = false)]
        [TableField(IsShow = false)]
        [Column(Insert = false, Update = false)]
        public List<ComponentEntity> Components
        {
            get;
            set;
        }

        [TableField(IsShow = false)]
        [FormField(Editable = false)]
        public LayoutEntity Layout
        {
            get;
            set;
        }

        [TableField(IsShow = false)]
        [FormField(Editable = false)]
        public List<ExternalFileEntity> ExtFiles
        {
            get;
            set;
        }
    }

    public class PageEntityService : CustomEntityService
    {
        public PageEntityService()
        {
            EntityType = typeof(PageEntity);
        }

        public override object Get(object id)
        {
            var pages = (DataSource as EntityDataSource).TableSource.GetAll().ToEntities<PageEntity>();
            var pageEntity = pages.FirstOrDefault(o => o.PageId.Equals(id.ToString(), StringComparison.CurrentCultureIgnoreCase) || o.PageName.Equals(id.ToString(), StringComparison.CurrentCultureIgnoreCase));
            var temp = new EntityDataSource(typeof(ComponentEntity)).TableSource.GetAll().ToEntities<ComponentEntity>();
            var componentEntities = temp.Where(o => o.PageId.Equals(pageEntity.PageId, StringComparison.CurrentCultureIgnoreCase)).ToList();
            pageEntity.Components = componentEntities;

            pageEntity.Layout = new LayoutEntityService().Get(pageEntity.LayoutId) as LayoutEntity;

            var extFiles = new EntityDataSource(typeof(ExternalFileEntity)).GetList().ToEntities<ExternalFileEntity>();
            var pageFiles = new EntityDataSource(typeof(PageFileEntity)).GetList().ToEntities<PageFileEntity>().Where(o => o.PageId.Equals(pageEntity.PageId, StringComparison.CurrentCultureIgnoreCase)).Select(o => o.FileId);
            pageEntity.ExtFiles = extFiles.Where(o => pageFiles.Contains(o.FileId, StringComparer.Create(Thread.CurrentThread.CurrentCulture, true))).ToList();

            return pageEntity;
        }

        public override int Delete(object entity)
        {
            var service = new ComponentEntityService();
            service.EntityType = typeof(ComponentEntity);
            var componentIds = Dao.Get().QueryScalarList<string>("SummerFresh.Business.Entity.PageEntity.SelectComponentId", new { PageId = entity });
            foreach (var id in componentIds)
            {
                service.Delete(id);
            }
            Dao.Get().ExecuteNonQuery("SummerFresh.Business.Entity.PageFileEntity.DeleteByPageId", new { PageId = entity });
            return base.Delete(entity);
        }

        public IList<PageEntity> GetByLastUpdateTime(DateTime lastUpdateTime)
        {
            return Dao.Get().QueryEntities<PageEntity>("SummerFresh.Business.Entity.PageEntity.GetByLastUpdateTime", new { LastUpdateTime = lastUpdateTime });
        }

        public bool IfExist(string pageName)
        {
            var pageEntity = new PageEntity() { PageName = pageName };
            return IfExist(pageEntity);

        }

        private bool IfExist(PageEntity entity)
        {
            return (Dao.Get().QueryScalar<int>("SummerFresh.Business.Entity.PageEntity.IfExist", entity) > 0);
        }

        public override int Insert(CustomEntity entity)
        {
            if (IfExist(entity as PageEntity))
            {
                throw new Exception("页面地址已存在！");
            }
            (entity as PageEntity).LastUpdateTime = DateTime.Now;
            return base.Insert(entity);
        }

        public override int Update(CustomEntity entity)
        {
            if (IfExist(entity as PageEntity))
            {
                throw new Exception("页面地址已存在！");
            }
            (entity as PageEntity).LastUpdateTime = DateTime.Now;
            return base.Update(entity);
        }
    }

    public class CustomPageDataSource:IKeyValueDataSource
    {

        public IList<SelectListItem> SelectItems()
        {
            return Dao.Get().QueryEntities<SelectListItem>("SummerFresh.Business.Entity.CustomPage.Select");
        }

        public object Converter(string columnName, object columnValue, IDictionary<string, object> rowData)
        {
            var items = SelectItems();
            if (!items.IsNullOrEmpty())
            {
                var item = items.FirstOrDefault(o => o.Value.Equals(columnValue.ToString(), StringComparison.OrdinalIgnoreCase));
                if (item != null)
                {
                    return item.Text;
                }
            }
            return columnValue;
        }

        public string ID
        {
            get;
            set;
        }
    }



    [DisplayName("布局外置文件")]
    [Table("APP_LayoutFile")]
    public class LayoutFileEntity : CustomEntity
    {
        [PrimaryKey]
        [TableField(IsShow = false)]
        public string LayoutFileId { get; set; }

        [DisplayName("布局")]
        [CustomEntityDataSource(typeof(LayoutEntity))]
        [FormField(ControlType = ControlType.DropDownList)]
        [SearchField(IsSearchControl = false)]
        public string LayoutId { get; set; }

        [DisplayName("文件")]
        [CustomEntityDataSource(typeof(ExternalFileEntity))]
        [FormField(ControlType = ControlType.DropDownList)]
        [SearchField(IsSearchControl = false)]
        public string FileId { get; set; }
    }

    [DisplayName("页面外置文件")]
    [Table("APP_PageFile")]
    public class PageFileEntity : CustomEntity
    {
        [PrimaryKey]
        [TableField(IsShow = false)]
        public string PageFileId { get; set; }

        [DisplayName("页面")]
        [CustomEntityDataSource(typeof(PageEntity))]
        [FormField(ControlType = ControlType.DropDownList)]
        [SearchField(IsSearchControl=false)]
        public string PageId { get; set; }

        [DisplayName("文件")]
        [CustomEntityDataSource(typeof(ExternalFileEntity))]
        [FormField(ControlType = ControlType.DropDownList)]
        [SearchField(IsSearchControl = false)]
        public string FileId { get; set; }
    }
}
