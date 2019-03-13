using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Environment.Variable
{
    public abstract class BaseVariable : IEnvironmentVariable
    {
        public virtual string Name { get; set; }

        public virtual Scope Scope { get; set; }

        public abstract object Evaluate();
    }
}