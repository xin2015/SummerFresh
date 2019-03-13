using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using SummerFresh.Data.Mapping;
using SummerFresh.Data.Sql;
using SummerFresh.Data.Util;
using SummerFresh.Util;
using SummerFresh.Basic;
using SummerFresh.Basic.FastReflection;
using System.Configuration;
namespace SummerFresh.Data
{
    public abstract class GenericDao : Dao
    {
        private static readonly ILog log = LogManager.GetCurrentClassLogger();

        #region 构造函数与属性

        protected string _name;
        protected string _connectionString;
        protected string _dbProviderName;
        protected IDaoProvider _provider;

        protected GenericDao(string name)
        {
            this._name = name;
            this._connectionString = ConfigurationManager.ConnectionStrings[name].ConnectionString;
            this._dbProviderName = ConfigurationManager.ConnectionStrings[name].ProviderName;
            this._provider = DaoFactory.GetDaoProviderOfDbProvider(_dbProviderName);

            if (null == _provider)
            {
                throw new DaoException(string.Format("Dao Provider '{0}' Not Found", ConfigurationManager.ConnectionStrings[name].ProviderName));
            }
        }

        protected GenericDao(string name, string providerName)
        {
            this._name = name;
            this._dbProviderName = providerName;
            this._provider = DaoFactory.GetDaoProviderOfDbProvider(providerName);

            if (null == _provider)
            {
                throw new DaoException(string.Format("Dao Provider '{0}' Not Found", ConfigurationManager.ConnectionStrings[name].ProviderName));
            }
        }

        public override IDaoProvider Provider
        {
            get { return _provider; }
        }

        public override string ConnectionString
        {
            get { return _connectionString; }
        }

        public override string DbProviderName
        {
            get { return _dbProviderName; }
        }
        #endregion

        #region 基础实现
        public override int ExecuteNonQuery(string sql, object parameters = null)
        {
            return ExecuteNonQuery(sql, null, parameters);
        }

        public override IDataReader QueryReader(string sql, object parameters = null)
        {
            return QueryReader(sql, null, parameters);
        }

        public override DataSet QueryDataSet(string sql, object parameters = null)
        {
            return QueryDataSet(sql, null, parameters);
        }

        public virtual T ExecuteReader<T>(string sql, object parameters, Func<IDataReader, T> func)
        {
            return ExecuteReader<T>(sql, null, parameters, func);
        }
        #endregion

        #region 内部实现
        internal int ExecuteNonQuery(string sql, string connectionName, object parameters = null)
        {
            return Execute<int>(sql, parameters, connectionName, ExecuteNonQuery);
        }

        internal IDataReader QueryReader(string sql, string connectionName, object parameters = null)
        {
            return Execute<IDataReader>(sql, parameters, connectionName, ExecuteReader);
        }

        internal DataSet QueryDataSet(string sql, string connectionName, object parameters = null)
        {
            return Execute<DataSet>(sql, parameters, connectionName, ExecuteDataSet);
        }

        internal IList<IDictionary<string, object>> QueryDictionaries(string sql, string connectionName, object parameters = null)
        {
            return ExecuteReader<IList<IDictionary<string, object>>>(sql, connectionName, parameters,
                                                                     (reader) => reader.ReadDictionaries());
        }

        internal T ExecuteReader<T>(string sql, string connectionName, object parameters, Func<IDataReader, T> func)
        {
            using (IDataReader reader = QueryReader(sql, connectionName, parameters))
            {
                return func(reader);
            }
        }
        #endregion

        #region 扩展实现
        public override IList<IDictionary<string, object>> QueryDictionaries(string sql, object parameters = null)
        {
            return ExecuteReader<IList<IDictionary<string, object>>>(sql, parameters,
                                                                     (reader) => reader.ReadDictionaries());
        }

        public override IList<T> QueryEntities<T>(string sql, object parameters = null)
        {
            return ExecuteReader<IList<T>>(sql, parameters, (reader) =>
                    {
                        //bool isKey = IsStatement(sql);
                        return TypeMapper.ReadList<T>(reader);
                    });
        }

