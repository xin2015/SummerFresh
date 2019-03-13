using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SummerFresh.Data.Mapping
{
    public class ColumnMapping
    {
        public bool HasMapping { get; internal set; }

        public string ColumnName { get; set; }

        public DbType DataType { get; set; }

        public bool IsNull { get; set; }

        public PropertyInfo Property { get; internal set; }
    }
}