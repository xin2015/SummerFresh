using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using System.ComponentModel;
namespace SummerFresh.Business
{
    public abstract class KeyValueDataSourceBase:IKeyValueDataSource
    {
        private IList<System.Web.Mvc.SelectListItem> _Items;
        protected IList<System.Web.Mvc.SelectListItem> Items
        {
            get
            {
                return _Items ?? (_Items = SelectItems());
            }
        }

        public abstract IList<System.Web.Mvc.SelectListItem> SelectItems();

        protected object SplitConvert(string columnName, object columnValue, IDictionary<string, object> rowData)
        {
            string colValue = columnValue.ToString();
            if (colValue.Contains(","))
            {
                string[] valueArr = colValue.Split(new char[] { ',' });
                List<object> resultLst = new List<object>(valueArr.Length);
                foreach (var val in valueArr)
                {
                    resultLst.Add(Converter(columnName, val, rowData));
                }
                return string.Join(",", resultLst);
            }
            return null;
        }

        public virtual object Converter(string columnName, object columnValue, IDictionary<string, object> rowData)
        {
            var result = SplitConvert(columnName, columnValue, rowData);
            if (result != null)
            {
                return result;
            }
            if (columnValue != null)
            {
                var item = Items.FirstOrDefault(o => o.Value.Equals(columnValue.ToString(), StringComparison.OrdinalIgnoreCase));
                if(item!=null)
                {
                    return item.Text;
                }
            }
            return columnValue;
        }

        /// <summary>
        /// 数据源ID
        /// </summary>
        [DisplayName("数据源ID")]
        public string ID
        {
            get;
            set;
        }
    }
}
