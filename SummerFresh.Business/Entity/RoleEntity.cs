
using SummerFresh.Business.Service;
using SummerFresh.Data;
using SummerFresh.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SummerFresh.Business.Entity
{
    [EntityService(typeof(RoleEntityService))]
    [Table("SYS_Role"),DisplayName("系统角色")]
    public class RoleEntity:CustomEntity
    {
        [TableField(IsShow=false)]   
        [PrimaryKey]
        public string RoleId { get; set; }

        [DisplayName("角色类型")]
        [SearchField]
        [TitleField]
        [TableField(IsShow=false)]
        [FormField(ControlType=ControlType.DropDownList)]
        [CustomEntityDataSource(typeof(RoleTypeEntity))]
        public string RoleTypeId { get; set; }

        [DisplayName("角色类型名称")]
        [FormField(Editable=false)]
        [Column(Insert=false,Update=false)]
        public string RoleTypeName { get; set; }


        [DisplayName("所属组织")]
        [FormField(ControlType = ControlType.DropDownList)]
        [CustomEntityDataSource(typeof(DepartmentEntity))]
        [SearchField]
        public string DepartmentId { get; set; }
    }

    public class RoleEntityService:CustomEntityService
    {
        public override IList<ButtonEntity> AddButtons()
        {
            var buttons =  base.AddButtons();
            buttons.Add(new ButtonEntity()
            {
                ButtonControlId = "btnRoleUser",
                ButtonName = "角色用户",
                CssClass = "btn btn-warning btn-sm",
                OnClick = "$.showModalDialog({{url:'Entity/List/UserRole?RoleId={0}',overlayClose:true,width:800,title:'角色【{1}】的用户',height:500}});",
                DataFields = "RoleId,RoleTypeName",
                ButtonType = ButtonEntityType.TableRow
            });
            buttons.Add(new ButtonEntity() 
            {
                ButtonControlId="btnRolePermission",
                ButtonName="分配权限",
                CssClass = "btn btn-warning btn-sm",
                OnClick="$.showModalDialog({{url:'Permission/List?type=role&id={0}&RoleTypeId={1}',overlayClose:true,width:800,title:'为角色【{2}】分配权限',height:500}});",
                DataFields="RoleId,RoleTypeId,RoleTypeName",
                ButtonType= ButtonEntityType.TableRow
            });
            return buttons;
        }
        public override int Delete(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            string roleId = string.Empty;
            if (entity is RoleEntity)
            {
                roleId = (entity as RoleEntity).RoleId;
            }
            else
            {
                roleId = entity.ToString();
            }
            //先删除 该角色的所有用户
            new UserRoleEntityService().DeleteByRoleId(roleId);

            //再删除 该角色的所有权限
            new RolePermissionEntityService().DeleteByRoleId(roleId);

            //最后删除 该角色
            return base.Delete(entity);
        }
        public int DeleteByDepartmentId(string departmentId)
        {
            return Dao.Get().ExecuteNonQuery("SummerFresh.Business.Entity.RoleEntity.DeleteByOther", new { DepartmentId = departmentId });
        }

        public int DeleteByRoleTypeId(string roleTypeId)
        {
            return Dao.Get().ExecuteNonQuery("SummerFresh.Business.Entity.RoleEntity.DeleteByOther", new { RoleTypeId = roleTypeId });
        }

        public override int Insert(CustomEntity entity)
        {
            if (Dao.Get().QueryScalar<int>("SummerFresh.Business.Entity.RoleEntity.InserValidate", entity) > 0)
            {
                throw new CustomException("该角色已存在!");
            }
           return base.Insert(entity);
        }

        public override int Update(CustomEntity entity)
        {
            if (Dao.Get().QueryScalar<int>("SummerFresh.Business.Entity.RoleEntity.InserValidate", entity) > 0)
            {
                throw new CustomException("该角色已存在!");
            }
            return base.Update(entity);
        }
    }
}