using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Environment.Config;
using SummerFresh.Basic;
namespace SummerFresh.Environment.Variable
{
    internal class DynamicVariable : BaseVariable
    {
        private readonly string _expression;
        private readonly Type   _valueType;

        public DynamicVariable(VariableElement variable)
        {
            _expression = variable.Value;
            _valueType  = variable.Type ?? typeof (string);
            this.Scope  = variable.Scope;
        }

        public override object Evaluate()
        {
            string value = Env.Parse(_expression);
            return value.ConventToType(_valueType);
        }
    }
}
