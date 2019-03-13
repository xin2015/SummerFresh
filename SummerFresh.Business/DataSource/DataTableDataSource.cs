using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SummerFresh.Basic;
using SummerFresh.Data;
using System.ComponentModel;
using SummerFresh.Util;
namespace SummerFresh.Business
{
    /// <summary>
    /// 数据表数据源
    /// </summary>
    [DisplayName("数据表数据源")]
    public class DataTableDataSource : ListDataSourceBase, ICascadeDataSource, IKeyValueDataSource
    {
        public DataTableDataSource()
        {
            EnableCache = false;
        }
        public string TableName { get; set; }

        public string DataTextField { get; set; }

        public string DataValueField { get; set; }

        public string ParentField { get; set; }

        public string SearchCondition { get; set; }

        public bool EnableCache { get; set; }


        public IList<SelectListItem> SelectItems()
        {
            if (DataTextField.IsNullOrEmpty() || DataValueField.IsNullOrEmpty())
            {
                throw new CustomException("必须设置DataTextField和DataValueField属性");
            }
            var returnValue = new List<SelectListItem>();
            var dicts = GetAll();
            foreach (var dict in dicts)
            {
                returnValue.Add(new SelectListItem()
                {
                    Text = dict[DataTextField].ToString(),
                    Value = dict[DataValueField].ToString()
                });
            }
            return returnValue;
        }

        public override IList<IDictionary<string, object>> GetList()
        {
            string queryString = GetQueryString();
            if (!SortExpression.IsNullOrEmpty())
            {
                queryString += "ORDER BY {0}".FormatTo(SortExpression);
            }
            return Dao.Get().QueryDictionaries(queryString, Parameter);
        }

        public override IList<IDictionary<string, object>> GetList(int pageIndex, int pageSize, out int recordCount)
        {
            string queryString = GetQueryString();
            if (SortExpression.IsNullOrEmpty())
            {
                SortExpression = DataValueField;
            }
            return Dao.Get().PageQueryDictionariesByPage(queryString, pageIndex, pageSize, SortExpression, out recordCount, Parameter);
        }

        private string GetQueryString()
        {
            return SQLSyntaxGeneratorFactory.GetGenerator(TableName).GetSelectSQL(SearchCondition);
        }

        public IList<TreeNode> SelectRootItems(TreeNode parent = null)
        {
            if (DataTextField.IsNullOrEmpty() || DataValueField.IsNullOrEmpty() || ParentField.IsNullOrEmpty())
            {
                throw new CustomException("必须设置DataTextField、DataValueField和ParentField属性");
            }
            var returnValue = new List<TreeNode>();
            var dicts = GetAll();
            foreach (var dict in dicts)
            {
                returnValue.Add(new TreeNode()
                {
                    name = dict[DataTextField].ToString(),
                    id = dict[DataValueField].ToString(),
                    pId = dict[ParentField] == null ? "" : dict[ParentField].ToString()
                });
            }
            return returnValue;
        }

        public object Converter(string columnName, object columnValue, IDictionary<string, object> rowData)
        {
            var result = SplitConvert(columnName, columnValue, rowData);
            if (result != null)
            {
                return result;
            }
            var dicts = GetAll();
            if (!dicts.IsNullOrEmpty())
            {
                var d = dicts.FirstOrDefault(o => o[DataValueField].ToString().Equals(columnValue.ToString(), StringComparison.CurrentCultureIgnoreCase));
                if (!d.IsNullOrEmpty())
                {
                    return d[DataTextField];
                }
            }
            return null;
        }

        public IList<IDictionary<string, object>> GetAll()
        {
            if (EnableCache)
            {
                string key = NamingCenter.GetCacheKey(CacheType.DATA_TABLE, TableName);
                var dicts = CacheHelper.GetFromCache<IList<IDictionary<string, object>>>(key, () =>
                {
                    return GetList();
                });
                return dicts;
            }
            return GetList();
        }
    }
}
