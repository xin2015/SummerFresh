using System;
using System.Collections.Generic;
using System.Linq;
using SummerFresh.Basic;
namespace SummerFresh.Data.Mapping.Provider
{
    public class OracleMappingProvider : GenericMappingProvider
    {
        public override bool Supports(string dbProviderName)
        {
            return "System.Data.OracleClient".Equals(dbProviderName, StringComparison.OrdinalIgnoreCase) || "Oracle.DataAccess.Client".Equals(dbProviderName, StringComparison.OrdinalIgnoreCase);
        }

        protected override string GetTablesSql()
        {
            return @"Select TABLE_NAME AS ""Name"" From USER_TABLES
                     Union
                     Select VIEW_NAME AS ""Name"" from USER_VIEWS";
        }

        protected override string GetColumnsSql()
        {
            return @"select table_name AS ""Table"",
                            column_name AS ""Name""
                     from USER_TAB_COLS
                     order by column_id";
        }

        protected override string GetKeysSql()
        {
            return @"select uc.TABLE_NAME AS ""Table"", 
                            column_name AS ""Column""
                      from USER_CONSTRAINTS uc
                     inner join USER_CONS_COLUMNS ucc
                        on (uc.constraint_name = ucc.constraint_name and uc.table_name = ucc.table_name and uc.owner = ucc.owner)
                     where uc.constraint_type = 'P'
                       and ucc.position = 1";
        }

        protected override string EscapeIdentifier(string name)
        {
            return string.Format("\"{0}\"", name);
        }

        public override string CreateTableCommand(Table mapping)
        {
            string createTableSQL = "CREATE TABLE {0}({1})";
            var keys = new List<string>();
            mapping.Keys.Select(o => o.Name).ForEach(o =>
            {
                keys.Add(EscapeIdentifier("{0}"));
            });
            var columns = new List<string>();
            var fields = mapping.Columns;
            foreach (var field in fields)
            {
                columns.Add(GetColumnsSQL(field));
            }
            string keyString = string.Join(",", keys.ToArray());
            return string.Format(createTableSQL, mapping.Name, string.Join(",", columns.ToArray()));
        }

        private string GetColumnsSQL(Column field)
        {
            return string.Format("{0} {1}{2} {3} {4}",
                EscapeIdentifier(field.Name),
                field.Type,
                string.IsNullOrEmpty(field.Length) ? "" : "(" + field.Length + ")",
                //field.IsAutoIncrement ? "IDENTITY(1,1)" : "",
                field.IsNullable ? "NULL" : "NOT NULL",
                field.IsKey ? "PRIMARY KEY" : ""
                );
        }

        public override string GetDropTableCommand(Table table)
        {
            return "DROP TABLE {0}".FormatTo(table.Name);
        }
    }
}