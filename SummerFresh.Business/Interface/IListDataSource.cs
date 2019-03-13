using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    public interface IListDataSource : IFieldConverter
    {
        object Parameter { get; set; }

        string SortExpression { get; set; }

        IList<IDictionary<string, object>> GetList();

        IList<IDictionary<string, object>> GetList(int pageIndex, int pageSize, out int recordCount);

    }
}
