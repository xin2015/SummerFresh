using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    public abstract class DataSourceAttribute : Attribute
    {
        public abstract IControl GetDataSource();
    }
    public class CustomEntityDataSourceAttribute : DataSourceAttribute
    {
        public CustomEntityDataSourceAttribute(Type type)
        {
            EntityType = type;
        }
        public Type EntityType { get; set; }


        public override IControl GetDataSource()
        {
            var result = new EntityDataSource(EntityType);
            return result;
        }
    }

    public class DictionaryDataSourceAttribute : DataSourceAttribute
    {
        public DictionaryDataSourceAttribute(string dictionaryCode)
        {
            DictionaryCode = dictionaryCode;
        }

        public string DictionaryCode { get; set; }

        public override IControl GetDataSource()
        {
            var result = new DictionaryDataSource();
            result.DictionaryCode = DictionaryCode;
            return result;
        }
    }

    public class DataTableDataSourceAttribute : DataSourceAttribute
    {
        public string TableName { get; set; }

        public string DataTextField { get; set; }

        public string DataValueField { get; set; }

        public string SearchCondition { get; set; }

        public override IControl GetDataSource()
        {
            return new DataTableDataSource() { TableName = TableName, DataTextField = DataTextField, DataValueField = DataValueField, SearchCondition = SearchCondition };
        }
    }

    public class EnumDataSourceAttribute : DataSourceAttribute
    {
        private Type EnumType { get; set; }
        public EnumDataSourceAttribute(Type type)
        {
            if (!type.IsEnum)
            {
                throw new ArgumentOutOfRangeException("type");
            }
            EnumType = type;
        }

        public override IControl GetDataSource()
        {
            var result = new EnumDataSource();
            result.EnumType = EnumType;
            result.EnumValueType = EnumValueType.Code;
            return result;
        }
    }

    public class FunctionDataSourceAttribute : DataSourceAttribute
    {
        private Type Type { get; set; }

        public FunctionDataSourceAttribute(Type type)
        {
            Type = type;
        }
        public override IControl GetDataSource()
        {
            return Activator.CreateInstance(Type) as IControl;
        }
    }

    public class TableColumnConverter : Attribute
    {
        private Type Type { get; set; }

        public TableColumnConverter(Type type)
        {
            Type = type;
        }
        public IFieldConverter GetDataSource()
        {
            return Activator.CreateInstance(Type) as IFieldConverter;
        }
    }

    public class TypeDataSourceAttribute : DataSourceAttribute
    {
        private Type Type { get; set; }

        public TypeDataSourceAttribute(Type type)
        {
            Type = type;
        }
        public override IControl GetDataSource()
        {
            return new TypeDataSource() { BaseType = Type } as IKeyValueDataSource;
        }
    }
}