        public override IList<T> QueryEntities<T>(Type type, string sql, object parameters = null)
        {
            return ExecuteReader<IList<T>>(sql, parameters, (reader) =>
                    {
                        //bool isKey = IsStatement(sql);
                        return TypeMapper.ReadList<T>(reader,type);
                    });
        }

        public override IDictionary<string, object> QueryDictionary(string sql, object parameters = null)
        {
            return ExecuteReader<IDictionary<string, object>>(sql, parameters,
                                                              (reader) =>
                                                              {
                                                                  var dict = reader.ReadDictionary();
                                                                  if (reader.Read())
                                                                  {
                                                                      throw new DataReturnMultipleRowsException("reader has multiple rows data");
                                                                  }
                                                                  return dict;
                                                              });
        }

        public override T QueryEntity<T>(string sql, object parameters = null)
        {
            return QueryEntity<T>(typeof(T), sql, parameters);
        }

        public override T QueryEntity<T>(Type type, string sql, object parameters = null)
        {
            return ExecuteReader<T>(sql, parameters, (reader) =>
            {
                //bool isKey = IsStatement(sql);
                //T entity = reader.Read<T>(type, isKey ? sql : null);
                T entity = TypeMapper.Read<T>(reader, type);

                if (reader.Read())
                {
                    throw new DataReturnMultipleRowsException("reader has multiple rows data");
                }

                return entity;
            });
        }

        public override bool Exists(string sql, object parameters = null)
        {
            return ExecuteReader<bool>(sql, parameters, (reader) => reader.Read());
        }

        public override T QueryScalar<T>(string sql, object parameters = null)
        {
            return QueryScalar<T>(sql, null, parameters);
        }

        public override IList<T> QueryScalarList<T>(string sql, object parameters = null)
        {
            return ExecuteReader<IList<T>>(sql, parameters, (reader) =>
            {
                IList<T> list = new List<T>();
                while (reader.Read())
                {
                    list.Add(reader[0].ConverTo<T>());
                }
                return list;
            });
        }
        #endregion

        #region ORMapping

        public override void CreateTable<T>()
        {
            CreateTable(typeof(T));
        }

        public override void CreateTable(Type type)
        {
            var table = TableMapper.ReadTable(type);
            IMappingProvider provider = DaoFactory.GetMappingProvider(DbProviderName);
            if (TableMapper.ExistTable(type, this, provider))
            {
                ExecuteNonQuery(provider.GetDropTableCommand(table));
            }
            ExecuteNonQuery(provider.CreateTableCommand(table));
        }

        public override T Select<T>(object id)
        {
            TableMapping mapping = TableMapper.GetTableMapping(this, typeof(T));

            using (IDataReader reader = QueryReader(mapping.CreateSelectCommand(id), null))
            {
                return TableMapper.Read<T>(reader,mapping);
            }
        }

        public override object Select(Type type, object id)
        {
            TableMapping mapping = TableMapper.GetTableMapping(this,type);

            using (IDataReader reader = QueryReader(mapping.CreateSelectCommand(id), null))
            {
                return TableMapper.Read(type,reader, mapping);
            }
        }

        public override IList<T> SelectAll<T>()
        {
            TableMapping mapping = TableMapper.GetTableMapping(this, typeof(T));

            using (IDataReader reader = QueryReader(mapping.CreateSelectAllCommand(), null))
            {
                return TableMapper.ReadAll<T>(reader, mapping);
            }
        }

        public override IList<object> SelectAll(Type type)
        {
            TableMapping mapping = TableMapper.GetTableMapping(this, type);

            using (IDataReader reader = QueryReader(mapping.CreateSelectAllCommand(), null))
            {
                return TableMapper.ReadAll(type,reader, mapping);
            }
        }

        public override int BatchUpdate(object entity, object parameter)
        {
            TableMapping mapping = TableMapper.GetTableMapping(this, entity.GetType());

            return ExecuteNonQuery(mapping.CreateBatchUpdateCommand(entity, parameter), null);
        }

