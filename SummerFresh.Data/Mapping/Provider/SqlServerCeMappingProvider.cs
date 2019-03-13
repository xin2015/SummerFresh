using System;

namespace SummerFresh.Data.Mapping.Provider
{
    public class SqlServerCeMappingProvider : GenericMappingProvider
    {
        public override bool Supports(string dbProviderName)
        {
            return dbProviderName.StartsWith("System.Data.SqlServerCe", StringComparison.OrdinalIgnoreCase);
        }

        protected override string GetTablesSql()
        {
            return @"SELECT TABLE_SCHEMA AS [Schema],
                            TABLE_NAME AS [Name] 
                            FROM INFORMATION_SCHEMA.TABLES 
                            WHERE TABLE_TYPE='TABLE'";
        }

        protected override string GetColumnsSql()
        {
            return @"SELECT TABLE_SCHEMA AS [Schema], 
			                TABLE_NAME AS [Table], 
			                COLUMN_NAME AS [Name],
                            AUTOINC_INCREMENT AS [IsAutoIncrement]
		             FROM INFORMATION_SCHEMA.COLUMNS
		             ORDER BY ORDINAL_POSITION ASC";
        }

        protected override string GetKeysSql()
        {
            return @"SELECT KCU.TABLE_SCHEMA AS [Schema],
	                        KCU.TABLE_NAME AS [Table],
	                        KCU.COLUMN_NAME AS [Column]
                     FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU
                          JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC
                          ON (KCU.CONSTRAINT_NAME = TC.CONSTRAINT_NAME AND KCU.TABLE_NAME = TC.TABLE_NAME AND KCU.CONSTRAINT_SCHEMA = TC.CONSTRAINT_SCHEMA)
                     WHERE TC.CONSTRAINT_TYPE='PRIMARY KEY'
                     ORDER BY ORDINAL_POSITION ASC";
        }


        protected override string EscapeIdentifier(string name)
        {
            return string.Format("[{0}]",name);
        }

        public override string CreateTableCommand(Table mapping)
        {
            throw new NotImplementedException();
        }

        public override string GetDropTableCommand(Table table)
        {
            throw new NotImplementedException();
        }
    }
}