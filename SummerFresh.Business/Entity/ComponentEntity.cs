using SummerFresh.Business.Service;
using SummerFresh.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using SummerFresh.Basic;
using SummerFresh.Data;
namespace SummerFresh.Business.Entity
{
    [UnValidateInputeClass]
    [Serializable]
    [EntityService(typeof(ComponentEntityService))]
    [Table("APP_Component")]
    public class ComponentEntity : CustomEntity
    {
        [TableField(IsShow = false)]
        [PrimaryKey]
        public string ComponentId { get; set; }

        [CustomEntityDataSource(typeof(PageEntity))]
        [FormField(ControlType = ControlType.DropDownList)]
        [DisplayName("所属页面")]
        [SearchField(IsSearchControl = false)]
        [TableField(IsShow = false)]
        public string PageId { get; set; }

        [TableField(IsShow = false)]
        [DisplayName("所属父级")]
        [SearchField(IsSearchControl = false)]
        public string ParentId { get; set; }

        [DisplayName("组件名称")]
        public string ComponentName { get; set; }

        [DisplayName("组件类型")]
        public string ComponentType { get; set; }

        [DisplayName("组件XML")]
        [TableField(IsShow = false)]
        public string ComponentXML { get; set; }

        /// <summary>
        /// 组件数据类型
        /// </summary>
        [DisplayName("组件数据类型")]
        [FormField(Editable = false)]
        [TableField(IsShow = false)]
        public string ComponentDataType { get; set; }

        [DisplayName("排序号")]
        [DefaultSortField(Business.OrderByType.ASC)]
        [FormField(Editable = false)]
        public int? Rank { get; set; }

        [DisplayName("目标ID")]
        [DefaultSortField(Business.OrderByType.ASC)]
        [SearchField(IsSearchControl = false)]
        public string TargetId { get; set; }

        [DisplayName("最后更新时间")]
        [FormField(Editable = false)]
        [TableField(IsShow = false)]
        public DateTime LastUpdateTime { get; set; }
    }

    public class ComponentEntityService : CustomEntityService
    {
        public override int Insert(CustomEntity entity)
        {
            var component = entity as ComponentEntity;
            component.LastUpdateTime = DateTime.Now;
            UpdatePage(component);
            return base.Insert(component);
        }

        private void UpdatePage(ComponentEntity component)
        {
            Dao.Get().ExecuteNonQuery("SummerFresh.Business.Entity.ComponentEntity.UpdatePage", new { PageId = component.PageId });
        }

        public override int Update(CustomEntity entity)
        {
            var component = entity as ComponentEntity;
            component.LastUpdateTime = DateTime.Now;
            UpdatePage(component);
            return base.Update(component);
        }

        public ComponentEntityService()
        {
            EntityType = typeof(ComponentEntity);
        }
        public override IList<ButtonEntity> AddButtons()
        {
            var buttons = base.AddButtons();
            var button = buttons.FirstOrDefault(o => o.ButtonControlId == "btnEdit");
            if (button != null)
            {
                button.OnClick = "$.showModalDialog({{url:'/PageDesigner/ComponentEditor?componentId={0}',overlayClose:true,width:950,height:600}});";
                button.DataFields = "ComponentId";
            }
            var insertButton = buttons.FirstOrDefault(o => o.ButtonControlId == "btnInsert");
            if (insertButton != null)
            {
                insertButton.OnClick = "showAddComponent()";
            }
            buttons.Add(new ButtonEntity() { ButtonControlId = "btnEdit", ButtonName = "编辑信息", CssClass = "btn btn-warning btn-sm", ButtonType = ButtonEntityType.TableRow });
            buttons.Add(new ButtonEntity() { ButtonControlId = "btnGetFromDs", OnClick = "getFromDs()", ButtonName = "从数据源获取列", CssClass = "btn btn-primary", ButtonType = ButtonEntityType.Toolbar });
            buttons.Add(new ButtonEntity() { ButtonControlId = "btnMergeColumn", OnClick = "mergeColumn()", ButtonName = "合并列", CssClass = "btn btn-primary", ButtonType = ButtonEntityType.Toolbar });
            return buttons;
        }
    }

}
