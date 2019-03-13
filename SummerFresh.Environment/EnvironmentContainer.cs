using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using SummerFresh.Util;
using SummerFresh.Environment.Config;
using SummerFresh.Environment.Variable;
using SummerFresh.Data;

namespace SummerFresh.Environment
{
    /// <summary>
    /// <see cref="T:SummerFresh.Environment.IEnvironmentContainer">IEnvironmentContainer</see>的默认实现
    /// </summary>
    public class EnvironmentContainer : IEnvironmentContainer
    {
        private static readonly ILog log = LogManager.GetCurrentClassLogger();

        public const string DefaultConfigFileName = "Environment.config";

        public const string DefaultConfigDiretory = "Env";

        [ThreadStatic]
        private static IDictionary<string, object> _requestCache;

        private static IDictionary<string, object> RequestCache
        {
            get { return _requestCache ?? (_requestCache = new Dictionary<string, object>()); }
        }

        private string _configFile = DefaultConfigFileName;
        private readonly IList<IEnvironmentProvider> _providers = new List<IEnvironmentProvider>();
        private readonly IDictionary<string, object> _caches = new Dictionary<string, object>();
        private readonly IDictionary<string, IEnvironmentVariable> _registry = new Dictionary<string, IEnvironmentVariable>();
        private readonly ReaderWriterLockSlim _cachesLock = new ReaderWriterLockSlim();
        private readonly ReaderWriterLockSlim _registryLock = new ReaderWriterLockSlim();
        private readonly ReaderWriterLockSlim _sessionLock = new ReaderWriterLockSlim();
        private readonly string _cacheKey = "Environment.Cache$" + Guid.NewGuid().ToString();
        private readonly object _sessionSync = new object();

        #region constructor and properties
        public EnvironmentContainer(string configFile = null)
        {
            if (!string.IsNullOrEmpty(configFile))
            {
                _configFile = configFile;
            }
            this.Initialize();
        }

        public virtual IEnumerable<IEnvironmentProvider> Providers
        {
            get { return _providers; }
        }

        public virtual string ConfigFile
        {
            get { return _configFile; }
        }

        public EnvironmentSection Configuration { get; private set; }
        #endregion

        #region public methods
        public virtual void Register(string name, object value)
        {
            Register(name, new SimpleVariable(name, value));
        }

        public virtual void Register(string name, IEnvironmentVariable variable)
        {
            if (null == name || null == variable)
            {
                throw new ArgumentNullException();
            }

            _registryLock.EnterWriteLock();
            try
            {
                //if name exists ,replace it
                variable.Name = name;
                _registry[name] = variable;
            }
            finally
            {
                _registryLock.ExitWriteLock();
            }
        }

        public virtual object Resolve(string name)
        {
            object value;
            return TryResolve(name, out value) ? value : null;
        }

        public virtual bool TryResolve(string name, out object value)
        {
            IEnvironmentVariable variable;

            string prefix = GetPrefix(name);
            bool usePrefix = false;

            //Get From Resolved Varaibles Cache
            usePrefix = GetResolvedVariable(prefix, name, out variable);
            log.Debug("Variable '{0}' {1}found in resolved cache", name, null == variable ? "not " : "");

            //If Not Resolved, Resolve it From Providers
            if (null == variable)
            {
                usePrefix = ResolveVariable(prefix, name, out variable);
            }
            log.Debug("Variable '{0}' {1}found in resolved from providers", name, null == variable ? "not " : "");

            //Resolve variable value from cache or evaluate it.
            if (null != variable)
            {
                if (ResolveValue(usePrefix ? prefix : null, name, variable, out value))
                {
                    log.Info("Variable '{0}' was resolved , value : {1}", name, value);
                    return true;
                }
                else
                {
                    log.Warn("Variable '{0}' can't resolved value", name);
                    value = null;
                    return false;
                }
            }

            //Not Resolved : TODO : Cache not resolved 'name' to improve performance
            value = null;
            return false;
        }
        #endregion

        #region resolve the variable by name
        protected virtual bool GetResolvedVariable(string prefix, string name, out IEnvironmentVariable variable)
        {
            _registryLock.EnterReadLock();
            try
            {
                if (_registry.TryGetValue(name, out variable))
                {
                    return false;
                }
                else if (null != prefix)
                {
                    // 允许向Container注册一个对象变量（是个变量集合），然后通过obj.attr去获取集合中的变量
                    return _registry.TryGetValue(prefix, out variable);
                }
                else
                {
                    variable = null;
                    return false;
                }
            }
            finally
            {
                _registryLock.ExitReadLock();
            }
        }

