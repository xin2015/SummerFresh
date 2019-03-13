using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    public interface IDataFilter : ISortableControl
    {
        IList<IDictionary<string, object>> Conveter(IList<IDictionary<string, object>> data);
    }
}