        public override int BatchUpdate<T>(object entity, object parameter)
        {
            TableMapping mapping = TableMapper.GetTableMapping(this, typeof(T));

            return ExecuteNonQuery(mapping.CreateBatchUpdateCommand(entity, parameter), null);
        }

        public override int BatchDelete(Type type, object parameter)
        {
            TableMapping mapping = TableMapper.GetTableMapping(this, type);
            return ExecuteNonQuery(mapping.CreateBatchDeleteCommand(parameter), null);
        }

        public override int BatchDelete<T>(object parameter)
        {
            return BatchDelete(typeof(T), parameter);
        }

        public override bool Exists(object entity)
        {
            TableMapping mapping = TableMapper.GetTableMapping(this, entity.GetType());
            return QueryScalar<int>(mapping.CreateExistsCommand(entity), null) > 0;
        }

        public override bool Exists<T>(object id)
        {
            TableMapping mapping = TableMapper.GetTableMapping(this, typeof(T));
            return QueryScalar<int>(mapping.CreateExistsCommand(id), null) > 0;
        }

        public override int Insert(object entity)
        {
            TableMapping mapping = TableMapper.GetTableMapping(this, entity.GetType());

            return ExecuteNonQuery(mapping.CreateInsertCommand(entity), null);
        }

        public override int Insert<T>(object entity)
        {
            TableMapping mapping = TableMapper.GetTableMapping(this, typeof (T));
            return ExecuteNonQuery(mapping.CreateInsertCommand(entity), null);
        }

        public override int InsertFields<T>(IDictionary<string, object> fields)
        {
            TableMapping mapping = TableMapper.GetTableMapping(this, typeof (T));
            return ExecuteNonQuery(mapping.CreateInsertCommand(fields), null);
        }

        public override int Update(object entity)
        {
            TableMapping mapping = TableMapper.GetTableMapping(this, entity.GetType());

            return ExecuteNonQuery(mapping.CreateUpdateCommand(entity), null);
        }

        public override int Update<T>(object entity)
        {
            TableMapping mapping = TableMapper.GetTableMapping(this, typeof(T));
            return ExecuteNonQuery(mapping.CreateUpdateCommand(entity), null);
        }

        public override int UpdateFields<T>(IDictionary<string, object> fields)
        {
            TableMapping mapping = TableMapper.GetTableMapping(this, typeof(T));
            return ExecuteNonQuery(mapping.CreateUpdateCommand(fields), null);
        }

        public override int UpdateFields<T>(Object entity, params String[] inclusiveFields)
        {
            TableMapping mapping = TableMapper.GetTableMapping(this, typeof(T));
            return ExecuteNonQuery(mapping.CreateUpdateCommand(entity, inclusiveFields, true), null);
        }

        public override int UpdateFields(object entity, params string[] inclusiveFields)
        {
            TableMapping mapping = TableMapper.GetTableMapping(this, entity.GetType());
            return ExecuteNonQuery(mapping.CreateUpdateCommand(entity, inclusiveFields, true), null);
        }

        public override int UpdateFieldsExcluded(object entity, params string[] exclusiveFields)
        {
            TableMapping mapping = TableMapper.GetTableMapping(this, entity.GetType());
            return ExecuteNonQuery(mapping.CreateUpdateCommand(entity, exclusiveFields, false), null);
        }

        public override int UpdateFieldsExcluded<T>(object entity, params string[] exclusiveFields)
        {
            TableMapping mapping = TableMapper.GetTableMapping(this, typeof(T));
            return ExecuteNonQuery(mapping.CreateUpdateCommand(entity, exclusiveFields, false), null);
        }

        public override int UpdateFieldsNotNull(object entity)
        {
            IList<string> includedProperties = new List<string>();
            FastProperty[] properties = SummerFresh.Data.Mapping.TypeReflection.Get(entity.GetType()).Setters;
            foreach (FastProperty property in properties)
            {
                if (null != property.GetValue(entity))
                {
                    includedProperties.Add(property.Name);
                }
            }
            return this.UpdateFields(entity, includedProperties.ToArray());
        }

