using System;
using System.Collections.Generic;
using System.Security.Principal;
using SummerFresh.Security.Cache;
using SummerFresh.Basic;
namespace SummerFresh.Security.Principal
{
    [Serializable]
    public class User : IUser
    {
        protected string _id;
        protected string _name;
        protected string _loginId;
        protected IIdentity _identity;
        protected IPrincipal _principal;
        protected List<IRole> _roles = new List<IRole>();
        protected IUserState _data = new UserState();

        protected IDictionary<string, object> _properties = new Dictionary<string, object>();

        public virtual string UserId
        {
            get { return _id ?? LoginId; }
            set { _id = value; }
        }

        public virtual string UserCode { get; set; }

        public virtual string UserName
        {
            get { return _name ?? LoginId; }
            set { _name = value; }
        }

        public virtual string LoginId
        {
            get { return _loginId ?? (null == _identity ? null : _identity.Name); }
            set { _loginId = value; }
        }

        public virtual string Password { get; set; }

        public virtual string Email { get; set; }

        public virtual string MobilePhone { get; set; }

        public virtual string Sex { get; set; }

        public virtual int Age { get; set; }


        public virtual DateTime Birthday { get; set; }

        public virtual string DepartmentId { get; set; }

        public virtual IDictionary<string, object> Properties
        {
            get { return _properties; }
        }

        public virtual IEnumerable<IRole> Roles
        {
            get { return _roles; }
            set
            {
                _roles.Clear();
                value.ForEach(role => _roles.Add(role));
            }
        }

        public virtual bool IsAuthenticated
        {
            get { return Identity.IsAuthenticated; }
        }

        public virtual bool HasPermission(string operation)
        {
            return SecurityFactory.Provider.HasPermission(operation);
        }

        public virtual bool IsInRole(string roleId)
        {
            //ExpressionContext.User = this;
            //return Parser.Compile(roleId, TokenType.Role).Evaluate();
            return false;
        }

        public virtual IIdentity Identity
        {
            get { return _identity ?? Principal.Identity; }
            set
            {
                _identity = value;
                _principal = null;
            }
        }

        public virtual IPrincipal Principal
        {
            get { return _principal ?? this; }
            set
            {
                _principal = value;
                _identity = value.Identity;
            }
        }

        public virtual IUserState Data
        {
            get { return _data; }
        }
    }
}