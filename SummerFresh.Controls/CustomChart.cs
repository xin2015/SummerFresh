using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace SummerFresh.Controls
{
    public static class CustomChartExtenstion
    {
        public static MvcHtmlString CustomChart(this HtmlHelper html,string chartType)
        {
            
            return MvcHtmlString.Create("");
        }

        public static MvcHtmlString CustomChart(this HtmlHelper html,ChartConfig config)
        {
            return MvcHtmlString.Create("");
        }

        public class ChartConfig
        {
            public string ChartTitle { get; set; }

            public Color BackgroundColor { get; set; }

            public ChartTheme ChartTheme { get; set; }



            public IList<ChartDataSource> DataSource { get; set; }
            
        }

        public class ChartDataSource
        {

            public ChartType ChartType { get; set; }
            public IList<IDictionary<string, object>> Data { get; set; }

            public IList<ChartField> ChartField { get; set; }
        }

        public class ChartField
        {
            public string FieldName { get; set; }

            public string DisplayName { get; set; }
        }

        public enum ChartTheme
        {
            DarkBlue,
            DarkGreen,
            DarkUnica,
            Gray,
            GridLight,
            Grid,
            SandSignika,
            Skies
        }

        public enum ChartType
        {
            /// <summary>
            /// 
            /// </summary>
            Line,
            Spline,
            Area,
            AreaSpline,
            Column,
            Bar,
            Pie,
            Scatter
        }
    }
}
