using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using SummerFresh.Security.Permission;
using SummerFresh.Security.Principal;
using SummerFresh.Security.Rule;
using SummerFresh.Security.Store;
using System.Linq;

namespace SummerFresh.Security.Authorization
{
    /// <summary>
    /// 结合缓存与非缓存两种方式获取各种权限信息
    /// </summary>
    internal class SecurityAuthorizer : ISecurityAuthorizer
    {
        private static readonly string UserAllPermissionsKey = "AuthorizedAll";
        private static readonly string UserUrlPermissionsKey = "AuthorizedUrls";
        private static readonly string AllUrlPermissionsKey = "AllUrlPermissions";
        private static readonly string AllUIPermissionsKey = "AllUIPermissions";
        private static readonly string PageUIPermissionsKey = "UI:{0}";

        protected ISecurityStore _store;

        public virtual ISecurityStore Store
        {
            get { return _store ?? SecurityFactory.Store; }
            set { _store = value; }
        }

        public virtual IEnumerable<UIPermission> GetUIPermissionsOfUrl(IUser user, string virtualPath, string queryString)
        {
            string key = string.Format(PageUIPermissionsKey, virtualPath);

            return user.Data.GetValue(key, () =>
            {
                // 必须得转换为List不然序列化不了
                return GetAllUIPermissions(user).Where(p => null != p.Url && p.Url.StartsWith(virtualPath, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
            }) as IEnumerable<UIPermission>;
        }

        public virtual bool IsUserHasPermissionOfUrl(IUser user, string virtualPath, string queryString, out UrlPermission permission)
        {
            permission = null;

            //1、匹配注册的URL，从最长的开始匹配
            IEnumerable<UrlPermission> urls =
                from p in GetAllUrlPermissions(user) where p.VirtualPath.Equals(virtualPath,System.StringComparison.CurrentCultureIgnoreCase) orderby p.QueryString.Length descending select p;

            if (urls.Count() > 0)
            {
                //找到精确匹配给定的Url的注册权限再判断用户是否具有改权限
                string virtualUrl = virtualPath + queryString;

                //查找匹配的Url,一个Url可能对应多个Url
                var permissions = urls.Where(url => virtualUrl.StartsWith(url.Url, System.StringComparison.CurrentCultureIgnoreCase));                
                if (permissions.Count() > 0)
                {
                    string firstUrl = permissions.First().Url;
                    foreach (var p in permissions)
                    {
                        //第一个比对之后，要判断是否和第一个的URL一样
                        if (firstUrl.Equals(p.Url, System.StringComparison.CurrentCultureIgnoreCase))
                        {
                            permission = p;
                            if (IsUserHasPermission(user, p.Operation, p.Url))
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }
                else
                {
                    //如果都找不到，则不能访问
                    //这种场景会出现这种情况：只注册了带QueryString的Url，而访问的Url和注册的Url QueryString部分不符
                    return false;
                }
            }

            //2、如果该URL没有注册，则寻找全局的控制 TODO 性能问题
            string excludePartterns = System.Configuration.ConfigurationManager.AppSettings[
                "SummerFresh.Security.DenyIfNoRegisterUrlExcludeParttern"];
            if (!string.IsNullOrEmpty(excludePartterns))
            {
                foreach (var parttern in excludePartterns.Split('|'))
                {
                    string excludeParttern = GetWildcardRegexString(parttern);
                    if (Regex.IsMatch(virtualPath, excludeParttern))
                    {
                        return true;
                    }
                }
            }

            string includePartterns = System.Configuration.ConfigurationManager.AppSettings[
                "SummerFresh.Security.DenyIfNoRegisterUrlParttern"];
            if (!string.IsNullOrEmpty(includePartterns))
            {
                foreach (var parttern in includePartterns.Split('|'))
                {
                    string includeParttern = GetWildcardRegexString(parttern);
                    if (Regex.IsMatch(virtualPath, includeParttern))
                    {
                        return false;
                    }
                }
            }

            //3、如果Url没有注册并没有在全局控制的设置范围，则默认为允许访问
            return true;
        }

        public virtual bool IsUserHasPermission(IUser user, string operation)
        {
            //return GetUserPermissions(user).Any(p => p.Operation.Equals(operation, System.StringComparison.CurrentCultureIgnoreCase));
            return this.IsUserHasPermission(user, operation, null);
        }

        public virtual bool IsUserHasPermission(IUser user, string operation, string url)
        {
            return GetUserPermissions(user).Any(p => p.Operation.Equals(operation, System.StringComparison.CurrentCultureIgnoreCase)
                && (string.IsNullOrEmpty(url) || p.Url.Equals(url, System.StringComparison.CurrentCultureIgnoreCase)));
        }

        public virtual string GetUserPermissionRule(IUser user, string operation)
        {
            //从用户权限集合中找出匹配operation并且规则表达式不为空的权限
            IEnumerable<SecurityRule> rules =
                from p in GetUserPermissions(user)
                where operation.Equals(p.Operation, System.StringComparison.CurrentCultureIgnoreCase) && !string.IsNullOrEmpty(p.Rule)
                select new SecurityRule() { Text = p.Rule, Priority = p.Priority };

            int count;
            if ((count = rules.Count()) > 0)
            {
                return (count == 1 ? rules.First() : rules.OrderBy(rule => rule.Priority).First()).Text;
            }

            return null;
        }

        public string GetUserPermissionBehaviour(IUser user, string operation)
        {
            //从用户权限集合中找出匹配operation并且规则表达式不为空的权限
            IEnumerable<GenericPermission> permissions =
                GetUserPermissions(user).Where(p => operation.Equals(p.Operation, System.StringComparison.CurrentCultureIgnoreCase)).OrderBy(p => p.Priority);

            //当拥有多个行为时，取优先级最高的
            GenericPermission permission = permissions.FirstOrDefault();

            return null == permission ? null : permission.Behaviour;
        }

        public virtual IEnumerable<UrlPermission> GetAllUrlPermissions(IUser user)
        {
            return user.Data.GetValue(AllUrlPermissionsKey,
                                      () => Store.GetAllUrlPermissions()) as IEnumerable<UrlPermission>;
        }

        public virtual IEnumerable<UIPermission> GetAllUIPermissions(IUser user)
        {
            return user.Data.GetValue(AllUIPermissionsKey,
                                      () => Store.GetAllUIPermissions()) as IEnumerable<UIPermission>;
        }

        /*protected virtual IEnumerable<GenericPermission> GetUserUrlPermissions(IUser user)
        {
            return user.Data.GetValue(UserUrlPermissionsKey,() =>
                    {
                        return from p in GetUserPermissions(user)
                               where !string.IsNullOrEmpty(p.Url)
                               select p;
                    }) as IEnumerable<GenericPermission>;
        }*/

        protected virtual IEnumerable<GenericPermission> GetUserPermissions(IUser user)
        {
            return user.Data.GetValue(UserAllPermissionsKey,
                                     () => Store.GetAllUserPermissions(user)) as IEnumerable<GenericPermission>;
        }

        /*
        public virtual bool IsUserHasPermissionOfUI(IUser user, string operation, out UIBehaviour behaviour)
        {
            GenericPermission permission =
                GetUserPermissions(user).SingleOrDefault(p => p.Operation.Equals(operation));

            if (null == permission)
            {
                //TODO : 默认为不可见
                behaviour = new UIBehaviour();
                return false;
            }
            else
            {
                bool invisible = UIBehaviour.InvisibleString.EqualsIgnoreCase(permission.Behaviour);
                bool disabled = UIBehaviour.DisabledString.EqualsIgnoreCase(permission.Behaviour);

                behaviour = new UIBehaviour() { Invisible = invisible, Disabled = disabled };

                return true;
            }
        }
        */

        /// <summary>
        /// 替换掉以下字符
        /// 1、~打头的替换为ApplicationPaht
        /// 2、替换掉？和*
        /// </summary>
        /// <param name="wildcardStr"></param>
        /// <returns></returns>
        private static string GetWildcardRegexString(string wildcardStr)
        {
            Regex replace = new Regex("^[~]|[.$^{\\[(|)*+?\\\\]");
            string ret = replace.Replace(wildcardStr,
                 delegate(Match m)
                 {
                     switch (m.Value)
                     {
                         case "?":
                             return ".?";
                         case "*":
                             return ".*";
                         case "~":
                             return GetVirthPath();
                         default:
                             return "\\" + m.Value;
                     }
                 });

            return "^" + ret + "$";
        }

        private static string GetVirthPath()
        {
            if (null == HttpContext.Current)
            { return string.Empty; }

            string applicationPath = HttpContext.Current.Request.ApplicationPath;
            return ("/".Equals(applicationPath) || string.IsNullOrEmpty(applicationPath)) ? string.Empty : applicationPath;
        }
    }
}