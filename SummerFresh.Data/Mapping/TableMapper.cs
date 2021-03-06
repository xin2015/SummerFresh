﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using SummerFresh.Data.Attributes;
using SummerFresh.Data.Mapping.Provider;
using SummerFresh.Basic.FastReflection;
using SummerFresh.Basic;
using System.Linq;
namespace SummerFresh.Data.Mapping
{
    public class TableMapper
    {
        private static readonly IDictionary<string, Tables> _tables = new Dictionary<string, Tables>();
        private static readonly IDictionary<Type, TableMapping> _mappings = new Dictionary<Type, TableMapping>();
        private static readonly ReaderWriterLockSlim _tablesLock = new ReaderWriterLockSlim();
        private static readonly ReaderWriterLockSlim _mappingsLock = new ReaderWriterLockSlim();
        private static readonly IList<IMappingProvider> _readers;

        static TableMapper()
        {
            _readers = new List<IMappingProvider>
                           {
                               new SqlServerMappingProvider(),
                               new SqlServerCeMappingProvider(),
                               new OracleMappingProvider(),
                               new MySqlMappingProvider()
                           };
        }

        public static IList<IMappingProvider> BuiltinProviders
        {
            get { return _readers; }
        }

        /// <summary>
        /// 获取对应实体类型的数据库表映射关系（有做缓存处理）
        /// </summary>
        public static TableMapping GetTableMapping(Dao dao, Type type)
        {
            TableMapping mapping;

            _mappingsLock.EnterUpgradeableReadLock();
            try
            {
                if (!_mappings.TryGetValue(type, out mapping))
                {
                    _mappingsLock.EnterWriteLock();
                    try
                    {
                        mapping = ReadTableMapping(dao, type);

                        _mappings.Add(type, mapping);
                    }
                    finally
                    {
                        _mappingsLock.ExitWriteLock();
                    }
                }
            }
            finally
            {
                _mappingsLock.ExitUpgradeableReadLock();
            }

            return mapping;
        }

        /// <summary>
        /// 清空对实体与数据表映射关系的缓存
        /// </summary>
        public static void ClearTableMapping()
        {
            _mappingsLock.EnterUpgradeableReadLock();
            try
            {
                _mappingsLock.EnterWriteLock();
                try
                {
                    _mappings.Clear();
                    _tables.Clear();
                }
                finally
                {
                    _mappingsLock.ExitWriteLock();
                }
            }
            finally
            {
                _mappingsLock.ExitUpgradeableReadLock();
            }
        }

        internal static T Read<T>(IDataReader reader, TableMapping mapping)
        {
            if (reader.Read())
            {
                return DoRead<T>(reader, mapping);
            }
            return default(T);
        }

        internal static object Read(Type type,IDataReader reader, TableMapping mapping)
        {
            if (reader.Read())
            {
                return DoRead(type,reader, mapping);
            }
            return null;
        }

        internal static IList<T> ReadAll<T>(IDataReader reader, TableMapping mapping)
        {
            IList<T> list = new List<T>();

            while (reader.Read())
            {
                list.Add(DoRead<T>(reader, mapping));
            }

            return list;
        }

        internal static IList<object> ReadAll(Type type,IDataReader reader, TableMapping mapping)
        {
            IList<object> list = new List<object>();

            while (reader.Read())
            {
                list.Add(DoRead(type,reader, mapping));
            }

            return list;
        }

        internal static object DoRead(Type type,IDataReader reader,TableMapping mapping)
        {
            object instance = Activator.CreateInstance(type);

            foreach (Column column in mapping.Table.Columns)
            {
                FastProperty prop = column.Property;

                if (null != prop)
                {
                    //TODO : 更好的处理可能出现列名不存在的情况
                    bool exists = false;
                    object value = null;
                    try
                    {
                        value = reader[column.Name];
                        exists = true;
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        //找不到列名抛出此错误，忽略掉
                    }

                    if (exists)
                    {
                        prop.SetValue(instance, value.ConventToType(prop.Type));
                    }
                }
            }
            return instance;
        }

        internal static T DoRead<T>(IDataReader reader, TableMapping mapping)
        {
            object instance = Activator.CreateInstance(typeof(T));

            foreach (Column column in mapping.Table.Columns)
            {
                FastProperty prop = column.Property;

                if (null != prop)
                {
                    //TODO : 更好的处理可能出现列名不存在的情况
                    bool exists = false;
                    object value = null;
                    try
                    {
                        value = reader[column.Name];
                        exists = true;
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        //找不到列名抛出此错误，忽略掉
                    }

                    if (exists)
                    {
                        prop.SetValue(instance, value.ConventToType(prop.Type));
                    }
                }
            }
            return (T)instance;
        }

