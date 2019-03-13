using SummerFresh.Business.Service;
using SummerFresh.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using System.Threading;
namespace SummerFresh.Business.Entity
{

    [UnValidateInputeClass]
    [Serializable]
    [EntityService(typeof(LayoutEntityService))]
    [Table("APP_Layout")]
    public class LayoutEntity : CustomEntity
    {
        [PrimaryKey]
        [TableField(IsShow = false)]
        public string LayoutId { get; set; }

        [TitleField]
        [DisplayName("布局名称")]
        public string LayoutName { get; set; }


        [DisplayName("布局HTML")]
        [FormField(ControlType = ControlType.TextEditor)]
        [TableField(HtmlEncode=true,ShowLength=20)]
        [TabItem]
        public string LayoutHtml { get; set; }

        [DisplayName("布局启动脚本")]
        [TabItem]
        [TableField(ShowLength = 20)]
        [FormField(ControlType = ControlType.TextArea)]
        public string LayoutStartUpScript { get; set; }

        [DisplayName("布局样式")]
        [TabItem]
        [TableField(IsShow=false)]
        [FormField(ControlType = ControlType.TextArea)]
        public string LayoutPageStyle { get; set; }

        [DisplayName("最后更新时间")]
        [FormField(Editable = false)]
        public DateTime LastUpdateTime { get; set; }


        [TableField(IsShow = false)]
        [FormField(Editable = false)]
        public List<ExternalFileEntity> ExtFiles
        {
            get;
            set;
        }
    }

    public class LayoutEntityService : CustomEntityService
    {
        public override object Get(object id)
        {
            var layouts = (DataSource as EntityDataSource).TableSource.GetAll().ToEntities<LayoutEntity>();
            var layout = layouts.FirstOrDefault(o => o.LayoutId.Equals(id.ToString(), StringComparison.CurrentCultureIgnoreCase));
            if(layout!=null)
            {
                var extFiles = new EntityDataSource(typeof(ExternalFileEntity)).GetList().ToEntities<ExternalFileEntity>();
                var layoutFiles = new EntityDataSource(typeof(LayoutFileEntity)).GetList().ToEntities<LayoutFileEntity>().Where(o => o.LayoutId.Equals(layout.LayoutId, StringComparison.CurrentCultureIgnoreCase)).Select(o => o.FileId);
                layout.ExtFiles = extFiles.Where(o => layoutFiles.Contains(o.FileId, StringComparer.Create(Thread.CurrentThread.CurrentCulture, true))).ToList();
            }
            return layout;
        }

        public LayoutEntityService()
        {
            EntityType = typeof(LayoutEntity);
        }

        public override IList<ButtonEntity> AddButtons()
        {
            var buttons = base.AddButtons();
            buttons.Add(new ButtonEntity()
            {
                ButtonControlId = "btnLayoutFile",
                ButtonName = "关联外置文件",
                OnClick = "$.showModalDialog({{url:'/Entity/List/LayoutFile?LayoutId={0}',overlayClose:true,width:950,height:600}});",
                DataFields = "LayoutId",
                CssClass="btn btn-warning btn-sm",
                ButtonType = ButtonEntityType.TableRow
            });
            buttons.Add(new ButtonEntity() {
                ButtonControlId = "btnCopyLayoutFile",
                ButtonName = "另存副本",
                OnClick = "summerFresh.dataService('/PageDesigner/CopyLayout/{0}',{{}},function(res){{if(res){{alert('另存副本成功！')}}}})",
                DataFields = "LayoutId",
                CssClass = "btn btn-warning btn-sm",
                ButtonType = ButtonEntityType.TableRow
            });
            buttons.Add(new ButtonEntity() { 
                ButtonControlId="btnExportLayout",
                ButtonName="导出布局",
                OnClick = "window.open('/PageDesigner/ExportLayout/{0}')",
                DataFields = "LayoutId",
                CssClass = "btn btn-warning btn-sm",
                ButtonType=ButtonEntityType.TableRow,
            });
            buttons.Add(new ButtonEntity()
            {
                ButtonControlId = "btnExportLayout",
                ButtonName = "导入布局",
                OnClick = "$.showModalDialog({url:'/PageDesigner/ImportXMLFile?xmltype=layout',overlayClose:true,width:950,height:600});",
                CssClass = "btn btn-warning btn-sm",
                ButtonType = ButtonEntityType.Toolbar,
            });
            return buttons;
        }

        public override int Insert(CustomEntity entity)
        {
            (entity as LayoutEntity).LastUpdateTime = DateTime.Now;
            return base.Insert(entity);
        }

        public override int Update(CustomEntity entity)
        {
            (entity as LayoutEntity).LastUpdateTime = DateTime.Now;
            return base.Update(entity);
        }
    }
}
