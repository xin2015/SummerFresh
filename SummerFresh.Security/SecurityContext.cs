using SummerFresh.Security.Cache;
using SummerFresh.Security.Principal;

namespace SummerFresh.Security
{
    /// <summary>
    /// 此类提供当前线程的所有安全上下文信息
    /// </summary>
    public sealed class SecurityContext
    {
        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        public static IUser User
        {
            get { return Provider.GetCurrentUser(); }
        }

        /// <summary>
        /// 获取当前登录用户的状态数据存储
        /// </summary>
        public static IUserState Data
        {
            get { return User.Data; }
        }

        /// <summary>
        /// 返回当前应用ISecurityProvider实现类的实例
        /// </summary>
        public static ISecurityProvider Provider
        {
            get { return SecurityFactory.Provider;}
        }

        /// <summary>
        /// 取消当前安全上下文中用户的会话数据，一般在注销用户时调用此方法
        /// </summary>
        public static void Abandon()
        {
            Provider.Abandon();
        }

        /// <summary>
        /// 返回当前用户是否拥有指定操作的权限
        /// </summary>
        /// <param name="operation">操作权限标识</param>
        /// <returns>拥有权限返回true，否则返回false</returns>
        public static bool HasPermission(string operation)
        {
            return Provider.HasPermission(operation);
        }
    }
}