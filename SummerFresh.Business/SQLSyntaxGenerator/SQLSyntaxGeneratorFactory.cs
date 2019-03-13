using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Data.Mapping;
using SummerFresh.Data;
namespace SummerFresh.Business
{
    public class SQLSyntaxGeneratorFactory
    {

        public static ISQLSyntaxGenerator GetGenerator(string tableName)
        {
            ISQLSyntaxGenerator generator = null;
            var dao = Dao.Get();
            string providerName = dao.Provider.Name.ToLower();
            var provider = DaoFactory.GetMappingProvider(dao.DbProviderName);
            var table = SummerFresh.Data.Mapping.TableMapper.ReadTable(dao, provider, tableName, string.Empty);
            var tableMapping = new TableMapping(dao.Provider, provider, null, table);
            switch (providerName)
            {
                case "sqlserver":
                    generator = new SqlServerSQLSyntaxGenerator(tableMapping);
                    break;
                case "mysql":
                    generator = new MySqlSQLSyntaxGenerator(tableMapping);
                    break;
                case "oracle":
                    generator = new OracleSQLSyntaxGenerator(tableMapping);
                    break;
                default:
                    generator = new SqlServerSQLSyntaxGenerator(tableMapping);
                    break;
            }
            return generator;
        }
    }
}
