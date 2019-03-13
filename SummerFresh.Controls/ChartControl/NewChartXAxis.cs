using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Controls
{
    public class NewChartXAxis : ISerializeable
    {
        public object[] Categories { get; set; }
        public Dictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>() { { "categories", Categories } };
        }
    }
}
