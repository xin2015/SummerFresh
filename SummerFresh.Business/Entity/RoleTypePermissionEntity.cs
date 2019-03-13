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
    [Table("SYS_RoleTypePermission")]
    public class RoleTypePermissionEntity:CustomEntity
    {
        [TableField(IsShow=false)]
        [PrimaryKey]
        public string RoleTypePermissionId { get; set; }

        [DisplayName("角色类型")]
        [FormField(ControlType = ControlType.DropDownList)]
        [CustomEntityDataSource(typeof( RoleTypeEntity))]
        public string RoleTypeId { get; set; }

        [DisplayName("权限")]
        [FormField(ControlType = ControlType.DropDownList)]
        [CustomEntityDataSource(typeof(PermissionEntity))]
        public string PermissionId { get; set; }

        [DisplayName("权限规则")]
        [FormField(ControlType = ControlType.DropDownList)]
        [CustomEntityDataSource(typeof(PermissionRuleEntity))]
        public string PermissionRuleId { get; set; }
    }

    public class RoleTypePermissionEntityService:CustomEntityService
    {

        /// <summary>
        /// 根据roleTypeId删除相关的所有角色类型权限
        /// </summary>
        /// <param name="roleTypeId"></param>
        /// <returns></returns>
        public int DeleteByRoleTypeId(string roleTypeId)
        {
            return Dao.Get().ExecuteNonQuery("SummerFresh.Business.Entity.RoleTypePermissionEntity.DeleteByOther", new { RoleTypeId = roleTypeId });
        }

        /// <summary>
        /// 根据permissionId删除相关的所有角色类型权限
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        public int DeleteByPermissionId(string permissionId)
        {
            return Dao.Get().ExecuteNonQuery("SummerFresh.Business.Entity.RoleTypePermissionEntity.DeleteByOther", new { PermissionId = permissionId });
        }

        /// <summary>
        /// 根据permissionRuleId删除相关的所有角色类型权限规则
        /// </summary>
        /// <param name="permissionRuleId"></param>
        /// <returns></returns>
        public int DeleteByPermissionRuleId(string permissionRuleId)
        {
            return Dao.Get().ExecuteNonQuery("SummerFresh.Business.Entity.RoleTypePermissionEntity.DeleteByOther", new { PermissionRuleId = permissionRuleId });
        }
    }
}
