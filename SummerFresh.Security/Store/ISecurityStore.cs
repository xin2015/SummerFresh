using System.Collections.Generic;
using SummerFresh.Security.Permission;
using SummerFresh.Security.Principal;
using SummerFresh.Security.Rule;

namespace SummerFresh.Security.Store
{
    /// <summary>
    /// 以非缓存的形式从存储介质中获取各种所需的信息
    /// </summary>
    public interface ISecurityStore
    {
        /// <summary>
        /// 根据用户的登录名称返回用户登录验证信息，如密码
        /// </summary>
        /// <param name="loginId">用户登录名称</param>
        /// <returns></returns>
        IUser GetUserLoginInfo(string loginId);

        /// <summary>
        /// 根据用户的登录名称返回包含用户基础信息的用户对象
        /// </summary>
        /// <param name="loginId">用户登录名称</param>
        /// <returns>用户登录名称不存在返回null，否则返回对应的对象实例</returns>
        IUser GetUserByLoginId(string loginId);

        /// <summary>
        /// 返回用户所拥有的所有角色集合
        /// </summary>
        /// <param name="userId">用户的唯一标识</param>
        /// <returns>角色集合</returns>
        IEnumerable<IRole> GetAllUserRoles(string userId);

        /// <summary>
        /// 返回用户所拥有的所有操作权限
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<GenericPermission> GetAllUserPermissions(IUser user);

        /// <summary>
        /// 返回系统注册的所有Url访问权限
        /// </summary>
        /// <returns></returns>
        IEnumerable<UrlPermission> GetAllUrlPermissions();

        /// <summary>
        /// 返回所有定义了页面元素标识的UI权限
        /// </summary>
        /// <returns></returns>
        IEnumerable<UIPermission> GetAllUIPermissions();

        /*
        /// <summary>
        /// 返回用户所拥有的所有URL权限
        /// </summary>
        /// <returns></returns>
        IEnumerable<UrlPermission> GetUserUrlPermissions(string userId);

        /// <summary>
        /// 返回用户是否拥有指定操作的权限
        /// </summary>
        /// <returns>拥有权限返回true，否则返回false</returns>
        bool GetUserHasPermission(string userId, string operation);

        /// <summary>
        /// 返回用户在该操作权限下所分配的安全规则集合
        /// </summary>
        /// <param name="userId">用户唯一标识</param>
        /// <param name="operation">操作权限标识</param>
        /// <returns>安全规则集合</returns>
        IEnumerable<SecurityRule> GetUserPermissionRules(string userId, string operation);
        */
    }
}