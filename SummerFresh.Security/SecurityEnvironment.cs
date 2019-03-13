using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using SummerFresh.Environment;
using SummerFresh.Environment.Config;
using SummerFresh.Environment.Variable;
using SummerFresh.Security.Principal;
using SummerFresh.Basic.FastReflection;

namespace SummerFresh.Security
{
    public class SecurityEnvironment : IEnvironmentProvider
    {
        private string _prefix;

        public string Prefix
        {
            get { return _prefix; }
            set { _prefix = value ?? ""; }
        }

        public bool Configure(IEnvironmentContainer container, ProviderElement providerElement)
        {
            return true;
        }

        public IEnvironmentVariable GetVariable(string name)
        {
            IUser user = SecurityContext.User;

            PropertyInfo propInfo = GetProperty(user.GetType(), name);

            if (null != propInfo)
            {
                return new UserPropertyVariable(name, new FastProperty(propInfo.Name,propInfo));
            }
            else
            {
                string extendPropName = GetProperty(user.Properties, name);

                if (null != extendPropName)
                {
                    return new UserDetailPropertyVariable(name, extendPropName);    
                }
            }
            return null;
        }

        protected PropertyInfo GetProperty(Type type,string name)
        {
            foreach (PropertyInfo prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty))
            {
                if (name.Equals(prop.Name,StringComparison.OrdinalIgnoreCase))
                {
                    return prop;
                }
            }
            return null;
        }

        protected string GetProperty(IDictionary<string,object> properties,string name)
        {
            foreach (string key in properties.Keys)
            {
                if (key.Equals(name,StringComparison.OrdinalIgnoreCase))
                {
                    return key;
                }
            }
            return null;
        }
    }

    internal class UserPropertyVariable : IEnvironmentVariable
    {
        private readonly string       _name;
        private readonly FastProperty _prop;

        public UserPropertyVariable(string name,FastProperty prop)
        {
            this._name = name;
            this._prop = prop;
        }

        public object Evaluate()
        {
            return _prop.GetValue(SecurityContext.User);
        }

        public string Name
        {
            get { return _name; }
            set { }
        }

        public Scope Scope
        {
            get { return Scope.Request; }
            set { }
        }
    }

    internal class UserDetailPropertyVariable : IEnvironmentVariable
    {
        private readonly string _variableName;
        private readonly string _propertyName;

        public UserDetailPropertyVariable(string variableName,string propertyName)
        {
            this._variableName = variableName;
            this._propertyName = propertyName;
        }

        public object Evaluate()
        {
            object value;
            return SecurityContext.User.Properties.TryGetValue(_propertyName, out value) ? value : null;
        }

        public string Name
        {
            get { return _variableName; }
            set { }
        }

        public Scope Scope
        {
            get { return Scope.Request; }
            set {  }
        }
    }
}