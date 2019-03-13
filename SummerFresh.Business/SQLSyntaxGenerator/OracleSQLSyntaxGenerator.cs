using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Data.Mapping;
using SummerFresh.Basic;
namespace SummerFresh.Business
{
    public class OracleSQLSyntaxGenerator : ISQLSyntaxGenerator
    {
        public OracleSQLSyntaxGenerator(TableMapping mapping)
        {
            Mapping = mapping;
        }

        /// <summary>
        /// 增加单个字段
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public virtual string GetAddColumnSQL(Column field)
        {
            string alterTableSQL = "ALTER TABLE {0} ADD {1}";
            return string.Format(alterTableSQL, Mapping.Table.Name, GetColumnsSQL(field));
        }

        public virtual string GetCreateTableSQL()
        {
            //string createTableSQL = "CREATE TABLE ANONYMOUS.\"{0}\" ({1},constraint \"PK_{0}\" PRIMARY KEY({2}))";
            string createTableSQL = "CREATE TABLE \"{0}\" ({1},constraint \"PK_{0}\" PRIMARY KEY({2}))";
            List<string> keys = new List<string>();
            List<string> columns = new List<string>();
            var fields = Mapping.Table.Columns;
            foreach (var field in fields)
            {
                columns.Add(GetColumnsSQL(field));
            }
            return string.Format(createTableSQL, Mapping.Table.Name, string.Join(",", columns.ToArray()), string.Format("{0} ASC", Mapping.Table.Keys.FirstOrDefault().Name));
        }

        public virtual string GetDefaultSortExpression()
        {
            string sortField = "Rank";
            if (Mapping.Table.Columns.Count(o => o.Name.Equals(sortField)) == 0)
            {
                if (Mapping.Table.Keys.Count > 0)
                {
                    sortField = Mapping.Table.Keys.FirstOrDefault().Name;
                }
                else
                {
                    sortField = Mapping.Table.Columns.FirstOrDefault().Name;
                }
            }
            return "\"{0}\"".FormatTo(sortField);
        }

        public virtual string GetDeleteSQL()
        {
            string deleteSQL = "DELETE FROM \"{0}\"  WHERE {1}";
            if (Mapping.Table.Keys.Count > 1)
            {
                return string.Format(deleteSQL, Mapping.Table.Name, GetWhereCondition());
            }
            else
            {
                return string.Format(deleteSQL, Mapping.Table.Name, "\"{0}\" IN (${0}$)".FormatTo(Mapping.Table.Keys.FirstOrDefault().Name));
            }
        }

        /// <summary>
        /// 获取删除单个字段的
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public virtual string GetDropColumnSQL(string tableName, string fieldName)
        {
            return "BEGIN  EXECUTE IMMEDIATE 'ALTER TABLE \"{0}\" DROP COLUMN \"{1}\" '; EXCEPTION WHEN OTHERS THEN NULL; END;".FormatTo(tableName, fieldName);
        }

        public virtual string GetDropTableSQL()
        {
            return "BEGIN  EXECUTE IMMEDIATE 'DROP TABLE \"{0}\"'; EXCEPTION WHEN OTHERS THEN NULL; END;".FormatTo(Mapping.Table.Name);
        }

        public virtual string GetIfExistColumnSQL(string fieldName)
        {
            return "SELECT COUNT(0) FROM ALL_TAB_COLUMNS WHERE  TABLE_NAME = '{0}' and COLUMN_NAME = '{1}';".FormatTo(Mapping.Table.Name, fieldName);
        }

        /// <summary>
        /// 获取判断表是否存在的脚本
        /// </summary>
        /// <returns></returns>
        public string GetIfExistTableSQL()
        {
            return " SELECT COUNT(0) FROM user_tables WHERE table_name = '{0}'; ".FormatTo(Mapping.Table.Name);
        }

        public virtual string GetInsertSQL()
        {

            List<string> fieldString = new List<string>();
            List<string> values = new List<string>();
            var fields = Mapping.Table.Columns;
            foreach (var field in fields)
            {
                if (!field.IsAutoIncrement)
                {
                    values.Add(string.Format("#{0}#", field.Name));
                    fieldString.Add(string.Format("\"{0}\"", field.Name));
                }
            }
            return string.Format("INSERT INTO \"{0}\" \n({1}) \n VALUES \n ({2})", Mapping.Table.Name, string.Join(",", fieldString.ToArray()), string.Join(",", values.ToArray()));
        }