        protected virtual bool ResolveVariable(string prefix, string name, out IEnvironmentVariable variable)
        {
            log.Debug("Resolving variable from providers...");

            variable = null;
            foreach (IEnvironmentProvider provider in _providers)
            {
                string variableName = name;

                bool hasProviderPrefix = !string.IsNullOrEmpty(provider.Prefix);

                //前缀不匹配继续下一个Provider
                if (hasProviderPrefix && !provider.Prefix.Equals(prefix))
                {
                    log.Debug("Ignore provider '{0}',variable prefix '{1}' not match provider prefix '{2}'", provider.GetType().FullName, prefix, provider.Prefix);
                    continue;
                }

                log.Debug("Try to resolving variable from provider : {0}", provider.GetType().FullName);

                if (hasProviderPrefix)
                {
                    if (name.Length > provider.Prefix.Length + 1)
                    {
                        variableName = name.Substring(provider.Prefix.Length + 1);
                    }
                    else
                    {
                        continue;
                    }
                }

                bool usePrefix = false;
                variable = provider.GetVariable(variableName);

                //如果变量没有找到并且Provider没有前缀，告诉Provider找名称等于前缀的变量
                //疑问：为什么要找名称等于前缀的变量？  (这逻辑应该交由Provider处理比较合适)
                //支持从Provider中的变量是一个对象，用户引用环境变量语法可以是 "obj.attr"
                if (null == variable && !hasProviderPrefix && !string.IsNullOrEmpty(prefix))
                {
                    usePrefix = true;
                    variable = provider.GetVariable(prefix);
                }

                if (null != variable)
                {
                    if (log.IsDebugEnabled)
                    {
                        log.Debug("Variable was resolved as '{0}' by provider : {1}", variable.GetType().FullName, provider.GetType().FullName);
                    }
                    Register(usePrefix ? prefix : name, variable);
                    return usePrefix;
                }
            }
            return false;
        }
        #endregion

        #region get value from cache or evaluate it after the variable was resolved
        protected virtual bool ResolveValue(string prefix, string name, IEnvironmentVariable variable, out object value)
        {
            if (variable is SimpleVariable)
            {
                value = ((SimpleVariable)variable).Value;
                log.Trace("value of variable is not evaluatable,return it directly");
            }
            else if (Scope.None.Equals(variable.Scope))
            {
                log.Trace("variable scope is none,evaluate it's value");
                value = variable.Evaluate();
            }
            else if (Scope.Application.Equals(variable.Scope))
            {
                log.Trace("variable scope is application,resolve it's value from cache");
                value = ResolveValueFromApplicationCache(prefix ?? name, variable);
            }
            else if (Scope.Request.Equals(variable.Scope))
            {
                log.Trace("variable scope is request,resolve it's value from request cache");
                value = ResolveValueFromRequestCache(prefix ?? name, variable);
            }
            else
            {
                log.Trace("variable scope is session,resolve it's value from cache");
                value = ResolveValueFromSessionCache(prefix ?? name, variable);
            }

            if (null == prefix || null == value)
            {
                return true;
            }
            else
            {
                //允许向Container注册一个对象变量（是个变量集合），然后通过obj.attr去获取集合中的变量
                //has prefix and value is not null
                object obj = value;
                string nestedName = name.Substring(prefix.Length + 1);

                log.Trace("resolve nested varaible value in object,the name is : {0}", nestedName);

                return ResolveNestedValueFromObject(obj, nestedName, out value);
            }
        }

        protected virtual object ResolveValueFromApplicationCache(string name, IEnvironmentVariable variable)
        {
            _cachesLock.EnterUpgradeableReadLock();
            try
            {
                object value = null;
                if (_caches.TryGetValue(name, out value))
                {
                    return value;
                }
                else
                {
                    _cachesLock.EnterWriteLock();
                    try
                    {
                        value = variable.Evaluate();
                        _caches[name] = value;
                        return value;
                    }
                    finally
                    {
                        _cachesLock.ExitWriteLock();
                    }
                }
            }
            finally
            {
                _cachesLock.ExitUpgradeableReadLock();
            }
        }

        protected virtual object ResolveValueFromRequestCache(string name, IEnvironmentVariable variable)
        {
            HttpContext context = HttpContext.Current;

            IDictionary<string, object> cache = null;
            if (null != context)
            {
                if (context.Items.Contains(_cacheKey))
                {
                    cache = (IDictionary<string, object>)context.Items[_cacheKey];
                }
                else
                {
                    cache = new Dictionary<string, object>();
                    context.Items[_cacheKey] = cache;
                }
            }
            else
            {
                //如果不是在HttpContext环境中，那么使用线程上下文来作为当前请求的缓存
                cache = RequestCache;
            }

            object value;
            if (cache.TryGetValue(name, out value))
            {
                log.Trace("variable value resolved from request cache");
                return value;
            }
            else
            {
                log.Debug("variable was not cached,eval it's value and cache it");
                value = variable.Evaluate();
                cache[name] = value;
                return value;
            }
        }

