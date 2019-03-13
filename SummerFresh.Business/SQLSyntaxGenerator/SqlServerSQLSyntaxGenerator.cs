using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SummerFresh.Data.Mapping;
using SummerFresh.Basic;
namespace SummerFresh.Business
{
    /// <summary>
    /// SQL语句生成器
    /// </summary>
    public class SqlServerSQLSyntaxGenerator : ISQLSyntaxGenerator
    {
        public SqlServerSQLSyntaxGenerator(TableMapping mapping)
        {
            Mapping = mapping;
        }
        public TableMapping Mapping
        {
            get;
            private set;
        }

        public virtual string FieldType
        {
            get
            {
                return "varchar,nvarchar,text,datetime,decimal,money,smallmoney,numeric,bigint,int,tinyint,smallint,bit,uniqueidentifier";
            }
        }

        /// <summary>
        /// 获取新建数据表的脚本
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public virtual string GetCreateTableSQL()
        {
            string createTableSQL = "CREATE TABLE [dbo].{0}({1},CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED ({2})WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
            List<string> keys = new List<string>();
            List<string> columns = new List<string>();
            var fields = Mapping.Table.Columns;
            foreach (var field in fields)
            {
                columns.Add(GetColumnsSQL(field));
            }
            return string.Format(createTableSQL, Mapping.Table.Name, string.Join(",", columns.ToArray()), string.Format("{0} ASC", Mapping.Table.Keys.FirstOrDefault().Name));
        }

        /// <summary>
        /// 获取删除数据表的脚本
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public virtual string GetDropTableSQL()
        {
            return string.Format("IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{0}]') AND type in (N'U')) DROP TABLE [{0}]", Mapping.Table.Name);
        }

        /// <summary>
        /// 获取判断表是否存在的脚本
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public virtual string GetIfExistTableSQL()
        {
            return string.Format("SELECT COUNT(0) FROM sys.objects WHERE object_id = OBJECT_ID(N'[{0}]') AND type in (N'U')", Mapping.Table.Name);
        }

        /// <summary>
        /// 获取判断字段是否存在的脚本
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public virtual string GetIfExistColumnSQL(string fieldName)
        {
            return string.Format("SELECT COUNT(0) FROM sys.columns where object_id=object_id(N'[{0}]') AND name='{1}'", Mapping.Table.Name, fieldName);
        }

        /// <summary>
        /// 获取新增单个字段的脚本
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public virtual string GetAddColumnSQL(Column field)
        {
            string alterTableSQL = "ALTER TABLE {0} ADD {1}";
            return string.Format(alterTableSQL, Mapping.Table.Name, GetColumnsSQL(field));
        }

        public string GetCheckUnionSQL(string fieldName)
        {
            return string.Format("SELECT COUNT(0) FROM [{0}] WHERE [{1}]=#FieldValue# {{? AND [{2}]<>#KeyValue# }}", Mapping.Table.Name, fieldName, Mapping.Table.Keys.First().Name);
        }

