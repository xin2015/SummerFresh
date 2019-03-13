using SummerFresh.Business.Service;
using SummerFresh.Data;
using SummerFresh.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SummerFresh.Business.Entity
{
    [Table("APP_CRUD")]
    [EntityService(typeof(CRUDEntityService))]
    public class CRUDEntity : CustomEntity
    {
        [PrimaryKey]
        [TableField(IsShow = false)]
        public string CRUDId { get; set; }

        [TitleField]
        [ValueField]
        [Validator("required")]
        public string CRUDName { get; set; }

        [DisplayName("主键名")]
        public string PKName { get; set; }

        [TableField(IsShow = false)]
        [DisplayName("标题字段")]
        public string TitleFieldName { get; set; }

        [TableField(IsShow = false)]
        public string ValueFieldName { get; set; }

        [TableField(IsShow = false)]
        public string ParentFieldName { get; set; }

        [DisplayName("表名")]
        public string TableName { get; set; }

        [TableField(IsShow = false)]
        [TabItem]
        [FormField(ControlType = ControlType.TextArea)]
        public string InsertSQL { get; set; }

        [TableField(IsShow = false)]
        [TabItem]
        [FormField(ControlType = ControlType.TextArea)]
        public string UpdateSQL { get; set; }

        [TableField(IsShow = false)]
        [TabItem]
        [FormField(ControlType = ControlType.TextArea)]
        public string DeleteSQL { get; set; }

        [TableField(IsShow = false)]
        [TabItem]
        [FormField(ControlType = ControlType.TextArea)]
        public string GetOneSQL { get; set; }

        [TableField(IsShow = false)]
        [TabItem]
        [FormField(ControlType = ControlType.TextArea)]
        public string SelectSQL { get; set; }

        [TableField(IsShow = false)]
        public string DefaultSortExpression { get; set; }

        [DisplayName("最后更新时间")]
        [FormField(Editable = false)]
        [TableField(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime LastUpdateTime { get; set; }
    }

    public class CRUDEntityService : CustomEntityService
    {
        public CRUDEntityService()
        {
            EntityType = typeof(CRUDEntity);
        }

        public override IList<ButtonEntity> AddButtons()
        {
            var res= base.AddButtons();
            res.Add(new ButtonEntity() { ButtonControlId = "btnRebuild", ButtonName = "重新生成", CssClass = "btn btn-primary btn-sm", ButtonType = ButtonEntityType.TableRow,
                                         OnClick = "summerFresh.dataService('/CRUD/RebuildCRUD/',{{CRUDId:'{0}'}},function(res){{if(res){{alert('重新生成成功！')}}}})",
                                         DataFields = "CRUDId",
            });
            return res;
        }

        public override int Insert(CustomEntity entity)
        {
            var crudEntity = entity as CRUDEntity;
            var ds = new EntityDataSource(typeof(CRUDEntity)).Get(crudEntity.CRUDName);
            if (ds != null)
            {
                throw new CustomException("CRUDName不允许重复！");
            }
            crudEntity.LastUpdateTime = DateTime.Now;
            return base.Insert(crudEntity);
        }

        public override int Update(CustomEntity entity)
        {
            var crudEntity = entity as CRUDEntity;
            crudEntity.LastUpdateTime = DateTime.Now;
            return base.Update(crudEntity);
        }
    }
}