        private static TableMapping ReadTableMapping(Dao dao, Type type)
        {
            IMappingProvider provider = DaoFactory.GetMappingProvider(dao.DbProviderName);
            if (null == provider)
            {
                throw new DaoException(
                    string.Format("not supported db provider '{0}',you should implement IMappingProvider for it", dao.DbProviderName));
            }

            String tableName = GetTableName(type);

            String schemaName = GetSchemaName(type);

            Table table = ReadTable(dao, provider, tableName, schemaName);

            if (null != table)
            {
                return new TableMapping(dao.Provider, provider, type, table);
            }
            else
            {
                throw new DaoException(
                    string.Format("no table '{0}' found in database for type '{1}'", tableName, type.Name));
            }
        }

        public static Table ReadTable(Dao dao, IMappingProvider provider, String tableName, String schemaName)
        {
            _tablesLock.EnterUpgradeableReadLock();
            try
            {
                Tables tables;

                if (!_tables.TryGetValue(dao.ConnectionString, out tables))
                {
                    _tablesLock.EnterWriteLock();
                    try
                    {
                        tables = provider.ReadTables(dao);

                        _tables.Add(dao.ConnectionString, tables);
                    }
                    finally
                    {
                        _tablesLock.ExitWriteLock();
                    }
                }
                return tables.GuessTable(tableName, schemaName);
            }
            finally
            {
                _tablesLock.ExitUpgradeableReadLock();
            }
        }


        public static bool ExistTable(Type type, Dao dao, IMappingProvider provider)
        {
            string tableName = GetTableName(type);
            String schemaName = GetSchemaName(type);
            return ReadTable(dao, provider, tableName, schemaName) != null;
        }

        public static Table ReadTable(Type type)
        {
            var table = new Table();
            table.Name = GetTableName(type);
            table.Schema = GetSchemaName(type);
            FastType fastType = FastType.Get(type);
            foreach (var p in fastType.Setters)
            {
                if (p.Info.PropertyType.IsValueType || p.Type == typeof(string))
                {
                    var column = new Column();
                    column.Name = p.Name;
                    column.IsNullable = true;
                    column.IsAutoIncrement = false;
                    GetDefaultDataType(p.Type, column);
                    var attr = p.Info.GetCustomAttribute<ColumnAttribute>(true);
                    if (attr != null)
                    {
                        if (attr.IsDataField == false) continue;
                        column.Name = attr.Name.IsNullOrEmpty() ? p.Name : attr.Name;
                        column.Type = attr.DataType.IsNullOrEmpty() ? column.Type : attr.DataType;
                        column.Length = attr.Length.IsNullOrEmpty() ? column.Length : attr.Length;
                        column.Showable = attr.Showable;
                        column.Editable = attr.Editable;
                        column.Searchable = attr.Searchable;
                        column.IsAutoIncrement = attr.IsAutoIncrement;
                    }
                    var attr1 = p.Info.GetCustomAttribute<PrimaryKeyAttribute>(true);
                    if (attr1 != null || p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                    {
                        column.IsKey = true;
                        column.IsNullable = false;
                        table.Keys.Add(column);
                    }
                    column.Comment = p.Info.GetDisplayName();
                    if ("text|int|bit|datetime".Split('|').Contains(column.Type, StringComparer.OrdinalIgnoreCase))
                    {
                        column.Length = string.Empty;
                    }
                    table.Columns.Add(column);
                }
            }
            return table;
        }

        private static void GetDefaultDataType(Type type, Column column)
        {
            column.Type = "varchar";
            column.Length = "100";
            if (type == typeof(int))
            {
                column.Type = "int";
            }
            else if (type == typeof(DateTime))
            {
                column.Type = "datetime";
            }
            else if (type == typeof(decimal) || type == typeof(double))
            {
                column.Type = "decimal";
                column.Length = "18,2";
            }
            else if (type == typeof(bool))
            {
                column.Type = "bit";
            }
        }

        private static string GetTableName(Type type)
        {
            TableAttribute tableAttr = type.GetCustomAttribute<TableAttribute>(true);
            if (tableAttr != null)
            {
                return tableAttr.Name;
            }

            TablePrefixAttribute tablePrefixAttr = type.GetCustomAttribute<TablePrefixAttribute>(true);
            if (tablePrefixAttr != null)
            {
                return tablePrefixAttr.Name + type.Name;
            }

            return type.Name;
        }

        private static string GetSchemaName(Type type)
        {
            SchemaAttribute schemaAttr = type.GetCustomAttribute<SchemaAttribute>(true);
            if (schemaAttr != null)
            {
                return schemaAttr.Name;
            }

            return string.Empty;
        }
    }
}