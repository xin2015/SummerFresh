using SummerFresh.Security.Principal;

namespace SummerFresh.Security.Authentication
{
    public interface ISecurityAuthenticator
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
    }
}