using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using System.Web.Script.Serialization;
using System.ComponentModel;
namespace SummerFresh.Business
{
    public class CustomDictionary
    {
        private Dictionary<string, object> _dict;
        public CustomDictionary(Dictionary<string, object> dict)
        {
            _dict = dict.IsNullOrEmpty() ? new Dictionary<string, object>():dict;
        }

        public object this[string key]
        {
            get
            {
                if (!_dict.IsNullOrEmpty() && _dict.Keys.Contains(key))
                {
                    return _dict[key];
                }
                return string.Empty;
            }
            set
            {
                _dict[key] = value;
            }
        }

        public Dictionary<string, object>.KeyCollection Keys
        {
            get
            {
                return _dict.Keys;
            }
        }

        public int Count
        {
            get
            {
                return _dict.IsNullOrEmpty() ? 0 : _dict.Count;
            }
        }

        public bool ContainsKey(string key)
        {
            return !_dict.IsNullOrEmpty() && _dict.ContainsKey(key);
        }
    }

    public abstract class ListDataSourceBase : IListDataSource, IChildren
    {
        public ListDataSourceBase()
        {
            ColumnConverters = new List<IColumnConverter>();
            DataFilters = new List<IDataFilter>();
            Controls = new List<IControl>();
        }
        public virtual object Parameter
        {
            get;
            set;
        }

        private CustomDictionary _dictParameter;

        [ScriptIgnore]
        public CustomDictionary DictParameter
        {
            get
            {
                return _dictParameter ?? (_dictParameter = new CustomDictionary(Parameter as Dictionary<string, object>));
            }
        }

        public virtual string SortExpression
        {
            get;
            set;
        }

        [DisplayName("数据源过滤器")]
        [ScriptIgnore]
        public IList<IDataFilter> DataFilters
        {
            get;
            set;
        }

        
        [DisplayName("数据源列转换器")]
        [ScriptIgnore]
        public IList<IColumnConverter> ColumnConverters
        {
            get;
            set;
        }


        public abstract IList<IDictionary<string, object>> GetList();

        public virtual IList<IDictionary<string, object>> GetList(int pageIndex, int pageSize, out int recordCount)
        {
            var list = GetList();
            if(list.IsNullOrEmpty())
            {
                recordCount = 0;
                return list;
            }
            recordCount = list.Count;
            return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

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
            return columnValue;
        }

        public string ID
        {
            get;
            set;

        }

        [FormField(Editable=false)]
        [ScriptIgnore]
        public IList<IControl> Controls
        {
            get;
            set;
        }

        public virtual void AddChildren(string property, object component)
        {
            if (property.Equals("ColumnConverters", StringComparison.OrdinalIgnoreCase))
            {
                ColumnConverters.Add(component as IColumnConverter);
            }
            if (property.Equals("DataFilters", StringComparison.OrdinalIgnoreCase))
            {
                DataFilters.Add(component as IDataFilter);
            }
            Controls.Add(component as IControl);
        }

        public virtual IList<IDictionary<string, object>> FilterByParameter(IList<IDictionary<string, object>> result)
        {
            if (result.IsNullOrEmpty())
            {
                return result;
            }
            var first = result.First();
            string temp = string.Empty;
            foreach (var key in DictParameter.Keys)
            {
                temp = DictParameter[key] as string;
                if (!temp.IsNullOrEmpty()
                    && first.Keys.Contains(key, StringComparer.OrdinalIgnoreCase))
                {
                    if (temp.Contains(','))
                    {
                        var tmpArr = temp.Split(',');
                        result = result.Where(c => Array.IndexOf(tmpArr, c[key].ToString()) > -1).ToList();
                    }
                    else
                    {
                        result = result.Where(o => o[key].ToString().Equals(temp, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                }
            }
            return result;
        }
    }
}
