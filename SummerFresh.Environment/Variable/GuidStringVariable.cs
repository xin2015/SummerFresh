using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Environment.Variable
{
    /// <summary>
    /// Guid类型的环境变量
    /// </summary>
    class GuidStringVariable:BaseVariable
    {
        public GuidStringVariable()
        {
            
        }

        public GuidStringVariable(String name)
        {
            this.Name = name;
        }

        public override object Evaluate()
        {
            return Guid.NewGuid().ToString();
        }

        public override Scope Scope
        {
            get
            {
                return Scope.None;
            }
        }
    }
}
