using SummerFresh.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    /// <summary>
    /// SqlId数据源
    /// </summary>
    [DisplayName("SqlId数据源")]
    public class SqlIdDataSource : ListDataSourceBase,ICascadeDataSource,IKeyValueDataSource
    {
        private IList<TreeNode> items;

        [FunctionDataSource(typeof(AllSqlIdDataSource))]
        [FormField(ControlType=ControlType.DropDownList)]
        public string SqlId { get; set; }

        private IList<TreeNode> Items 
        {
            get
            {
                return items ?? (items = SelectRootItems());
            }
        }

        public override IList<IDictionary<string, object>> GetList()
        {
            return Dao.Get().QueryDictionaries(SqlId, Parameter);
        }

        public override IList<IDictionary<string, object>> GetList(int pageIndex, int pageSize, out int recordCount)
        {
            return Dao.Get().PageQueryDictionariesByPage(SqlId, pageIndex, pageSize, SortExpression, out recordCount, Parameter);
        }

        public override object Converter(string columnName, object columnValue, IDictionary<string, object> rowData)
        {
            var result = SplitConvert(columnName, columnValue, rowData);
            if (result != null)
            {
                return result;
            }
            if (columnValue == null) return columnValue;
            var i = Items.FirstOrDefault(o => o.id.Equals(columnValue.ToString(), StringComparison.OrdinalIgnoreCase));
            if (i != null)
            {
                return i.name;
            }
            return columnValue;
            //return Dao.Get().QueryDictionaries(SqlId, new { ID = columnValue });
        }

        public IList<TreeNode> SelectRootItems(TreeNode parent = null)
        {
            return Dao.Get().QueryEntities<TreeNode>(SqlId, parent);
        }

        public IList<System.Web.Mvc.SelectListItem> SelectItems()
        {
            return Dao.Get().QueryEntities<System.Web.Mvc.SelectListItem>(SqlId, Parameter);
        }
    }
}