        protected virtual object ResolveValueFromSessionCache(string name, IEnvironmentVariable variable)
        {
            HttpContext context = HttpContext.Current;
            var session = new SessionProvider();
            if (null == context)
            {
                log.Trace("not an Asp.Net Web environment,resolve from request cache");
                return ResolveValueFromRequestCache(name, variable);
            }
            else if (session.IsValid)
            {
                //double check lock to get or created the cached dictionary stored in session.
                IDictionary<string, object> cache = session[_cacheKey] as IDictionary<string, object>;
                if (null == cache)
                {
                    lock (_sessionSync)
                    {
                        cache = session[_cacheKey] as IDictionary<string, object>;
                        if (cache == null)
                        {
                            log.Info("create session cached variable dictionary");
                            cache = new Dictionary<string, object>();

                            session[_cacheKey] = cache;
                        }
                    }
                }

                _sessionLock.EnterUpgradeableReadLock();
                try
                {
                    object value;
                    if (cache.TryGetValue(name, out value))
                    {
                        log.Trace("variable value resolved from session cache");
                        return value;
                    }
                    else
                    {
                        log.Debug("variable was not cached,eval it's value and cache it");
                        value = variable.Evaluate();

                        _sessionLock.EnterWriteLock();
                        try
                        {
                            cache[name] = value;
                            return value;
                        }
                        finally
                        {
                            _sessionLock.ExitWriteLock();
                        }
                    }
                }
                finally
                {
                    _sessionLock.ExitUpgradeableReadLock();
                }
            }
            else
            {
                log.Debug("session is invalid,evaluate value directly.");
                return variable.Evaluate();
            }
        }

        protected virtual bool ResolveNestedValueFromObject(object obj, string name, out object value)
        {
            if (obj is IDictionary)
            {
                IDictionary dictionary = (IDictionary)obj;
                if (dictionary.Contains(name))
                {
                    value = dictionary[name];
                    return true;
                }
                else
                {
                    value = null;
                    return false;
                }
            }
            else
            {
                throw new NotSupportedException("only 'IDictionary' object was supported to resolving nested value");
            }
        }

        #endregion

        #region initialize container
        private void Initialize()
        {
            //Load Configuration
            EnvironmentSection section = LoadConfigAndCheck(_configFile);

            DirectoryInfo dir = null;
            if (AppUtility.FindDirectory("App_Config\\Env", out dir))
            {
                var files = dir.GetFiles("*.config", SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    ExeConfigurationFileMap map =
    new ExeConfigurationFileMap() { ExeConfigFilename = file.FullName };

                    Configuration configuration =
                        ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

                    var tempSection = configuration.GetSection(EnvironmentSection.SectionName) as EnvironmentSection;
                    foreach (ProviderElement provider in tempSection.Providers)
                    {
                        section.Providers.Add(provider);
                    }
                }
            }

            log.Debug("Found {0} configured providers", section.Providers.Count);

            Configuration = section;

            //Load Providers
            foreach (ProviderElement element in section.Providers)
            {
                log.Debug("provider prefix : '{0}' , type : '{0}'", element.Prefix, element.TypeName);

                //TODO : support create instance by ioc container.
                IEnvironmentProvider provider = Activator.CreateInstance(Type.GetType(element.TypeName)) as IEnvironmentProvider;

                //set prefix
                provider.Prefix = element.Prefix;

                if (provider.Configure(this, element))
                {
                    _providers.Add(provider);
                }
                else
                {
                    log.Debug("provider configure false,do not add it to container");
                }
            }
        }

        private static EnvironmentSection LoadConfigAndCheck(string configFile)
        {
            EnvironmentSection section =
                AppUtility.GetConfigSection<EnvironmentSection>(EnvironmentSection.SectionName, configFile);

            if (null == section)
            {
                throw new ConfigurationErrorsException(
                    string.Format("No '{0}' configuration section has been found", EnvironmentSection.SectionName));
            }

            if (section.Providers.Count == 0)
            {
                throw new ConfigurationErrorsException(
                    string.Format("No '{0}' element has been found in section '{1}'",
                                  ProviderElement.ElementName, EnvironmentSection.SectionName));
            }

            return section;
        }
        #endregion

        #region utility methods
        private static string GetPrefix(string name)
        {
            int dotIndex = name.IndexOf('.');

            return dotIndex > 0 ? name.Substring(0, dotIndex) : null;
        }
        #endregion
    }
}