using SummerFresh.Business.Entity;
using SummerFresh.Data;
using SummerFresh.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using SummerFresh.Data.Sql;
using SummerFresh.Data.Attributes;
namespace SummerFresh.Business
{
    public class SQLGenerator
    {
        public static string GetSelectOneCommand(Type type, object id)
        {
            if (!type.IsSubclassOf(typeof(CustomEntity)))
            {
                throw new ArgumentOutOfRangeException("type");
            }
            string key = type.FullName + ".Get";
            if (DaoFactory.GetSqlSource().Find(key) != null)
            {
                return key;
            }
            TableMapping mapping = TableMapper.GetTableMapping(Dao.Get(), type);
            string result = mapping.CreateSelectCommand(id).CommandText;
            ISqlStatement statement = SqlParser.Parse(result);
            DaoFactory.GetSqlSource().Add(key, statement);
            return key;
        }
        public static string GetSelectAllCommand(Type type)
        {
            if (!type.IsSubclassOf(typeof(CustomEntity)))
            {
                throw new ArgumentOutOfRangeException("type");
            }
            string key = type.FullName + ".Select";
            if (DaoFactory.GetSqlSource().Find(key) == null)
            {
                TableMapping mapping = TableMapper.GetTableMapping(Dao.Get(), type);
                string result = mapping.CreateSelectAllCommand().CommandText;
                StringBuilder searchSql = new StringBuilder(result);
                searchSql.AppendLine(" WHERE 1 = 1");
                foreach (var p in type.GetProperties())
                {
                    var attr = p.GetCustomAttribute<SearchFieldAttribute>(true);
                    if (attr != null)
                    {
                        if (p.PropertyType == typeof(System.String))
                        {
                            searchSql.AppendLine("{{? AND {0} LIKE '%${0}$%' }}".FormatTo(p.Name));
                        }
                        else if (p.PropertyType == typeof(DateTime))
                        {
                            FormFieldAttribute formAttr = p.GetCustomAttribute<FormFieldAttribute>(true);
                            if (formAttr != null && formAttr.ControlType == ControlType.DateTimeRange)
                            {
                                searchSql.AppendLine("{{? AND {0} >= #sdt{0}# }}".FormatTo(p.Name));
                                searchSql.AppendLine("{{? AND {0} <= #edt{0}# }}".FormatTo(p.Name));
                            }
                            else
                                searchSql.AppendLine("{{? AND {0} = #{0}# }}".FormatTo(p.Name));
                        }
                        else
                        {
                            searchSql.AppendLine("{{? AND {0} = #{0}# }}".FormatTo(p.Name));
                        }
                    }
                }
                ISqlStatement statement = SqlParser.Parse(searchSql.ToString());
                DaoFactory.GetSqlSource().Add(key, statement);
            }
            return key;
        }

        public static string GetDeleteCommand(Type type)
        {
            if (!type.IsSubclassOf(typeof(CustomEntity)))
            {
                throw new ArgumentOutOfRangeException("type");
            }
            string key = type.FullName + ".Delete";
            if (DaoFactory.GetSqlSource().Find(key) == null)
            {
                string commandTemplate = "DELETE FROM {0} WHERE {1} IN ($ids$)";
                TableMapping mapping = TableMapper.GetTableMapping(Dao.Get(), type);
                string tableName = type.Name;
                var attr = type.GetCustomAttribute<TableAttribute>(false);
                if (attr != null)
                {
                    tableName = attr.Name;
                }
                string pk = string.Empty;
                foreach (var p in type.GetProperties())
                {
                    var attr1 = p.GetCustomAttribute<PrimaryKeyAttribute>(true);
                    if (attr1 != null)
                    {
                        pk = p.Name;
                        break;
                    }
                }
                if (pk.IsNullOrEmpty())
                {
                    throw new Exception("实体需要主键标识");
                }
                ISqlStatement statement = SqlParser.Parse(commandTemplate.FormatTo(tableName, pk));
                DaoFactory.GetSqlSource().Add(key, statement);
            }
            return key;
        }
    }
}
