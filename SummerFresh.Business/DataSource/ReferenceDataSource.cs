using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using SummerFresh.Basic;
namespace SummerFresh.Business
{
    [DisplayName("引用数据源")]
    public class ReferenceDataSource:ListDataSourceBase
    {
        public string PageId { get; set; }

        public string DataSourceId { get; set; }

        private IListDataSource _dataSource;
        private IListDataSource DataSource
        {
            get
            {
                if (_dataSource == null)
                {
                    if (PageId.IsNullOrEmpty())
                    {
                        throw new ArgumentNullException("PageId");
                    }
                    if (DataSourceId.IsNullOrEmpty())
                    {
                        throw new ArgumentOutOfRangeException("DataSourceId");
                    }
                    var page = PageBuilder.BuildPage(PageId, HttpContext.Current.Request);
                    if (page == null)
                    {
                        throw new ArgumentOutOfRangeException("PageId");
                    }
                    _dataSource = page.FindControl(DataSourceId) as IListDataSource;
                    if (_dataSource == null)
                    {
                        throw new CustomException("指定的DataSourceId非数据源");
                    }
                    _dataSource.Parameter = Parameter;
                    _dataSource.SortExpression = SortExpression;
                }
                return _dataSource;
            }
        }

        public override IList<IDictionary<string, object>> GetList()
        {
            return DataSource.GetList();
        }

        public override IList<IDictionary<string, object>> GetList(int pageIndex, int pageSize, out int recordCount)
        {
            return DataSource.GetList(pageIndex, pageSize, out recordCount);
        }

        public override object Converter(string columnName, object columnValue, IDictionary<string, object> rowData)
        {
            return DataSource.Converter(columnName, columnValue, rowData);
        }
    }
}
