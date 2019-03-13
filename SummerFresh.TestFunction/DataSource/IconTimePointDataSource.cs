using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;

namespace SummerFresh.Business
{
    public class IconTimePointDataSource:IFieldConverter
    {
        public string ID
        {
            get;
            set;
        }

        public object Converter(string columnName, object columnValue, IDictionary<string, object> rowData)
        {
            if (rowData.ContainsKey("NetStatus"))
            {
                bool status;
                bool.TryParse(rowData["NetStatus"].ToString(), out status);
                DateTime timePoint = Convert.ToDateTime(rowData["TimePoint"]);
                return string.Format("<div class='status{0}'>{1}</div>", status ? 1 : 0, timePoint.ToString("MM-dd HH:ss"));
            }
            return "";
        }
    }
}
