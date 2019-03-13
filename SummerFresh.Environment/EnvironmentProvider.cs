using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using SummerFresh.Util;
using SummerFresh.Environment.Config;
using SummerFresh.Environment.Variable;
using SummerFresh.Basic;
namespace SummerFresh.Environment
{
    /// <summary>
    /// <see cref="SummerFresh.Environment.IEnvironmentProvider">IEnvironmentProvider</see>的默认实现
    /// </summary>
    /// <remarks>
    /// 此实现读出配置节点下的&lt;variable/&gt;元素并加载对应的Resolver进行解析
    /// </remarks>
    public class EnvironmentProvider : IEnvironmentProvider
    {
        private static readonly ILog log = LogManager.GetCurrentClassLogger();

        private IDictionary<string,IEnvironmentVariable> _variables;
        private IVariableFactory _variableFactory = new VariableFactory();

        public string Prefix { get; set; }

        public bool Configure(IEnvironmentContainer container, ProviderElement providerElement)
        {
            lock (this)
            {
                _variables = new Dictionary<string, IEnvironmentVariable>();

                providerElement.XmlElement.Elements().ForEach(el =>
                {
                    if (!el.Name.LocalName.Equals("variable"))
                    {
                        log.Debug("Found Unkonw Element '{0}'", el.Name.LocalName);
                    }
                    else
                    {
                        VariableElement element = new VariableElement(); ;

                        element.Deserialize(el);

                        string name = element.Name;

                        log.Debug("config variable '{0}',factory : '{1}', type : '{2}', value : '{3}'",
                                  name, element.Factory, element.TypeName, element.Value);

                        IEnvironmentVariable variable = null;

                        if (!string.IsNullOrEmpty(element.Factory))
                        {
                            string factoryName = element.Factory.Trim();

                            IVariableFactory factory = container.Configuration.GetFactory(factoryName);

                            if (null == factory)
                            {
                                throw new ConfigurationErrorsException(
                                            string.Format("Unknow Factory '{0}'",factoryName));
                            }

                            log.Debug("Create Variable '{0}' By Factory : '{1}'", name, factoryName);

                            variable = factory.CreateVariable(element);
                        }
                        else
                        {
                            log.Debug("Create Variable '{0}' By Default Factory", name);

                            variable = _variableFactory.CreateVariable(element);
                        }

                        log.Debug("Load Variable '{0}',Type : '{1}'", name, variable.GetType().FullName);

                        _variables[name] = variable;
                    }
                });

                log.Debug("found {0} configured variables", _variables.Count);
            }
            return true;
        }

        public IEnvironmentVariable GetVariable(string name)
        {
            IEnvironmentVariable variable;
            return _variables.TryGetValue(name, out variable) ? variable : null;
        }        
    }
}