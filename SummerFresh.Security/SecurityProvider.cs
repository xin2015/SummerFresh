using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using SummerFresh.Security.Authentication;
using SummerFresh.Security.Authorization;
using SummerFresh.Security.Permission;
using SummerFresh.Security.Principal;
using SummerFresh.Security.Store;
using SummerFresh.Util;
using SummerFresh.Security.Cache;
using SummerFresh.Basic;
using SummerFresh.Environment;
namespace SummerFresh.Security
{
    public class SecurityProvider : ISecurityProvider
    {
        private static readonly ILog log = LogManager.GetCurrentClassLogger();

        private const string KeyContextAuthorized = "SecurityProvider.Authorized";
        private const string KeyHasUrlPermission = "HasUrlPermission:{0}";
        private const string KeyHasOperationPermission = "HasOpPermission:{0}";
        private const string KeyPermissionRule = "PermissionRule:{0}";
        private const string KeyPermissionBehaviour = "PermissionBehaviour:{0}";
        private const string KeyPageUISecuirty = "UISecurity:{0}";
        private const string KeyPageUIPermissions = "UIPermissions:{0}";

        private static readonly string UserSessionKey = typeof(SecurityProvider).FullName + "$User";
        private static readonly string UserCacheKey = typeof(SecurityProvider).FullName + "$Cache";

        private ISecurityStore _store;
        private ISecurityAuthorizer _authorizer;
        private ISecurityAuthenticator _authenticator;
        private ISessionProvider _session;

        #region 接口方法
        public virtual bool Authenticate(string username, string password)
        {
            return Authenticator.Authenticate(username, password);
        }

        public virtual string EncryptPassword(string password)
        {
            return Authenticator.EncryptPassword(password);
        }

        public virtual bool HasPermissionOfUrl(string virtualPath, string queryString)
        {
            string op;
            return HasPermissionOfUrl(virtualPath, queryString, out op);
        }

        public bool HasPermissionOfUrl(IUser user, string virtualPath, string queryString)
        {
            string op;
            return HasPermissionOfUrl(user, virtualPath, queryString, out op);
        }

        public virtual bool HasPermissionOfUrl(string virtualPath, string queryString, out string operation)
        {
            return HasPermissionOfUrl(GetCurrentUser(), virtualPath, queryString, out operation);
        }

        public bool HasPermissionOfUrl(IUser user, string virtualPath, string queryString, out string operation)
        {
            string virtualUrl = virtualPath + queryString;

            object[] data = CacheExecute(string.Format(KeyHasUrlPermission, virtualUrl), () =>
            {
                UrlPermission permission;

                bool hasPermission =
                    Authorizer.IsUserHasPermissionOfUrl(user, virtualPath, queryString, out permission);

                return new object[] { hasPermission, null == permission ? null : permission.Operation };
            }, this.IsCurrentUser(user));

            operation = data[1] as string;
            return (bool)data[0];
        }

        public bool HasPermission(string operation)
        {
            return HasPermission(this.GetCurrentUser(), operation);
        }

        public bool HasPermission(IUser user, string operation)
        {
            if (string.IsNullOrEmpty(operation))
            {
                return false;
            }

            //if (Parser.IsWord(operation))
            //{
            string key = string.Format(KeyHasOperationPermission, operation);

            return CacheExecute(key, () => Authorizer.IsUserHasPermission(user, operation), this.IsCurrentUser(user));
            //}
            //else
            //{
            //    ExpressionContext.User = user;
            //    return Parser.Compile(operation).Evaluate();
            //}
        }

        public IDictionary<string, bool> HasPermissions(params string[] operations)
        {
            return this.HasPermissions(this.GetCurrentUser(), operations);
        }

        public IDictionary<string, bool> HasPermissions(IUser user, params string[] operations)
        {
            IDictionary<string, bool> result = new Dictionary<string, bool>();
            foreach (string operation in operations)
            {
                result.Add(operation, HasPermission(user, operation));
            }
            return result;
        }