        /// <summary>
        /// 获取删除单个字段的脚本
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public virtual string GetDropColumnSQL(string tableName, string fieldName)
        {
            string dropColumnSQLTemplate = "IF EXISTS ( SELECT * FROM SYS.columns WHERE OBJECT_ID=OBJECT_ID( N'{0}' ) AND name='{1}' ) ALTER TABLE {0} DROP COLUMN [{1}]";
            return string.Format(dropColumnSQLTemplate, tableName, fieldName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public virtual string GetColumnsSQL(Column field)
        {
            return string.Format("[{0}] [{1}]{2} {3} {4}",
                field.Name,
                field.Type,
                string.IsNullOrEmpty(field.Length) ? "" : "(" + field.Length + ")",
                field.IsAutoIncrement ? "IDENTITY(1,1)" : "",
                field.IsNullable ? "NULL" : "NOT NULL"
                );
        }

        /// <summary>
        /// 获取主键查询条件
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public virtual string GetWhereCondition()
        {
            List<string> condition = new List<string>();
            foreach (var field in Mapping.Table.Keys)
            {
                condition.Add(string.Format("[{0}]=#{0}#", field.Name));
            }
            return string.Join(" AND ", condition.ToArray());
        }

        public virtual string GetSearchCondition()
        {
            var fields = Mapping.Table.Columns;
            List<string> condition = new List<string>();
            string[] likeTypes = "char|varchar|nvarchar|text".Split('|');
            string[] dateTypes = "datetime|datetime2".Split('|');
            foreach (var field in fields)
            {
                string temp = string.Empty;
                if (likeTypes.Contains(field.Type))
                {
                    temp = string.Format("\n{{? AND [{0}] like '%${0}$%' }}", field.Name);
                }
                else if (dateTypes.Contains(field.Type))
                {
                    temp = string.Format("\n{{? AND CONVERT(varchar(10),[{0}],120) = #{0}# }}", field.Name);
                }
                else
                {
                    temp = string.Format("\n{{? AND [{0}]=#{0}# }}", field.Name);
                }
                condition.Add(temp);
            }
            return string.Join(" ", condition.ToArray());
        }

        /// <summary>
        /// 获取Insert语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="allFields"></param>
        /// <returns></returns>
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
                    fieldString.Add(string.Format("[{0}]", field.Name));
                }
            }
            return string.Format("INSERT INTO {0} \n({1}) \n VALUES \n ({2})", Mapping.Table.Name, string.Join(",", fieldString.ToArray()), string.Join(",", values.ToArray()));
        }

        /// <summary>
        /// 获取Update语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public virtual string GetUpdateSQL()
        {
            if(Mapping.Table.Keys.IsNullOrEmpty())
            {
                return string.Empty;
            }
            string updateSQL = "UPDATE {0} SET {1} WHERE {2}";
            List<string> condition = new List<string>();
            var fields = Mapping.Table.Columns;
            string keyName = Mapping.Table.Keys.FirstOrDefault().Name;
            condition.Add(string.Format("[{0}]=#{0}#\n", keyName));
            foreach (var field in fields)
            {
                if (!field.IsKey)
                {
                    condition.Add(string.Format("{{?? ,[{0}]=#{0}# }}\n", field.Name));
                }
            }
            return string.Format(updateSQL, Mapping.Table.Name, string.Join("", condition.ToArray()), GetWhereCondition());
        }

        /// <summary>
        /// 获取Delete语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public virtual string GetDeleteSQL()
        {
            string deleteSQL = "DELETE FROM [{0}]  WHERE {1}";
            if (Mapping.Table.Keys.Count > 1)
            {
                return string.Format(deleteSQL, Mapping.Table.Name, GetWhereCondition());
            }
            else
            {
                return string.Format(deleteSQL, Mapping.Table.Name, "{0} IN (${0}$)".FormatTo(Mapping.Table.Keys.FirstOrDefault().Name));
            }
        }

        /// <summary>
        /// 获取Select语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public virtual string GetSelectSQL()
        {
            string selectSQL = "SELECT {0} \n FROM [{1}] WITH(NOLOCK) WHERE 1=1 {2}";
            return string.Format(selectSQL,GetFields(), Mapping.Table.Name, GetSearchCondition());
        }

        public virtual string GetSelectOneSQL()
        {
            string selectOneSQL = "SELECT {0} \n FROM {1} WITH(NOLOCK) WHERE {2}";
            return string.Format(selectOneSQL, GetFields(), Mapping.Table.Name, GetWhereCondition());
        }

        private string GetFields()
        {
            List<string> field = new List<string>();
            Mapping.Table.Columns.ForEach(o =>
            {
                field.Add("[{0}]".FormatTo(o.Name));
            });
            return string.Join(",\n", field.ToArray());
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
            return string.Format("[{0}] ASC", sortField);
        }


        public string GetSelectSQL(string searchCondition)
        {
            return "SELECT * FROM [{0}] WHERE 1=1 {1}".FormatTo(Mapping.Table.Name, searchCondition);
        }
    }
}