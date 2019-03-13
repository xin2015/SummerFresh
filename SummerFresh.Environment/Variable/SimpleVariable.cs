using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Environment.Variable
{
    internal class SimpleVariable : BaseVariable
    {
        public SimpleVariable()
        {
            
        }

        public SimpleVariable(string name,object value)
        {
            Name  = name;
            Value = value;
        }

        public override Scope Scope
        {
            get { return Scope.Application; }
        }

        public virtual object Value { get; set; }

        public override object Evaluate()
        {
            return Value;
        }
    }
}