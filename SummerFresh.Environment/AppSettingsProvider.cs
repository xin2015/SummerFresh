using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Security;
using SummerFresh.Environment.Config;

namespace SummerFresh.Environment
{
    public class AppSettingsProvider : IEnvironmentProvider
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
            var variable = new AppSettingVariable(name);

            return variable.Evaluate() == null ? null : variable;
        }
    }

    internal class AppSettingVariable : IEnvironmentVariable
    {
        private readonly string _name;

        public AppSettingVariable(string name)
        {
            this._name = name;
        }

        public object Evaluate()
        {
            return ConfigurationManager.AppSettings[this._name];
        }

        public string Name
        {
            get { return _name; }
            set { }
        }

        public Scope Scope
        {
            get { return Scope.None; }
            set { }
        }
    }

    public class QueryStringProvider : IEnvironmentProvider
    {
        public string Prefix
        {
            get;
            set;
        }

        public bool Configure(IEnvironmentContainer container, ProviderElement providerElement)
        {
            return true;
        }

        public IEnvironmentVariable GetVariable(string name)
        {
            var result = new QueryStringParameters();
            result.Name = name;
            if (result.Name.IndexOf(".") > 0)
            {
                result.Name = name.Substring(name.IndexOf("."));
            }
            return result;
        }


    }
}