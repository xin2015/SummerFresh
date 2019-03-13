using System;
using System.Collections.Generic;
using System.Text;

namespace SummerFresh.Data.Provider
{
    public class SqlServerProvider : DaoProvider
    {
        private const string ProviderName = "SqlServer";
        private const string NameParamFormat = "@{0}";
        private const string NameFormat = "[{0}]";
        private const string SqlClientDbProvider = "System.Data.SqlClient";
        private const string SqlCeDbProvider = "System.Data.SqlServerCe";

        public SqlServerProvider()
            : base(ProviderName, NameParamFormat, NameFormat)
        {

        }

        public override bool SupportsDbProvider(string dbProviderName)
        {
            if (SqlClientDbProvider.Equals(dbProviderName,StringComparison.OrdinalIgnoreCase) ||
                dbProviderName.StartsWith(SqlCeDbProvider, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public override string WrapPageSql(string sql, string orderClause, int startRowIndex, int rowCount, out IDictionary<string, object> pageParam)
        {
            sql = RemoveOrderByClause(sql);
            StringBuilder pagingSelect = new StringBuilder(sql.Length + 100);

            if (!String.IsNullOrEmpty(orderClause))
            {
                orderClause = "order by " +FormatOrderClause( orderClause,NameFormat);
            }
            else
            {
                throw new DaoException("Paged query must set orderBy Clause");
            }

            pagingSelect.Append("select * from (select row_number() over(").Append(orderClause).Append(
                    ") as rownum,* from (");
            pagingSelect.Append(sql);
            pagingSelect.Append("\n ) as _table1) as _table2 where (rownum <=#").Append(PageParamNameEnd()).Append("# and rownum>= #")
                .Append(PageParamNameBegin()).Append("# ) ");

            pageParam = new Dictionary<string, object>(2)
                            {
                                {PageParamNameBegin(), startRowIndex},
                                {PageParamNameEnd(), startRowIndex + rowCount - 1}
                            };

            return pagingSelect.ToString();
        }

        public override string EscapeLikeParamValue(string value)
        {
            return value.Replace("[", "[[]").Replace("?", @"[?]").Replace("_", @"[_]").Replace("%", @"[%]");
        }
    }
}