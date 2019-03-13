using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SummerFresh.Data;
using SummerFresh.Data.Mapping;
using SummerFresh.Security.Permission;
using SummerFresh.Security.Principal;
using SummerFresh.Security.Rule;
using System.Linq;

namespace SummerFresh.Security.Store
{
    public class SecurityStore : ISecurityStore
    {
        public const string GetUserLoginInfoCommand      = "Security.GetUserLoginInfo";
        public const string GetUserByLoginIdCommand      = "Security.GetUserByLoginId";
        public const string GetAllUserRolesCommand       = "Security.GetAllUserRoles";
        public const string GetAllUserPermissionsCommand = "Security.GetAllUserPermissions";
        public const string GetAllUrlPermissionsCommand  = "Security.GetAllUrlPermissions";
        public const string GetAllUIPermissionsCommand   = "Security.GetAllUIPermissions";

        /*
        public const string GetUserHasPermissionCommand    = "Security.GetUserHasPermission";
        public const string GetUserPermissionRulesCommand  = "Security.GetUserPermissionRules";
        */

        protected Dao _dao;

        public virtual Dao Dao
        {
            get { return _dao ?? Dao.Get(); }
            set { _dao = value; }
        }

        public virtual IUser GetUserLoginInfo(string loginId)
        {
            return Dao.QueryEntity<User>(GetUserLoginInfoCommand,new{LoginId = loginId});
        }

        public virtual IUser GetUserByLoginId(string loginId)
        {
            IDictionary<string, object> data = Dao.QueryDictionary(GetUserByLoginIdCommand, new {LoginId = loginId});
            if (null != data)
            {
                Type type =  typeof(User);

                IUser user = TypeMapper.Read<IUser>(data, type);

                foreach (string key in data.Keys)
                {
                    user.Properties[key.Replace(" ", "_")] = data[key];
                }
                return user;
            }
            else
            {
                return null;
            }
        }

        public virtual IEnumerable<IRole> GetAllUserRoles(string userId)
        {
            return Dao.QueryEntities<IRole>(typeof(Role),GetAllUserRolesCommand, 
                                                         new {UserId = userId});
        }

        public virtual IEnumerable<GenericPermission> GetAllUserPermissions(IUser user)
        {
            return Dao.QueryEntities<GenericPermission>(GetAllUserPermissionsCommand, 
                                                        new {UserId = user.UserId,UserRoles = GetRoles(user)});
        }

        public virtual IEnumerable<UrlPermission> GetAllUrlPermissions()
        {
            return Dao.QueryEntities<UrlPermission>(GetAllUrlPermissionsCommand);
        }

        public IEnumerable<UIPermission> GetAllUIPermissions()
        {
            return Dao.QueryEntities<UIPermission>(GetAllUIPermissionsCommand);
        }

        /*
        public IEnumerable<UrlPermission> GetUserUrlPermissions(string virtualPath)
        {
            return Dao.QueryEntities<UrlPermission>(GetUrlPermissionsByPathCommand, new {Path = virtualPath});
        }

        public bool GetUserHasPermission(string userId, string operation)
        {
            return Dao.Exists(GetUserHasPermissionCommand, new {UserId = userId, Operation = operation});
        }

        public IEnumerable<Rule.SecurityRule> GetUserPermissionRules(string userId,string operation)
        {
            return Dao.QueryEntities<SecurityRule>(
                    GetUserPermissionRulesCommand, new{UserId = userId, Operation = operation});
        }
        */

        protected virtual string[] GetRoles(IUser user)
        {
            return (from role in user.Roles select role.Id).ToArray();
        }
    }
}