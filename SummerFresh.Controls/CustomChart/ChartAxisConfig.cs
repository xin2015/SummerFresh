using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Controls
{
    public class ChartXAxisConfig
    {
        public ChartXAxisConfig()
        {
            DataSourceName = "default";
        }

        public string DataSourceName { get; set; }

        public string AxisTitlt { get; set; }

        public string FieldName { get; set; }

        public Func<object, string> FieldViewFormatter { get; set; }
    }

    public class ChartYAxisConfig
    {
        public ChartYAxisConfig()
        {
            Max = decimal.MinValue;
            Min = decimal.MinValue;
        }

        public string AxisTitle { get; set; }

        public decimal Max { get; set; }

        public decimal Min { get; set; }
    }
}