        public override int Delete<T>(object id)
        {
            TableMapping mapping = TableMapper.GetTableMapping(this, typeof(T));
            return ExecuteNonQuery(mapping.CreateDeleteCommand(id), null);
        }

        public override int Delete(object entity)
        {
            TableMapping mapping = TableMapper.GetTableMapping(this, entity.GetType());

            return ExecuteNonQuery(mapping.CreateDeleteCommand(entity), null);
        }
        #endregion

        #region 分页查询

        public override DataSet
            PageQueryDataSet(string sql, int startRowIndex, int maximumRows, String sortExpression, object parameters = null)
        {
            ISqlStatement statement;

            string text = FindText(sql, out statement);

            string connectionName = null == statement ? null : statement.Connection;

            if (maximumRows <= 0)
            {
                string newText = _provider.WrapCountSql(text);

                int totalRowCount = QueryScalar<int>(newText, connectionName, parameters);

                maximumRows = totalRowCount;
            }

            object outParams;
            string sqlText = GetPageQuery(text, startRowIndex, maximumRows, sortExpression, parameters, out outParams);
            return QueryDataSet(sqlText, connectionName, outParams);
        }

        public override DataSet
            PageQueryDataSet(string sql, int startRowIndex, int maximumRows, String sortExpression, out int totalRowCount, object parameters = null)
        {
            ISqlStatement statement;

            string text = FindText(sql, out statement);

            string newText = _provider.WrapCountSql(text);

            string connectionName = null == statement ? null : statement.Connection;

            totalRowCount = QueryScalar<int>(newText, connectionName, parameters);

            return PageQueryDataSet(sql, startRowIndex, maximumRows <= 0 ? totalRowCount : maximumRows, sortExpression, parameters);
        }

        public override IList<IDictionary<string, object>>
            PageQueryDictionaries(string sql, int startRowIndex, int maximumRows, String sortExpression, object parameters = null)
        {
            ISqlStatement statement;

            string text = FindText(sql, out statement);

            string connectionName = null == statement ? null : statement.Connection;

            if (maximumRows <= 0)
            {
                string newText = _provider.WrapCountSql(text);

                int totalRowCount = QueryScalar<int>(newText, connectionName, parameters);

                maximumRows = totalRowCount;
            }

            object outParams;
            string sqlText = GetPageQuery(text, startRowIndex, maximumRows, sortExpression, parameters, out outParams);
            return QueryDictionaries(sqlText, connectionName, outParams);
        }

        public override IList<IDictionary<string, object>>
            PageQueryDictionaries(string sql, int startRowIndex, int maximumRows, String sortExpression, out int totalRowCount, object parameters = null)
        {
            ISqlStatement statement;

            string text = FindText(sql, out statement);

            string newText = _provider.WrapCountSql(text);

            string connectionName = null == statement ? null : statement.Connection;

            totalRowCount = QueryScalar<int>(newText, connectionName, parameters);

            return PageQueryDictionaries(sql, startRowIndex, maximumRows <= 0 ? totalRowCount : maximumRows, sortExpression, parameters);
        }

        public override IList<T>
            PageQueryEntities<T>(string sql, int startRowIndex, int maximumRows, String sortExpression, object parameters = null)
        {
            ISqlStatement statement;

            string text = FindText(sql, out statement);

            string connectionName = null == statement ? null : statement.Connection;

            if (maximumRows <= 0)
            {
                string newText = _provider.WrapCountSql(text);

                int totalRowCount = QueryScalar<int>(newText, connectionName, parameters);

                maximumRows = totalRowCount;
            }

            object outParams;
            string sqlText = GetPageQuery(text, startRowIndex, maximumRows, sortExpression, parameters, out outParams);
            return ExecuteReader<IList<T>>(sqlText, connectionName, outParams, TypeMapper.ReadList<T>);
        }

