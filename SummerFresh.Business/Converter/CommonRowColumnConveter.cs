using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using System.ComponentModel;

namespace SummerFresh.Business
{
    /// <summary>
    /// 通用行转列 
    /// </summary>
    [DisplayName("通用行转列过滤器")]
    public class CommonRowColumnConveter:IDataFilter
    {
        public string ID
        {
            get;
            set;
        }

        public int Rank
        {
            get;
            set;
        }
        public string ColumnNameField { get; set; }

        public string ColumnValueField { get; set; }

        public string GroupByField { get; set; }

        public string DefaultGroupNames { get; set; }

        public IList<IDictionary<string, object>> Conveter(IList<IDictionary<string, object>> data)
        {
            if (data.IsNullOrEmpty())
            {
                return data;
            }
            var result = new List<IDictionary<string, object>>();
            IDictionary<string, object> first = data.First();
            if (!first.ContainsKey(ColumnNameField))
            {
                throw new Exception("未能找到ColumnNameField ：" + ColumnNameField);
            }
            string[] valueFieldArr = ColumnValueField.Split(',');
            ValidateFieldExist("ColumnValueField", valueFieldArr, first);
            string[] groupFieldArr = GroupByField.Split(',');
            ValidateFieldExist("GroupByField", groupFieldArr, first);
            List<string> colNameLst = DefaultGroupNames.IsNullOrWhiteSpace() ?
                                    data.Select(c => c[ColumnNameField].ToString()).Distinct().ToList() :
                                    DefaultGroupNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            Dictionary<string, IDictionary<string, object>> groups = new Dictionary<string, IDictionary<string, object>>();
            string groupKey;
            IDictionary<string, object> newRow;
            foreach (IDictionary<string, object> item in data)
            {
                groupKey = GetGroupKey(groupFieldArr, item);
                if (!groups.ContainsKey(groupKey))
                {
                    groups[groupKey] = new Dictionary<string, object>();
                    //对各个字段ValueField设置默认值
                    if (valueFieldArr.Length == 1)
                    {
                        colNameLst.ForEach(c => groups[groupKey][c] = null);
                    }
                    else
                    {
                        colNameLst.ForEach(c =>
                        {
                            for (int i = 0; i < valueFieldArr.Length; i++)
                            {
                                groups[groupKey][c + "_" + valueFieldArr[i]] = null;
                            }
                        });
                    }
                    //对分组字段设值
                    groupFieldArr.ForEach(c => groups[groupKey][c] = item[c]);

                }
                newRow = groups[groupKey];
                //对单个Value赋值
                if (valueFieldArr.Length == 1)
                {
                    newRow[item[ColumnNameField].ToString()] = item[valueFieldArr[0]];
                }
                //对多个Value赋值
                else
                {
                    valueFieldArr.ForEach(c =>
                    {
                        newRow[item[ColumnNameField] + "_" + c] = item[c];
                    });
                }
                //if key 不存在，新增key value,初始化字段,对group 字段赋值
                //通过key取value 对字段赋值
            }
            result.AddRange(groups.Values);
            return result;
        }

        private string GetGroupKey(string[] keyArr, IDictionary<string, object> dataRow)
        {
            string result = string.Empty;

            keyArr.ForEach(c => result += "{0},".FormatTo(dataRow[c]));

            return result.Trim(',');
        }

        private void ValidateFieldExist(string fieldType, string[] fieldArr, IDictionary<string, object> dataItem)
        {
            for (int i = 0; i < fieldArr.Length; i++)
            {
                if (!dataItem.ContainsKey(fieldArr[i]))
                    throw new Exception(string.Format("未能找到{0} ：{1}", fieldType, fieldArr[i]));
            }
        }
    }
}
