using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Data.Attributes;
using SummerFresh.Business.Service;
using System.Transactions;
using SummerFresh.Data;

namespace SummerFresh.Business.Entity
{
    [EntityService(typeof(PermissionRuleEntityService))]
    [Table("SYS_PermissionRule")]
    public class PermissionRuleEntity : CustomEntity
    {
        [TableField(IsShow=false)]
        [PrimaryKey]
        public string PermissionRuleId { get; set; }

        [DisplayName("权限")]
        [TableField(IsShow=false)]
        [SearchField]
        [FormField(ControlType = ControlType.DropDownList)]
        [CustomEntityDataSource(typeof(PermissionEntity))]
        public string PermissionId { get; set; }

        [DisplayName("规则名")]
        [TitleField]
        public string RuleName { get; set; }

        [DisplayName("规则优先级")]
        public int Priority { get; set; }

        [DisplayName("规则内容")]
        public string RuleContent { get; set; }

        [FormField(ControlType = ControlType.TextArea)]
        [DisplayName("描述")]
        public string Description { get; set; }
    }

    public class PermissionRuleEntityService : CustomEntityService
    {
        public override int Delete(object entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            string permissionRuleId = string.Empty;
            if (entity is PermissionRuleEntity)
            {
                permissionRuleId = (entity as PermissionRuleEntity).PermissionRuleId;
            }
            else 
            {
                permissionRuleId = entity.ToString();
            }
            using (TransactionScope tran = new TransactionScope())
            {
                //先删除 角色类型 的 权限规则
                new RoleTypePermissionEntityService().DeleteByPermissionRuleId(permissionRuleId);

                //再删除 角色 的 权限规则
                new RolePermissionEntityService().DeleteByPermissionRuleId(permissionRuleId);

                //再删除 权限规则
                int effectCount = base.Delete(entity);

                tran.Complete();

                return effectCount;
            }
        }

        public int DeleteByPermissionId(string permissionId)
        {
            return Dao.Get().ExecuteNonQuery("SummerFresh.Business.Entity.PermissionRule.DeleteByPermissionId", new { PermissionId = permissionId });
        }
    }
}