        public virtual string GetSearchCondition()
        {
            var fields = Mapping.Table.Columns;
            List<string> condition = new List<string>();
            string[] likeTypes = "char|varchar2|nvarchar2".Split('|');
            string[] dateTypes = "date|timestamp|timestamp with local time zone|timestamp with time zone".Split('|');
            foreach (var field in fields)
            {
                string temp = string.Empty;
                if (likeTypes.Contains(field.Type))
                {
                    temp = string.Format("\n{{? AND \"{0}\" like '%${0}$%' }}", field.Name);
                }
                else if (dateTypes.Contains(field.Type))
                {
                    temp = string.Format("\n{{? AND to_chart('${0}$','yyyy-mm-dd hh24'||':'||'mi'||':'||'ss') = #{0}# }}", field.Name);
                }
                else
                {
                    temp = string.Format("\n{{? AND \"{0}\"=#{0}# }}", field.Name);
                }
                condition.Add(temp);
            }
            return string.Join(" ", condition.ToArray());
        }

        public virtual string GetSelectOneSQL()
        {
            string selectOneSQL = "SELECT {0} \n FROM \"{1}\"  WHERE {2}";
            return string.Format(selectOneSQL, GetFields(), Mapping.Table.Name, GetWhereCondition());
        }

        public virtual string GetSelectSQL()
        {
            string selectSQL = "SELECT {0} \n FROM \"{1}\"  WHERE 1=1 {2}";
            return string.Format(selectSQL, GetFields(), Mapping.Table.Name, GetSearchCondition());
        }

        private string GetFields()
        {
            List<string> field = new List<string>();
            Mapping.Table.Columns.ForEach(o =>
            {
                field.Add("\"{0}\"".FormatTo(o.Name));
            });
            return string.Join(",\n", field.ToArray());
        }

        public virtual string GetUpdateSQL()
        {
            if (Mapping.Table.Keys.IsNullOrEmpty())
            {
                return string.Empty;
            }
            string updateSQL = "UPDATE \"{0}\" SET {1} WHERE {2}";
            List<string> condition = new List<string>();
            var fields = Mapping.Table.Columns;
            string keyName = Mapping.Table.Keys.FirstOrDefault().Name;
            condition.Add(string.Format("\"{0}\"=#{0}#\n", keyName));
            foreach (var field in fields)
            {
                if (!field.IsKey)
                {
                    condition.Add(string.Format("{{?? ,\"{0}\"=#{0}# }}\n", field.Name));
                }
            }
            return string.Format(updateSQL, Mapping.Table.Name, string.Join("", condition.ToArray()), GetWhereCondition());
        }

        public virtual string GetCheckUnionSQL(string fieldName)
        {
            return string.Format("SELECT COUNT(0) FROM \"{0}\" WHERE \"{1}\"=#FieldValue# {{? AND \"{2}\"<>#KeyValue# }}", Mapping.Table.Name, fieldName, Mapping.Table.Keys.First().Name);
        }

        public virtual string GetWhereCondition()
        {
            List<string> condition = new List<string>();
            foreach (var field in Mapping.Table.Keys)
            {
                condition.Add(string.Format("\"{0}\"=#{0}#", field.Name));
            }
            return string.Join(" AND ", condition.ToArray());
        }

        public virtual string GetColumnsSQL(Column field)
        {
            return "\"{0}\" {1}{2} {3}".FormatTo(
                field.Name,
                field.Type,
                field.Length.IsNullOrEmpty() ? "" : "(" + field.Length + ")",
                //field.IsAutoIncrement,
                field.IsNullable ? "NULL" : "NOT NULL");
        }

        public string FieldType
        {
            get
            {
                return "integer,binary_double,binary_float,blob,clob,char,date,interval day to second,interval year to month,long,long raw,nclob,number,nvarchar2,raw,timestamp,timestamp with local time zone,timestamp with time zone,varchar2";
            }
        }

        public TableMapping Mapping
        {
            get;
            private set;
        }


        public string GetSelectSQL(string searchCondition)
        {
            return "SELECT * FROM \"{0}\" WHERE 1=1 {1}".FormatTo(Mapping.Table.Name, searchCondition);
        }
    }
}
