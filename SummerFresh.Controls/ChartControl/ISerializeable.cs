using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Controls
{
    interface ISerializeable
    {
        Dictionary<string, object> Serialize();
    }
}
