using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Data
{
    public interface ISqlParameters
    {
        object Resolve(string name);

        bool TryResolve(string name, out object value);
    }
}
