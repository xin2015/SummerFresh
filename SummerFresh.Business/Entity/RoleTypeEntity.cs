using SummerFresh.Business.Service;
using SummerFresh.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Transactions;

namespace SummerFresh.Business.Entity
{
    [EntityService(typeof(RoleTypeEntityService))]
    [Table("SYS_RoleType"), DisplayName("系统角色类型")]
    public class RoleTypeEntity : CustomEntity
    {
        [PrimaryKey]
        [DisplayName("角色类型Id")]
        [TableField(IsShow = false)]
        public string RoleTypeId { get; set; }

        [TitleField]
        [DisplayName("角色类型名称")]
        [SearchField]
        [Validator("required,zhNumChar")]
        public string RoleTypeName { get; set; }
    }

    public class RoleTypeEntityService : CustomEntityService
    {
        public override int Insert(CustomEntity entity)
        {
            if (Data.Dao.Get().QueryScalar<int>("SummerFresh.Business.Entity.RoleTypeEntity.InserValidate", entity) > 0)
            {
                throw new CustomException("角色类型名称重复");
            }
            return base.Insert(entity);
        }

        public override int Update(CustomEntity entity)
        {
            if (Data.Dao.Get().QueryScalar<int>("SummerFresh.Business.Entity.RoleTypeEntity.InserValidate", entity) > 0)
            {
                throw new CustomException("角色类型名称重复");
            }
            return base.Update(entity);
        }

        public override int Delete(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            string roleTypeId = string.Empty;

            if (entity is RoleTypeEntity)
            {
                roleTypeId = (entity as RoleTypeEntity).RoleTypeId;
            }
            else
            {
                roleTypeId = entity.ToString();
            }
            using (TransactionScope tran = new TransactionScope())
            {

                //先删除 角色类型 的 所有角色的 角色用户
                new UserRoleEntityService().DeleteByRoleTypeId(roleTypeId);

                //再删除 角色类型 的 所有角色的 角色权限
                new RolePermissionEntityService().DeleteByRoleTypeId(roleTypeId);

                //再删除 角色类型 的 所有角色
                new RoleEntityService().DeleteByRoleTypeId(roleTypeId);

                //再删除 角色类型 的 所有权限
                new RoleTypePermissionEntityService().DeleteByRoleTypeId(roleTypeId);

                //再删除 角色类型
                var effectCount = base.Delete(entity);

                tran.Complete();

                return effectCount;
            }
        }
        public override IList<ButtonEntity> AddButtons()
        {
            var buttons = base.AddButtons();
            buttons.Add(new ButtonEntity()
            {
                ButtonControlId = "btnRoleTypeUser",
                CssClass = "btn btn-warning btn-sm",
                ButtonType = ButtonEntityType.TableRow,
                OnClick = "$.showModalDialog({{ url: 'Entity/List/UserRole?RoleTypeId={0}', width: 800, height: 500,title:'角色类型【{1}】的用户信息', overlayClose: true }});",
                DataFields = "RoleTypeId,RoleTypeName",
                ButtonName = "角色类型用户"
            });
            buttons.Add(new ButtonEntity()
            {
                ButtonControlId = "btnRoleTypePermission",
                CssClass = "btn btn-warning btn-sm",
                ButtonType = ButtonEntityType.TableRow,
                OnClick = "$.showModalDialog({{ url: 'Permission/List?type=roleType&id={0}', width: 800, height: 500,title:'给角色类型【{1}】分配权限', overlayClose: true }});",
                DataFields="RoleTypeId,RoleTypeName",
                ButtonName = "分配权限"
            });
            return buttons;
        }
    }
}
