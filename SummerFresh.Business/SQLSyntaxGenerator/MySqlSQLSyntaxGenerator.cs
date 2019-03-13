using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Data.Mapping;
using SummerFresh.Basic;
namespace SummerFresh.Business
{
    public class MySqlSQLSyntaxGenerator : ISQLSyntaxGenerator
    {
        public MySqlSQLSyntaxGenerator(TableMapping mapping)
        {
            Mapping = mapping;
        }

        
        public virtual string FieldType
        {
            get
            {
                return "char,varchar,tinytext,text,mediumtext,longtext,date,time,datetime,year,decimal,float,double,bigint,int,tinyint,smallint,mediumint,bit";
            }
        }

        public virtual string GetCreateTableSQL()
        {
            string createTableSQL = "CREATE TABLE {0}({1})ENGINE=MyISAM DEFAULT CHARSET=utf8;";
            List<string> keys = new List<string>();
            List<string> columns = new List<string>();
            var fields = Mapping.Table.Columns;
            foreach (var field in fields)
            {
                columns.Add(GetColumnsSQL(field));
            }
            return string.Format(createTableSQL, Mapping.Table.Name, string.Join(",", columns.ToArray()));
        }

        public virtual string GetAddColumnSQL(Column field)
        {
            string alterTableSQL = "ALTER TABLE {0} ADD COLUMN {1}";
            return string.Format(alterTableSQL, Mapping.Table.Name, GetColumnsSQL(field));
        }

        public virtual string GetColumnsSQL(Column field)
        {
            return string.Format("`{0}` {1} {2} {3} {4} {5} {6}",
                field.Name,
                field.Type,
                string.IsNullOrEmpty(field.Length) ? "" : "(" + field.Length + ")",
                field.IsKey ? "PRIMARY KEY" : "",
                field.IsAutoIncrement ? "AUTO_INCREMENT" : "",
                field.IsNullable ? "NULL" : "NOT NULL",
                string.IsNullOrEmpty(field.Comment) ? string.Format("COMMENT {0}", field.Comment) : ""
                );
        }

        public virtual string GetDropColumnSQL(string tableName, string fieldName)
        {
            string dropColumnSQLTemplate = "ALTER TABLE {0} DROP COLUMN {1}";
            return string.Format(dropColumnSQLTemplate, tableName, fieldName);
        }


        public TableMapping Mapping
        {
            get;
            private set;
        }


        public virtual string GetDefaultSortExpression()
        {
            string sortField = "Rank";
            if(Mapping.Table.Columns.Count(o=>o.Name.Equals(sortField))==0)
            {
                sortField = Mapping.Table.Keys.FirstOrDefault().Name;
            }
            return string.Format("`{0}` ASC", sortField);
        }

        public string GetDeleteSQL()
        {
            string deleteSQL = "DELETE FROM {0} WHERE {1}";
            return string.Format(deleteSQL, Mapping.Table.Name, GetWhereCondition());
        }

        public string GetDropTableSQL()
        {
            return string.Format("DROP TABLE {0}", Mapping.Table.Name);
        }

        public string GetIfExistColumnSQL(string fieldName)
        {
            return string.Format("SELECT COUNT(0) FROM information_schema.columns where table_name='{0}' and column_name='{1}'", Mapping.Table.Name, fieldName);
        }

        public string GetIfExistTableSQL()
        {
            return string.Format("SELECT COUNT(0) FROM information_schema.tables where table_name='{0}'", Mapping.Table.Name);
        }

        public string GetInsertSQL()
        {
            List<string> fieldString = new List<string>();
            List<string> values = new List<string>();
            var fields = Mapping.Table.Columns;
            foreach (var field in fields)
            {
                if (!field.IsAutoIncrement)
                {
                    values.Add(string.Format("#{0}#", field.Name));
                    fieldString.Add(string.Format("`{0}`", field.Name));
                }
            }
            return string.Format("INSERT INTO {0} \n({1}) \n VALUES \n ({2})", Mapping.Table.Name, string.Join(",", fieldString.ToArray()), string.Join(",", values.ToArray()));
        }

        public string GetSearchCondition()
        {
            var fields = Mapping.Table.Columns;
            List<string> condition = new List<string>();
            string[] likeTypes = "char|varchar|tinytext|text|mediumtext|longtext".Split('|');
            string[] dateTypes = "datetime|date|time".Split('|');
            foreach (var field in fields)
            {
                string temp = string.Empty;
                if (likeTypes.Contains(field.Type))
                {
                    temp = string.Format("\n{{? AND `{0}` like '%${0}$%' }}", field.Name);
                }
                else if (dateTypes.Contains(field.Type))
                {
                    temp = string.Format("\n{{? AND DATE_FORMAT(`{0}`,'%Y-%m-%d') = #{0}# }}", field.Name);
                }
                else
                {
                    temp = string.Format("\n{{? AND `{0}`=#{0}# }}", field.Name);
                }
                condition.Add(temp);

            }
            return string.Join(" ", condition.ToArray());
        }

        public string GetSelectOneSQL()
        {
            string selectOneSQL = "SELECT {0} FROM {1} WHERE {2}";
            return string.Format(selectOneSQL,GetFields(), Mapping.Table.Name, GetWhereCondition());
        }

        private string GetFields()
        {
            List<string> field = new List<string>();
            Mapping.Table.Columns.ForEach(o =>
            {
                field.Add("`{0}`".FormatTo(o.Name));
            });
            return string.Join(",\n", field.ToArray());
        }

        public string GetSelectSQL()
        {
            
            string selectSQL = "SELECT {0} FROM {1} WHERE 1=1 {2}";
            return string.Format(selectSQL,GetFields(), Mapping.Table.Name, GetSearchCondition());
        }

        public string GetCheckUnionSQL(string fieldName)
        {
            return string.Format("SELECT COUNT(0) FROM {0} WHERE `{1}`=#FieldValue# {{? AND `{2}`<>#KeyValue# }}", Mapping.Table.Name, fieldName, Mapping.Table.Keys.FirstOrDefault().Name);
        }

        public string GetUpdateSQL()
        {
            string updateSQL = "UPDATE {0} SET {1}  WHERE {2}";
            List<string> condition = new List<string>();
            var fields = Mapping.Table.Columns;
            string keyName = Mapping.Table.Keys.FirstOrDefault().Name;
            condition.Add(string.Format("`{0}`=#{0}#\n", keyName));
            foreach (var field in fields)
            {
                condition.Add(string.Format("{{?? ,`{0}`=#{0}# }}\n", field.Name));

            }
            return string.Format(updateSQL, Mapping.Table.Name, string.Join("", condition.ToArray()), GetWhereCondition());
        }

        public string GetWhereCondition()
        {
            List<string> condition = new List<string>();
            foreach (var field in Mapping.Table.Keys)
            {
                condition.Add(string.Format("`{0}`=#{0}#", field.Name));

            }
            return string.Join(" AND ", condition.ToArray());
        }

        public string GetSelectSQL(string searchCondition)
        {
            return "SELECT * FROM `{0}` WHERE 1=1 {1}".FormatTo(Mapping.Table.Name, searchCondition);
        }
    }
}
