using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using SummerFresh.Security.Principal;

namespace SummerFresh.Security
{
    /// <summary>
    /// 提供系统中用户身份验证，权限验证，获取登录用户信息，加载用户信息到HttpContext中 相关操作
    /// </summary>
    public interface ISecurityProvider
    {
        /// <summary>
        /// 验证给定用户的登录名称与密码是否有效
        /// </summary>
        /// <param name="username">用户登录名称</param>
        /// <param name="password">用户登录密码</param>
        /// <returns>认证通过返回true，否则返回false</returns>
        bool Authenticate(string username, string password);

        /// <summary>
        /// 对密码文本进行加密后得到加密的密码串
        /// </summary>
        /// <param name="password">密码文本</param>
        /// <returns>加密后的密码文本</returns>
        /// <remarks>
        /// 如果实现类不支持密码加密，应该返回传入的密码文本
        /// </remarks>
        string EncryptPassword(string password);

        /// <summary>
        /// 返回是否对请求的URL路径拥有访问权限
        /// </summary>
        /// <param name="virtualPath">当前应用中的虚拟请求路径</param>
        /// <param name="queryString">请求URL中的参数字符串(带"?")</param>
        /// <returns>拥有该URL路径的访问权限返回true,否则返回false</returns>
        bool HasPermissionOfUrl(string virtualPath, string queryString);

        /// <summary>
        /// 返回是否对请求的URL路径拥有访问权限
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="virtualPath">当前应用中的虚拟请求路径</param>
        /// <param name="queryString">请求URL中的参数字符串(带"?")</param>
        /// <returns>拥有该URL路径的访问权限返回true,否则返回false</returns>
        bool HasPermissionOfUrl(IUser user, string virtualPath, string queryString);

        /// <summary>
        /// 返回是否对请求的URL路径拥有访问权限
        /// </summary>
        /// <param name="virtualPath">当前应用中的虚拟请求路径</param>
        /// <param name="queryString">请求URL中的参数字符串(带"?")</param>
        /// <param name="operation">输出该Url对应的唯一操作标识，如果该Url没有注册输出null</param>
        /// <returns>拥有该URL路径的访问权限返回true,否则返回false</returns>
        bool HasPermissionOfUrl(string virtualPath,string queryString,out string operation);

        /// <summary>
        /// 返回是否对请求的URL路径拥有访问权限
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="virtualPath">当前应用中的虚拟请求路径</param>
        /// <param name="queryString">请求URL中的参数字符串(带"?")</param>
        /// <param name="operation">输出该Url对应的唯一操作标识，如果该Url没有注册输出null</param>
        /// <returns>拥有该URL路径的访问权限返回true,否则返回false</returns>
        bool HasPermissionOfUrl(IUser user, string virtualPath, string queryString, out string operation);

        /// <summary>
        /// 返回当前用户是否拥有该操作的权限
        /// </summary>
        /// <param name="operation">操作权限的唯一标识</param>
        /// <returns>拥有权限返回true，否则返回false</returns>
        bool HasPermission(string operation);

        /// <summary>
        /// 返回当前用户是否拥有该操作的权限
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="operation">操作权限的唯一标识</param>
        /// <returns>拥有权限返回true，否则返回false</returns>
        bool HasPermission(IUser user, string operation);

        /// <summary>
        /// 返回当前用户是否对指定的多个操作拥有权限
        /// </summary>
        /// <param name="operations">操作权限标识的集合</param>
        /// <returns>返回IDictionary&lt string operation,bool hasPermission&gt;集合</returns>
        IDictionary<string, bool> HasPermissions(params string[] operations);

        /// <summary>
        /// 返回当前用户是否对指定的多个操作拥有权限
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="operations">操作权限标识的集合</param>
        /// <returns>返回IDictionary&lt string operation,bool hasPermission&gt;集合</returns>
        IDictionary<string, bool> HasPermissions(IUser user, params string[] operations);

        /// <summary>
        /// 返回当前用户对指定操作权限的权限规则
        /// </summary>
        /// <param name="operation">操作权限的唯一标识</param>
        /// <returns>如果用户没有该操作权限或者没有定义规则，返回空字符串，否则返回规则内容</returns>
        string GetPermissionRule(string operation);

        /// <summary>
        /// 返回当前用户对指定操作权限的权限规则
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="operation">操作权限的唯一标识</param>
        /// <returns>如果用户没有该操作权限或者没有定义规则，返回空字符串，否则返回规则内容</returns>
        string GetPermissionRule(IUser user, string operation);

        /// <summary>
        /// 返回当前用户对指定操作权限的行为
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        string GetPermissionBehaviour(string operation);

        /// <summary>
        /// 返回当前用户对指定操作权限的行为
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="operation"></param>
        /// <returns></returns>
        string GetPermissionBehaviour(IUser user, string operation);

        /// <summary>
        /// 对当前上下文进行认证处理，如加载用户信息到上下文中，安全上下文在此方法中进行初始化
        /// </summary>
        /// <remarks>
        /// 此方法默认已经在SecurityModule中进行自动调用，一般情况下不需要直接调用，除非您自定义了SecurityModule的逻辑。
        /// </remarks>
        /// <returns>如果用户没有经过认证返回false，否则返回true</returns>
        bool Authenticate(HttpContextBase context);

        /// <summary>
        /// 对当前请求上下文进行授权处理
        /// </summary>
        /// <remarks>
        /// 此方法默认已经在SecurityModule中进行自动调用，一般情况下不需要直接调用，除非您自定义了SecurityModule的逻辑。
        /// </remarks>
        void Authorize(HttpContextBase context);

        /// <summary>
        /// 返回当前实现是否需要使用会话状态来存储用户状态数据
        /// </summary>
        bool RequireSessionState { get; }

        /// <summary>
        /// 取消当前安全上下文中的用户会话.
        /// </summary>
        /// <remarks>
        /// 一旦取消了当前的用户会话，所有用户缓存数据都被清空
        /// </remarks>
        void Abandon();

        /// <summary>
        /// 返回当前登录用户
        /// </summary>
        IUser GetCurrentUser();

        /// <summary>
        /// 返回指定用户
        /// 可以通过IUser接口，判断用户是否有权限等操作
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        IUser GetUser(string loginId);

        /// <summary>
        /// 是否为当前登录用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool IsCurrentUser(IUser user);

        /// <summary>
        /// 获取当前目录所有UI权限，及当前用户是否拥有这些UI权限
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        IDictionary<string, UISecurityBehaviour> GetUISecurityBehaviours(string virtualPath, string queryString);
    }
}