using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    public class NetStatusIconDataSource:IFieldConverter
    {
        public string ID
        {
            get;
            set;
        }

        public object Converter(string columnName, object columnValue, IDictionary<string, object> rowData)
        {
            bool boolValue;
            if (bool.TryParse(columnValue.ToString(), out boolValue))
            {
                return string.Format("<div class='status{0}'></div>", boolValue ? 1 : 0);
            }
            return "";
        }
    }
}
