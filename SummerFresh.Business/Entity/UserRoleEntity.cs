using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Data.Attributes;
using SummerFresh.Business.Service;
using SummerFresh.Data;
using SummerFresh.Basic;
namespace SummerFresh.Business.Entity
{
    [EntityService(typeof(UserRoleEntityService))]
    [Table("SYS_UserRole",ShowCheckbox=false)]
    public class UserRoleEntity:CustomEntity
    {
        [TableField(IsShow=false)]
        [PrimaryKey]
        public string UserRoleId { get; set; }

        [DisplayName("用户")]
        [FormField(ControlType = ControlType.DropDownList)]
        [CustomEntityDataSource(typeof(UserEntity))]
        [Validator("required")]
        public string UserId { get; set; }

        [DisplayName("角色类型")]
        [FormField(Editable=false)]
        [CustomEntityDataSource(typeof(RoleEntity))]
        public string RoleId { get; set; }

        [DisplayName("角色类型所属组织")]
        [Column(Insert=false,Update=false)]
        [FormField(ControlType = ControlType.DropDownList)]
        [Validator("required")]
        [CustomEntityDataSource(typeof(DepartmentEntity))]
        public string DepartmentId { get; set; }

        [DisplayName("角色类型")]
        [FormField(ControlType = ControlType.DropDownList)]
        [CustomEntityDataSource(typeof(RoleTypeEntity))]
        [Validator("required")]
        [TableField(IsShow=false)]
        public string RoleTypeId { get; set; }

    }

    public class UserRoleEntityService:CustomEntityService
    {

        public override IList<ButtonEntity> AddButtons()
        {
            var buttons = base.AddButtons();
            buttons.Remove(new ButtonEntity() { ButtonControlId = "btnBatchDelete" });
            buttons.Remove(new ButtonEntity() { ButtonControlId = "btnEdit" });
            return buttons;
        }

        public override int Insert(CustomEntity entity)
        {
            var userRole = entity as UserRoleEntity;
            var roleId = Dao.Get().QueryScalar<string>("SummerFresh.Business.Entity.UserRoleEntity.InsertValidate1", userRole);
            if(roleId.IsNullOrEmpty())
            {
                var roleService = new RoleEntityService();
                roleId = Guid.NewGuid().ToString();
                roleService.Insert(new RoleEntity()
                {
                    RoleId = roleId,
                    DepartmentId = userRole.DepartmentId,
                    RoleTypeId = userRole.RoleTypeId
                });
            }
            userRole.RoleId = roleId;
            if (Dao.Get().QueryScalar<int>("SummerFresh.Business.Entity.UserRoleEntity.InsertValidate2", userRole) > 0)
            {
                throw new CustomException("用户已经拥有该角色!");
            }
            return base.Insert(entity);
        }

        /// <summary>
        /// 根据userId删除相关的所有用户角色
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public int DeleteByUserId(string userId)
        {
            return Dao.Get().ExecuteNonQuery("SummerFresh.Business.Entity.UserRoleEntity.DeleteByOther", new { UserId = userId });
        }

        /// <summary>
        /// 根据departmentId删除相关的所有用户角色
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public int DeleteByDepartmentId(string departmentId)
        {
            return Dao.Get().ExecuteNonQuery("SummerFresh.Business.Entity.UserRoleEntity.DeleteByOther", new { DepartmentId = departmentId });
        }

        /// <summary>
        /// 根据roleTypeId删除相关的所有用户角色
        /// </summary>
        /// <param name="roleTypeId"></param>
        /// <returns></returns>
        public int DeleteByRoleTypeId(string roleTypeId)
        {
            return Dao.Get().ExecuteNonQuery("SummerFresh.Business.Entity.UserRoleEntity.DeleteByOther", new { RoleTypeId = roleTypeId });
        }

        /// <summary>
        /// 根据roleId删除相关的所有用户角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int DeleteByRoleId(string roleId)
        {
            return Dao.Get().ExecuteNonQuery("SummerFresh.Business.Entity.UserRoleEntity.DeleteByOther", new { RoleId = roleId });
        }
    }
}
