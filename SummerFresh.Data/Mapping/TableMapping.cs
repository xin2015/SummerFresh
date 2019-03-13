using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SummerFresh.Data.Attributes;
using SummerFresh.Basic;
using SummerFresh.Basic.FastReflection;

namespace SummerFresh.Data.Mapping
{
    public class TableMapping
    {
        private readonly IList<Column> _insertColumns = new List<Column>();
        private readonly IList<Column> _updateColumns = new List<Column>();

        public TableMapping(IDaoProvider daoProvider, IMappingProvider mappingProvider, Type type, Table table)
        {
            this.DaoProvider = daoProvider;
            this.MappingProvider = mappingProvider;
            this.Type = type;
            this.Table = table;

            Initialize();
        }

        public IDaoProvider DaoProvider
        {
            get;
            internal set;
        }

        public IMappingProvider MappingProvider
        {
            get;
            internal set;
        }

        public Type Type
        {
            get;
            internal set;
        }

        public Table Table
        {
            get;
            internal set;
        }

        public IList<Column> InsertColumns
        {
            get { return _insertColumns; }
        }

        public IList<Column> UpdateColumns
        {
            get { return _updateColumns; }
        }

        public FastProperty GetMappingProperty(string columnName)
        {
            Column column = Table.Columns.SingleOrDefault(col => col.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));

            return column == null ? null : column.Property;
        }

        public ISqlCommand CreateSelectCommand(object id)
        {
            return MappingProvider.CreateSelectCommand(this, id);
        }

        public ISqlCommand CreateExistsCommand(object parameters)
        {
            return MappingProvider.CreateSelectCountCommand(this, parameters);
        }

        public ISqlCommand CreateSelectAllCommand()
        {
            return MappingProvider.CreateSelectAllCommand(this);
        }

        public ISqlCommand CreateInsertCommand(object entity)
        {
            return MappingProvider.CreateInsertCommand(this, entity);
        }

        public ISqlCommand CreateUpdateCommand(object entity)
        {
            return MappingProvider.CreateUpdateCommand(this, entity);
        }

        public ISqlCommand CreateBatchUpdateCommand(object entity, object parameter)
        {
            return MappingProvider.CreateBatchUpdateCommand(this, entity, parameter);
        }

        public ISqlCommand CreateUpdateCommand(object entity, string[] fields, bool inclusive)
        {
            return MappingProvider.CreateUpdateCommand(this, entity, fields, inclusive);
        }

        public ISqlCommand CreateDeleteCommand(object id)
        {
            return MappingProvider.CreateDeleteCommand(this, id);
        }

        public ISqlCommand CreateBatchDeleteCommand(object id)
        {
            return MappingProvider.CreateBatchDeleteCommand(this, id);
        }

        private void Initialize()
        {
            PropertyInfo[] props = null;
            if (Type != null)
            {
                props = Type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            }
            Table.Columns.ForEach(col =>
            {
                ColumnAttribute attr = null;

                FastProperty prop = null;
                if (!props.IsNullOrEmpty())
                {
                    foreach (PropertyInfo info in props)
                    {
                        attr = info.GetCustomAttribute<ColumnAttribute>(true);
                        if (null != attr && !string.IsNullOrEmpty(attr.Name))
                        {
                            if (col.Name.Equals(attr.Name, StringComparison.OrdinalIgnoreCase))
                            {
                                prop = new FastProperty(attr.Name, info);
                                break;
                            }
                        }
                        else
                        {
                            if (col.Name.Equals(info.Name, StringComparison.OrdinalIgnoreCase) ||
                                    col.Name.Replace("_", "").Replace(" ", "").Equals(info.Name, StringComparison.OrdinalIgnoreCase))
                            {
                                prop = new FastProperty(info.Name, info);
                                break;
                            }
                        }
                    }
                }
                if (null != prop)
                {
                    col.Property = prop;
                }
                if (null == attr || attr.Insert)
                {
                    _insertColumns.Add(col);
                }

                if (null == attr || attr.Update)
                {
                    _updateColumns.Add(col);
                }
            });
        }
    }
}