        public string GetPermissionRule(string operation)
        {
            return this.GetPermissionRule(this.GetCurrentUser(), operation);
        }

        public string GetPermissionRule(IUser user, string operation)
        {
            string key = string.Format(KeyPermissionRule, operation);
            return CacheExecute(key, () => Authorizer.GetUserPermissionRule(user, operation), this.IsCurrentUser(user));
        }

        public string GetPermissionBehaviour(string operation)
        {
            return this.GetPermissionBehaviour(this.GetCurrentUser(), operation);
        }

        public string GetPermissionBehaviour(IUser user, string operation)
        {
            string key = string.Format(KeyPermissionBehaviour, operation);
            return CacheExecute(key, () => Authorizer.GetUserPermissionBehaviour(user, operation), this.IsCurrentUser(user));
        }

        public bool Authenticate(HttpContextBase context)
        {
            //TODO 疑问：匿名用户访问应用，此时User为空？如果是，这里是否存在问题？
            IUser user = GetCurrentUser();
            context.User = user;
            return user != null ? user.IsAuthenticated : false;
        }

        public void Authorize(HttpContextBase context)
        {
            if (null != context.Items[KeyContextAuthorized])
            {
                log.Info("context has been authorized,should not authorize again");
                return;
            }

            //判断当前Url是否拥有权限访问
            HttpRequestBase request = context.Request;
            if (!HasPermissionOfUrl(request.Path, request.Url.Query))
            {
                context.Response.StatusCode = int.Parse(System.Configuration.ConfigurationManager.AppSettings["SummerFresh.Security.NotPermissionResponseStatusCode"] ?? "403");
                string redirect =
                    System.Configuration.ConfigurationManager.AppSettings[
                        "SummerFresh.Security.NotPermissionResponseRedirect"] ?? "~/Home/Page403";
                context.Response.Redirect(redirect);
                context.Response.End();
            }


            //记录当前的上下文已经经过授权处理
            context.Items[KeyContextAuthorized] = true;
        }


        public virtual IUser GetCurrentUser()
        {
            IPrincipal principal = HttpContext.Current.User;

            if (principal is IUser)
            {
                return principal as IUser;
            }

            if (null != principal)
            {
                if (principal.Identity.IsAuthenticated)
                {
                    IUser user = GetUser(principal);

                    if (null == user)
                    {
                        throw new InvalidOperationException(
                                    string.Format("Invalid User State,Return Null For Authenticated User '{0}'",
                                                    principal.Identity.Name));
                    }
                    return user;
                }
                else
                {
                    return new User() { Principal = principal };
                }
            }
            return null;
        }

        public virtual void Abandon()
        {
            try
            {
                RemoveUserFromSession();
            }
            catch (Exception e)
            {
                log.Error("Error Abandon Current User : {0}", e.Message, e);
            }
        }

        public virtual IDictionary<string, UISecurityBehaviour> GetUISecurityBehaviours(string virtualPath, string queryString)
        {
            string permissionsKey = string.Format(KeyPageUIPermissions, virtualPath);
            return CacheExecute(permissionsKey, () =>
            {
                IEnumerable<UIPermission> permissions =
                    Authorizer.GetUIPermissionsOfUrl(GetCurrentUser(), virtualPath, queryString);

                IDictionary<string, UISecurityBehaviour> behaviours = new Dictionary<string, UISecurityBehaviour>();

                permissions.ForEach(p =>
                {
                    string behaviour = GetPermissionBehaviour(p.Operation);

                    if (!string.IsNullOrEmpty(behaviour))
                    {
                        behaviours[p.ElementId] = new UISecurityBehaviour() { Behaviour = behaviour };
                    }
                    else if (!SecurityContext.HasPermission(p.Operation))
                    {
                        behaviour = string.IsNullOrEmpty(p.ElementBehaviour)
                                               ? UISecurityBehaviour.Invisible
                                               : p.ElementBehaviour;

                        behaviours[p.ElementId] = new UISecurityBehaviour() { Behaviour = behaviour };
                    }
                    else
                    {
                        behaviours[p.ElementId] = new UISecurityBehaviour();
                    }
                });
                return behaviours;
            });
        }