        public override IList<T>
            PageQueryEntities<T>(string sql, int startRowIndex, int maximumRows, String sortExpression, out int totalRowCount, object parameters = null)
        {
            ISqlStatement statement;

            string text = FindText(sql, out statement);

            string newText = _provider.WrapCountSql(text);

            string connectionName = null == statement ? null : statement.Connection;

            totalRowCount = QueryScalar<int>(newText, connectionName, parameters);

            return PageQueryEntities<T>(sql, startRowIndex, maximumRows <= 0 ? totalRowCount : maximumRows, sortExpression, parameters);
        }

        protected virtual string GetPageQuery(string sql, int startRowIndex, int maximumRows, String sortExpression, object inParmas, out object outParams)
        {
            ParametersWrapper param = new ParametersWrapper(SqlParameters.GetParameters(inParmas));

            IDictionary<string, object> pageParam = null;
            string newText = _provider.WrapPageSql(sql, sortExpression, startRowIndex, maximumRows, out pageParam);

            foreach (var key in pageParam.Keys)
            {
                param.Add(key, pageParam[key]);
            }            

            outParams = param;
            return newText;
        }

        public override DataSet PageQueryDataSetByPage(string sql, int page, int size, string sortExpression, object parameters = null)
        {
            return PageQueryDataSet(sql, (page - 1)*size + 1, size, sortExpression, parameters);
        }

        public override DataSet PageQueryDataSetByPage(string sql, int page, int size, string sortExpression, out int totalRowCount, object parameters = null)
        {
            return PageQueryDataSet(sql, (page - 1) * size + 1, size, sortExpression, out totalRowCount, parameters);
        }

        public override IList<IDictionary<string, object>> PageQueryDictionariesByPage(string sql, int page, int size, string sortExpression, object parameters = null)
        {
            return PageQueryDictionaries(sql, (page - 1)*size + 1, size, sortExpression, parameters);
        }

        public override IList<IDictionary<string, object>> PageQueryDictionariesByPage(string sql, int page, int size, string sortExpression, out int totalRowCount, object parameters = null)
        {
            return PageQueryDictionaries(sql, (page - 1)*size + 1, size, sortExpression, out totalRowCount, parameters);
        }

        public override IList<T> PageQueryEntitiesByPage<T>(string sql, int page, int size, string sortExpression, object parameters = null)
        {
            return PageQueryEntities<T>(sql, (page - 1)*size + 1, size, sortExpression, parameters);
        }

        public override IList<T> PageQueryEntitiesByPage<T>(string sql, int page, int size, string sortExpression, out int totalRowCount, object parameters = null)
        {
            return PageQueryEntities<T>(sql, (page - 1)*size + 1, size, sortExpression, out totalRowCount, parameters);
        }

        #endregion

        #region 内部接口实现
        internal override int ExecuteNonQuery(ISqlCommand command, String connectionName)
        {
            return ExecuteNonQuery(CreateDbCommand(command, connectionName), connectionName);
        }

        internal override DataSet QueryDataSet(ISqlCommand command, String connectionName)
        {
            return ExecuteDataSet(CreateDbCommand(command, connectionName), connectionName);
        }

        internal override IDataReader QueryReader(ISqlCommand command, String connectionName)
        {
            return ExecuteReader(CreateDbCommand(command, connectionName), connectionName);
        }

        internal override T QueryScalar<T>(ISqlCommand command, String connectionName)
        {
            return ExecuteScalar(CreateDbCommand(command, connectionName), connectionName).ConverTo<T>();
        }

        internal T QueryScalar<T>(string sql, string connectionName, object parameters = null)
        {
            return Execute<object>(sql, parameters, connectionName, ExecuteScalar).ConverTo<T>();
        }
        #endregion

