using SummerFresh.Business.Entity;
using SummerFresh.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business.Workflow
{
    public class Actor
    {

        /// <summary>
        /// 参与者所属的流程步骤
        /// </summary>
        public Activity Owner { get; set; }

        public string LoginId { get; set; }

        public string UserId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName
        {
            get;
            set;
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName
        {
            get;
            set;
        }

        /// <summary>
        /// 自动向上求解
        /// </summary>
        public bool AutoResolvParent
        {
            get;
            set;
        }

        /// <summary>
        /// 求解参与者
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual IList<UserEntity> Resolve(WorkflowContext context)
        {
            DepartmentEntity baseDept = null;

            if(!string.IsNullOrEmpty(LoginId) || !string.IsNullOrEmpty(UserId))
            {
                return BSDContext<UserEntity>.Instance.Where(o => o.LoginId.Equals(LoginId, StringComparison.OrdinalIgnoreCase) || o.UserId.Equals(UserId, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else if (!string.IsNullOrEmpty(DeptName))
            {
                baseDept = BSDContext<DepartmentEntity>.Instance.FirstOrDefault(o => o.DepartmentName.Equals(DeptName, StringComparison.OrdinalIgnoreCase));
                return GetMembers(baseDept);
            }
            else
            {
                return BSDContext<UserEntity>.Instance.Where(o => o.UserId.Equals(context.FlowInstance.CreatorUserId, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }

        protected virtual IList<UserEntity> GetMembers(DepartmentEntity department)
        {
            IList<UserEntity> result = new List<UserEntity>();
            if (string.IsNullOrEmpty(RoleName) || RoleName.Equals("部门成员"))
            {
                result = BSDContext<UserEntity>.Instance.Where(o => o.DepartmentId.Equals(department.DepartmentId, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else
            {
                try
                {
                    result = Dao.Get().QueryEntities<UserEntity>(
                                @"SELECT u.* FROM SYS_UserRole ur
                                LEFT JOIN SYS_Role r ON ur.RoleId=r.RoleId
                                LEFT JOIN SYS_RoleType rt ON r.RoleTypeId=rt.RoleTypeId
                                LEFT JOIN SYS_User u on ur.UserId=u.UserId
                                WHERE r.DepartmentId=#DeptId# AND rt.RoleTypeName=#RoleName#"
                                , new { DeptId = department.DepartmentId, RoleName = RoleName });
                    if (result.Count == 0 && AutoResolvParent)
                    {
                        var parent = BSDContext<DepartmentEntity>.Instance.FirstOrDefault(o => o.DepartmentId.Equals(department.ParentId, StringComparison.OrdinalIgnoreCase));
                        return GetMembers(parent);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("获取角色{0}的成员时出错:{1}。", this.RoleName, ex.Message), ex);
                }
            }
            return result;
        }
    }
}
