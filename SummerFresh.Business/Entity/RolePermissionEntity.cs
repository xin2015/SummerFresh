using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Data.Attributes;
using SummerFresh.Business.Service;
using SummerFresh.Data;

namespace SummerFresh.Business.Entity
{
    [Table("SYS_RolePermission")]
    public class RolePermissionEntity:CustomEntity
    {
        [TableField(IsShow = false)]
        [PrimaryKey]
        public string RolePermissionId { get; set; }

        [CustomEntityDataSource(typeof(RoleEntity))]
        [FormField(ControlType = ControlType.DropDownList)]
        [DisplayName("角色")]
        public string RoleId { get; set; }

        [CustomEntityDataSource(typeof(PermissionEntity))]
        [FormField(ControlType = ControlType.DropDownList)]
        [DisplayName("权限")]
        public string PermissionId { get; set; }

        [CustomEntityDataSource(typeof(PermissionRuleEntity))]
        [FormField(ControlType = ControlType.DropDownList)]
        [DisplayName("权限规则")]
        public string PermissionRuleId { get; set; }
    }

    public class RolePermissionEntityService:CustomEntityService
    {
        /// <summary>
        /// 根据permissionRuleId删除相关的所有角色权限规则
        /// </summary>
        /// <param name="permissionRuleId"></param>
        /// <returns></returns>
        public int DeleteByPermissionRuleId(string permissionRuleId)
        {
            return Dao.Get().ExecuteNonQuery("SummerFresh.Business.Entity.RolePermissionEntity.DeleteByOther", new { PermissionRuleId = permissionRuleId });
        }

        /// <summary>
        /// 根据permissionId删除相关的所有角色权限
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        public int DeleteByPermissionId(string permissionId)
        {
            return Dao.Get().ExecuteNonQuery("SummerFresh.Business.Entity.RolePermissionEntity.DeleteByOther", new { PermissionId = permissionId });
        }

        /// <summary>
        /// 根据departmentId删除相关的所有角色权限
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public int DeleteByDepartmentId(string departmentId)
        {
            return Dao.Get().ExecuteNonQuery("SummerFresh.Business.Entity.RolePermissionEntity.DeleteByOther", new { DepartmentId = departmentId });
        }

        /// <summary>
        /// 根据roleTypeId删除相关的所有角色权限
        /// </summary>
        /// <param name="roleTypeId"></param>
        /// <returns></returns>
        public int DeleteByRoleTypeId(string roleTypeId)
        {
            return Dao.Get().ExecuteNonQuery("SummerFresh.Business.Entity.RolePermissionEntity.DeleteByOther", new { RoleTypeId = roleTypeId });
        }

        /// <summary>
        /// 根据roleId删除相关的所有角色权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int DeleteByRoleId(string roleId)
        {
            return Dao.Get().ExecuteNonQuery("SummerFresh.Business.Entity.RolePermissionEntity.DeleteByOther", new { RoleId = roleId });
        }
    }
}
