using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;

namespace SummerFresh.Business
{
    public class PollutantValueDataSource:IFieldConverter
    {
        public string ID
        {
            get;
            set;
        }

        public object Converter(string columnName, object columnValue, IDictionary<string, object> rowData)
        {
            string mark = rowData[columnName + "Mark"].ToString();
            bool status;
            bool.TryParse(rowData["NetStatus"].ToString(), out status);
            if (mark.IsNullOrWhiteSpace())
            {
                return string.Format("<div class='status{0}'>{1}</div>", status ? 1 : 0, columnValue);
            }
            return string.Format("<div class='status{0}'>{1}({2})</div>", status ? 1 : 0, columnValue, mark);
        }
    }
}
