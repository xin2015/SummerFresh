using System.Collections.Generic;
using SummerFresh.Security.Permission;
using SummerFresh.Security.Principal;

namespace SummerFresh.Security.Authorization
{
    public interface ISecurityAuthorizer
    {
        /// <summary>
        /// 返回用户是否拥有该Url的访问权限
        /// </summary>
        /// <param name="user"></param>
        /// <param name="virtualPath"></param>
        /// <param name="queryString"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        bool IsUserHasPermissionOfUrl(IUser user,string virtualPath,string queryString,out UrlPermission permission);

        /// <summary>
        /// 返回指定用户是否拥有该操作的权限
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <param name="operation">操作的唯一标识</param>
        /// <returns>拥有权限返回true，否则返回false</returns>
        bool IsUserHasPermission(IUser user, string operation);

        /// <summary>
        /// 返回用户对指定操作的权限规则
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <param name="operation">操作的唯一标识</param>
        /// <returns>拥有权限返回规则内容，否则返回空字符串</returns>
        string GetUserPermissionRule(IUser user, string operation);

        /// <summary>
        /// 返回用户对指定UI操作权限的行为
        /// </summary>
        /// <param name="user"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        string GetUserPermissionBehaviour(IUser user, string operation);

        /// <summary>
        /// 返回某个页面Url下定义的所有UI元素权限
        /// </summary>
        /// <param name="user"></param>
        /// <param name="virtualPath"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        IEnumerable<UIPermission> GetUIPermissionsOfUrl(IUser user, string virtualPath, string queryString);

        /// <summary>
        /// 返回系统定义的所有Url权限,返回的Url带了访问的虚拟路径
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<UrlPermission> GetAllUrlPermissions(IUser user);
    }
}