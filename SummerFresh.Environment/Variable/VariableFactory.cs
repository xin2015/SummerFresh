using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using SummerFresh.Util;
using SummerFresh.Environment.Config;
using SummerFresh.Basic;
namespace SummerFresh.Environment.Variable
{
    public class VariableFactory : IVariableFactory
    {
        private static readonly ILog log = LogManager.GetCurrentClassLogger();

        public IEnvironmentVariable CreateVariable(VariableElement variable)
        {
            Type type = variable.Type ?? typeof(string);

            if (typeof(IEnvironmentVariable).IsAssignableFrom(type))
            {
                IEnvironmentVariable environmentVariable = (IEnvironmentVariable)System.Activator.CreateInstance(type);
                environmentVariable.Name = variable.Name;
                environmentVariable.Scope = variable.Scope;
                return environmentVariable;
            }

            if (string.IsNullOrEmpty(variable.Value))
            {
                throw new ConfigurationErrorsException(
                            string.Format("'value' of variable element '{0}' cant not be empty",variable.Name));
            }

            if (EnvironmentFactory.Parser.HasVariable(variable.Value))
            {
                log.Debug("'{0}' Has Variables,Create Dynamic Variable '{1}'",variable.Value,variable.Name);

                return new DynamicVariable(variable);
            }
            else
            {
                object value = variable.Value.ConventToType(type);

                log.Debug("Create Simple Variable '{0}' = {1}",variable.Name,value);

                return new SimpleVariable(variable.Name, value);
            }
        }
    }
}