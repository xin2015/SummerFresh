using SummerFresh.Business.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    [DisplayName("实体转换器")]
    public class EntityColumnConverter:ColumnConverterBase
    {
        [DisplayName("实体")]
        [TypeDataSource(typeof(CustomEntity))]
        [FormField(ControlType = ControlType.DropDownList)]
        public string EntityTypeFullName
        {
            get;
            set;
        }

        private EntityDataSource _dataSource;
        protected override IFieldConverter FieldConverter
        {
            get
            {
                return _dataSource ?? (_dataSource = new EntityDataSource() { EntityTypeFullName = EntityTypeFullName });
            }
        }
    }
}
