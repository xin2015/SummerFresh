using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SummerFresh.Business;

namespace SummerFresh.Controls
{
    public class ChartSeriesConfig
    {
        public ChartSeriesConfig()
        {
            DataSourceName = "default";
        }

        public string DataSourceName { get; set; }

        public ChartType ChartType { get; set; }

        public string SeriesName { get; set; }

        public string SeriesFieldName { get; set; }

        public string DataFieldName { get; set; }

        public Func<object, string> DataValueFormatter { get; set; }

        public int XAxisIndex { get; set; }

        public int YAxisIndex { get; set; }

        public Color SeriesColor { get; set; }

        public string SeriesStackFileName { get; set; }

        public Func<object, string> SeriesStackValueFormatter { get; set; }
    }
}
