using SummerFresh.Business.Service;
using SummerFresh.Data;
using SummerFresh.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web.Mvc;
using SummerFresh.Basic;
namespace SummerFresh.Business.Entity
{
    [Tree(IdParameterName = "ParentId")]
    [EntityService(typeof(PermissionEntityService))]
    [Table("SYS_Permission")]
    public class PermissionEntity : CustomEntity
    {
        [PrimaryKey]
        [TableField(IsShow = false)]
        public string PermissionId { get; set; }

        [TitleField]
        [DisplayName("功能名称")]
        [SearchField]
        [Validator("required")]
        public string PermissionName { get; set; }

        [DisplayName("功能编码")]
        [Validator("required")]
        [TableField(ShowLength = 10)]
        public string PermissionCode { get; set; }

        [DisplayName("URL地址")]
        [TableField(ShowLength = 10)]
        public string Url { get; set; }

        [SearchField(IsSearchControl=false)]
        [FunctionDataSource(typeof(CustomPermissionDataSource))]
        [DisplayName("所属父级")]
        [TableField(IsShow=false)]
        [FormField(ControlType = ControlType.DropDownList)]
        public string ParentId { get; set; }

        [DisplayName("排序值")]
        [TableField(IsShow = false)]
        public int Rank { get; set; }

        [DisplayName("控件ID")]
        public string ElementId { get; set; }

        [DisplayName("描述")]
        [TableField(IsShow = false)]
        public string Description { get; set; }

        [DisplayName("类型")]
        [DictionaryDataSource("PermissionType")]
        [TableField(IsShow=false)]
        [FormField(ControlType = ControlType.DropDownList)]
        [Validator("required")]
        public string PermissionType { get; set; }

        [DisplayName("图标")]
        [TableField(IsShow = false)]
        public string Icon { get; set; }

        [DictionaryDataSource("Status")]
        [FormField(ControlType= ControlType.DropDownList,DefaultValue="Enabled")]
        [DisplayName("状态")]
        public string Status { get; set; }
    }

    public class PermissionEntityService : CustomEntityService
    {
        public override IList<ButtonEntity> AddButtons()
        {
            var buttons = base.AddButtons();
            buttons.Add(new ButtonEntity()
            {
                ButtonControlId = "btnPermissionRule",
                CssClass = "btn btn-warning btn-sm",
                ButtonName = "规则",
                ButtonType = ButtonEntityType.TableRow,
                OnClick = "$.showModalDialog({{ url: '/Entity/List/PermissionRule?PermissionId={0}', overlayClose: true,title:'功能【{1}】的所有规则', width: 700, height: 500 }});",
                DataFields = "PermissionId,PermissionName",
                onRowDataBind = (dict) => {
                    if(dict["PermissionType"].ToString()=="3")
                    {
                        return true;
                    }
                    return false; 
                }
            });
            return buttons;
        }
        public override int Delete(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            string permissionId = string.Empty;
            if (entity is PermissionEntity)
            {
                permissionId = (entity as PermissionEntity).PermissionId;
            }
            else
            {
                permissionId = entity.ToString();
            }
            using (TransactionScope tran = new TransactionScope())
            {
                //先删除 权限 关联的 角色类型权限
                new RoleTypePermissionEntityService().DeleteByPermissionId(permissionId);

                //再删除 权限 关联的 角色权限
                new RolePermissionEntityService().DeleteByPermissionId(permissionId);

                //再删除 权限 的 规则
                new PermissionRuleEntityService().DeleteByPermissionId(permissionId);

                //再删除 权限
                int effectCount = base.Delete(entity);

                tran.Complete();

                return effectCount;
            }
        }
    }

    public class CustomPermissionDataSource : IKeyValueDataSource
    {

        public IList<SelectListItem> SelectItems()
        {
            //return Dao.Get().QueryEntities<SelectListItem>("SELECT PermissionId Value,PermissionName Text FROM [SYS_Permission] WHERE [PermissionType]='1'");
            return Dao.Get().QueryEntities<SelectListItem>("SummerFresh.Business.Entity.CustomPermission");
        }

        public object Converter(string columnName, object columnValue, IDictionary<string, object> rowData)
        {
            var items = SelectItems();
            if(!items.IsNullOrEmpty())
            {
                var item = items.FirstOrDefault(o => o.Value.Equals(columnValue.ToString(), StringComparison.OrdinalIgnoreCase));
                if(item!=null)
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
}
