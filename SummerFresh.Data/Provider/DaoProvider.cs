using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SummerFresh.Data.Provider
{
    public abstract class DaoProvider : IDaoProvider
    {
        internal const string OrderByClausePatterString = @"[{]\s*[?]\s*order\s+by[\d|\w|\s|$|#|@|:|-|_]*[}]|order\s+by[\d|\w|\s|$|#|@|:|-|_]*";
        private const string NamePatterString=@"\w+";

        public static readonly IDaoProvider SqlServer = new SqlServerProvider();
        public static readonly IDaoProvider Oracle = new OracleProvider();
        public static readonly IDaoProvider MySql = new MySqlProvider();

        private static readonly IList<IDaoProvider> Providers;

        static DaoProvider()
        {
            Providers = new List<IDaoProvider>
                             {
                                 SqlServer,
                                 Oracle,
                                 MySql
                             };
        }

        public static IList<IDaoProvider> BuiltinProviders
        {
            get { return Providers; }
        }

        public static IDaoProvider Default
        {
            get { return SqlServer; }
        }

        private bool _supportsNamedParameter = true;
        private string _namedParameterFormat;
        private string _name;
        private string _nameFormat;

        protected DaoProvider(string name)
        {
            _name = name;
        }

        protected DaoProvider(string name, string namedParameterFormat, string nameFormat)
        {
            _name = name;
            _namedParameterFormat = namedParameterFormat;
            _nameFormat = nameFormat;
        }

        protected DaoProvider(string name, string namedParameterFormat, string nameFormat, bool supportsNamedParameter)
            : this(name, namedParameterFormat, nameFormat)
        {
            _supportsNamedParameter = supportsNamedParameter;
        }

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual bool SupportsDbProvider(string dbProviderName)
        {
            return dbProviderName.Equals(Name, StringComparison.OrdinalIgnoreCase);
        }

        public virtual bool SupportsNamedParameter
        {
            get { return _supportsNamedParameter; }
            protected set { _supportsNamedParameter = value; }
        }

        public virtual bool NamedParameterMustOneByOne
        {
            get { return false; }
        }

        public virtual string NamedParameterFormat
        {
            get { return _namedParameterFormat; }
            protected set { _namedParameterFormat = value; }
        }
        public virtual string NameFormat
        {
            get { return _nameFormat; }
            protected set { _nameFormat = value; }
        }

        public abstract string WrapPageSql(string sql, string orderClause, int startRowIndex, int rowCount, out IDictionary<string, object> pageParam);

        public virtual string WrapCountSql(string sql)
        {
            sql = RemoveOrderByClause(sql);
            return " select count(1) from (\n" + sql + "\n) tt";
        }

        public virtual string EscapeText(string text)
        {
            return null == text ? text : text.Replace("'", "''");
        }

        /// <summary>
        /// 由字段名生成SQL参数名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual string EscapeParam(string name)
        {
            // 为参数统一增加后缀"_"，在Oracle数据库中，命名参数为关键字时，会执行报错
            return name.Replace(":", "_").Replace(".", "__").Replace("@", "_") + "_";
        }

        public virtual string EscapeLikeParamValue(string value)
        {
            return value;
        }

        public virtual string PageParamNameBegin()
        {
            return "Row__Begin";
        }

        public virtual string PageParamNameEnd()
        {
            return "Row__End";
        }

        protected string RemoveOrderByClause(string sql)
        {
            Regex OrderByClausePattern = new Regex(OrderByClausePatterString, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
            sql = OrderByClausePattern.Replace(sql.Trim(), String.Empty);
            return sql;
        }

        protected virtual string FormatOrderClause(string orderClause, string nameFormat)
        {
            var orderSplit = orderClause.Split(',');
            var result=new List<string>();
            bool isAsc, isDesc;
            for (int i = 0; i < orderSplit.Length; i++)
            {
                isAsc = isDesc = false;
                
                var t = orderSplit[i].ToUpper().Trim();
                if (t.Contains("ASC"))
                {
                    isAsc = true;
                }
                else if (t.Contains("DESC"))
                {
                    isDesc = true;
                }
                t = t.Replace("ASC", "").Replace("DESC", "");
                if (Regex.IsMatch(t, NamePatterString)) 
                {
                    t = string.Format(nameFormat, Regex.Match(t, NamePatterString).Value);
                    if (isAsc)
                    {
                        result.Add(string.Format("{0} ASC", t));
                    }
                    else if (isDesc)
                    {
                        result.Add(string.Format("{0} DESC", t));
                    }
                    else
                    {
                        result.Add(t);
                    }
                }
            }
            return string.Join(",",result);
        }
    }
}
