using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    public interface IKeyValueDataSourceControl
    {
        IKeyValueDataSource DataSource { get; set; }
    }
}