        #region 内部实现逻辑
        protected virtual T Execute<T>(string sql, object parameters, string connectionName, Func<DbCommand, String, T> func)
        {
            //检查参数是否基元类型或值类型，int 和Int32之类
            CheckArguments(sql, parameters);

            ISqlStatement statement;

            //sql变量是否是sqlid，若是直接提取sqlstatement,若否则需要sqlparser提取转换
            bool isKey = FindStatement(sql, out statement);
            log.Debug("\n------------Begin-----------\n");
            if (isKey)
            {
                log.Debug("Dao -> Found Statement For Key : \n'{0}'", sql);
            }
            else
            {
                log.Debug("Dao -> Parse Statement For Sql : \n{0}", sql);
                statement = SqlParser.Parse(sql);
                // add to cache
                DaoFactory.GetSqlSource().Add(sql, statement);
            }

            var sw = Stopwatch.StartNew();
            try
            {
                connectionName = String.IsNullOrEmpty(connectionName) ? statement.Connection: connectionName;
                return func(CreateDbCommand(statement, parameters, connectionName), connectionName);
            }
            finally
            {
                sw.Stop();
                if (isKey)
                {
                    log.Info("Dao -> Execute Command '{0}' Used {1}ms", sql, sw.ElapsedMilliseconds);
                }
                else
                {
                    log.Info("Dao -> Execute Sql Used {0}ms", sw.ElapsedMilliseconds);
                }
                log.Debug("\n------------End-----------\n");
            }
        }

        /// <summary>
        /// 检查sql参数是否为空，以及sql中的参数载体类型是否是简单类型
        /// </summary>
        protected virtual void CheckArguments(string sql, object parameters)
        {
            if (string.IsNullOrEmpty(sql))
            {
                throw new ArgumentNullException("sql");
            }

            if (null != parameters)
            {
                Type type = parameters.GetType();

                if (type.IsPrimitive || type.IsValueType)
                {
                    throw new InvalidOperationException(
                        string.Format("Not Supported Type '{0}' of Argument 'object paramters'", type.FullName));
                }
            }
        }

        /// <summary>
        /// 检查sqlId是否存在，如果存在则以ISqlStatement返回其sql
        /// </summary>
        protected virtual bool FindStatement(string sql, out ISqlStatement statement)
        {
            if (!DaoFactory.GetSqlSource().IsValidKey(sql))
            {
                statement = null;
                return false;
            }
            return null != (statement = DaoFactory.GetSqlSource().Find(sql, _provider.Name));
        }

        protected virtual bool IsStatement(string sql)
        {
            return null != (DaoFactory.GetSqlSource().Find(sql, _provider.Name));
        }

        protected virtual string FindText(string sql, out ISqlStatement statement)
        {
            return FindStatement(sql, out statement) ? statement.Text : sql;
        }

        protected virtual DbCommand CreateDbCommand(ISqlStatement statement, object parameters, string connectionName)
        {
            return CreateDbCommand(statement.CreateCommand(Provider, parameters), connectionName);
        }

        protected virtual DbCommand CreateDbCommand(ISqlCommand command, String connectionName)
        {
            log.Debug("\n\nSql -> \n{0}", command.CommandText);

            DbCommand dbCommand = CreateDbCommand(command.CommandText, connectionName);
            foreach (KeyValuePair<string, object> parameter in command.Parameters)
            {
                if (_provider.SupportsNamedParameter &&
                    dbCommand.Parameters.Contains(parameter.Key))
                {
                    if (!_provider.NamedParameterMustOneByOne)
                    {
                        log.Trace("Duplicate Name '{0}' Found,Ignore it", parameter.Key);
                        continue;
                    }
                }

                var dbParameter = dbCommand.CreateParameter();
                dbParameter.ParameterName = parameter.Key;

                object value = parameter.Value;
                dbParameter.Value = value ?? DBNull.Value;

                log.Debug("  #Parameter : '{0}' = '{1}'", parameter.Key, value);

                dbCommand.Parameters.Add(dbParameter);
            }
            return dbCommand;
        }
        #endregion

        #region 虚拟方法
        protected abstract DbCommand CreateDbCommand(string sql, String connectionName);

        protected abstract int ExecuteNonQuery(DbCommand command, String connectionName);

        protected abstract IDataReader ExecuteReader(DbCommand command, String connectionName);

        protected abstract DataSet ExecuteDataSet(DbCommand command, String connectionName);

        protected abstract object ExecuteScalar(DbCommand command, String connectionName);
        #endregion
    }
}