using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Controls
{
    interface IComplexable
    {
        Dictionary<string, object> Combine(IEnumerable list);
    }
}