        #endregion



        #region 缓存实现
        protected virtual string UserId
        {
            get { return GetCurrentUser().UserId; }
        }

        protected virtual IUserState UserCache
        {
            get
            {
                return (IUserState)GetCurrentUser().Data.GetValue(UserCacheKey, () => new UserState());
            }
        }

        protected virtual T CacheExecute<T>(string key, Func<T> execute, bool isNeed = true)
        {
            object value;
            if (isNeed && UserCache.TryGetValue(key, out value))
            {
                return (T)value;
            }
            else
            {
                T newValue = execute();
                if (isNeed)
                {
                    UserCache[key] = newValue;
                }
                return newValue;
            }
        }
        #endregion

        #region 属性成员
        public virtual bool RequireSessionState
        {
            get { return true; }
        }

        public ISecurityStore Store
        {
            get { return _store ?? SecurityFactory.Store; }
            set { _store = value; }
        }

        public ISessionProvider AppSession
        {
            get
            {
                return _session ?? (_session = new SessionProvider());
            }
            set
            {
                _session = value;
            }
        }

        public ISecurityAuthenticator Authenticator
        {
            get { return _authenticator ?? SecurityFactory.Authenticator; }
            set { _authenticator = value; }
        }

        public ISecurityAuthorizer Authorizer
        {
            get { return _authorizer ?? SecurityFactory.Authorizer; }
            set { _authorizer = value; }
        }
        #endregion

        #region 内部方法
        protected virtual IUser GetUser(IPrincipal principal)
        {
            string loginId = GetLoginIdFromPrincipal(principal);

            //Get From Session
            IUser user = GetUserFromSession(loginId);

            //If Not Found , Get From Store
            if (null == user)
            {
                user = Store.GetUserByLoginId(loginId);

                if (null == user)
                {
                    throw new Exception(
                        string.Format("User Not Found!", loginId));
                }

                user.Principal = principal;
                user.Roles = Store.GetAllUserRoles(user.UserId);

                StoreUserInSession(user);
            }
            else if (log.IsDebugEnabled)
            {
                //log.Debug("Load User '{0}' From Session : Id = '{1}' , Name = '{2}'", principal.Identity.Name, user.UserId, user.UserName);
            }

            return user;
        }

        public IUser GetUser(string loginId)
        {
            if (loginId == null)
            {
                throw new ArgumentNullException("登录账号Id不能为空！");
            }
            var user = Store.GetUserByLoginId(loginId);
            if (user != null)
            {
                user.Roles = Store.GetAllUserRoles(user.UserId);
            }

            return user;
        }

        public bool IsCurrentUser(IUser user)
        {
            return user == this.GetCurrentUser();
        }


        protected virtual string GetLoginIdFromPrincipal(IPrincipal principal)
        {
            string name = principal.Identity.Name;

            if (principal.Identity is WindowsIdentity)
            {
                int index = name.IndexOf("\\");

                return index > 0 ? name.Substring(index + 1) : name;
            }
            else
            {
                return name;
            }
        }

        protected virtual IUser GetUserFromSession(string name)
        {

            IUser user = AppSession.IsValid ? (AppSession[UserSessionKey] as IUser) : null;

            //如果发现会话中的用户和当前用户不一致则移除会话中的用户，以当前用户为准
            if (null != user && !user.LoginId.Equals(name))
            {
                RemoveUserFromSession();
                user = null;
            }

            return user;
        }

        protected virtual void StoreUserInSession(IUser user)
        {
            if (AppSession.IsValid)
            {
                log.Debug("Store User In Session : Id = '{0}',Name = '{1}'", user.UserId, user.UserName);
                AppSession[UserSessionKey] = user;
            }
        }

        protected virtual void RemoveUserFromSession()
        {
            if (AppSession.IsValid)
            {
                log.Debug("Remove User From Session");
                AppSession.Remove(UserSessionKey);
            }
        }
        #endregion
    }
}