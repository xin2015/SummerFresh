using System;
using System.Collections.Generic;
using System.Security.Principal;
using SummerFresh.Security.Cache;

namespace SummerFresh.Security.Principal
{
    /// <summary>
    /// 表示一个系统用户
    /// </summary>
    public interface IUser : IPrincipal
    {
        #region 基础信息

        /// <summary>
        /// 唯一标识
        /// </summary>
        string UserId { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// 用户编码
        /// </summary>
        string UserCode { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        string LoginId { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Email地址
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        string MobilePhone { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        string Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        int Age { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        string DepartmentId { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        DateTime Birthday { get; set; }
        #endregion

        #region 详细信息
        /// <summary>
        /// 扩展信息
        /// </summary>
        IDictionary<string, object> Properties { get; }
        #endregion

        #region 缓存信息
        IUserState Data { get; }
        #endregion

        #region 认证与授权信息

        /// <summary>
        /// 返回用户是否拥有指定的权限
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        bool HasPermission(string operation);

        /// <summary>
        /// 返回用户所属角色的集合
        /// </summary>
        IEnumerable<IRole> Roles { get; set; }

        /// <summary>
        /// 返回此用户是否已经登录
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// 返回HttpContext中的IPrincipal对象
        /// </summary>
        IPrincipal Principal { get; set; }

        #endregion
    }
}