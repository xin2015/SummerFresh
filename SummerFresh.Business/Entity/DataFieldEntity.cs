using SummerFresh.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using SummerFresh.Business.Service;
namespace SummerFresh.Business.Entity
{
    [EntityService(typeof(DataFieldEntityService))]
    public class DataFieldEntity : CustomEntity
    {
        [PrimaryKey]
        [FormField(Editable = false)]
        [TableField(IsShow = false)]
        public string FieldID { get; set; }

        [DisplayName("字段名")]
        [TableField(TextAlign = CellTextAlign.Left)]
        public string FieldName { get; set; }

        [DisplayName("注释")]
        [TableField(TextAlign = CellTextAlign.Left)]
        public string FieldComment { get; set; }

        [DisplayName("类型")]
        [TableColumnConverter(typeof(DataFieldDataSource))]
        [TableField(TextAlign = CellTextAlign.Left)]
        public string FieldType { get; set; }

        [DisplayName("可为空")]
        public bool FieldIsNullable { get; set; }

        [DisplayName("自增长")]
        public bool FieldIsAutoIncrease { get; set; }

        [DisplayName("长度")]
        [TableField(IsShow = false)]
        public string FieldLength { get; set; }

        [DisplayName("主键")]
        public bool FieldIsKey { get; set; }

        [TableField(IsShow = false)]
        [DefaultSortField(Business.OrderByType.ASC)]
        public int Rank { get; set; }

        [DisplayName("表单编辑")]
        public bool FieldEditable { get; set; }

        [DisplayName("列表显示")]
        public bool FieldDisplayable { get; set; }

        [DisplayName("列表排序")]
        public bool FieldSortable { get; set; }

        [DisplayName("查询字段")]
        public bool FieldSearchable { get; set; }
    }

    public class DataFieldEntityService : CustomEntityService
    {
        public override IList<ButtonEntity> AddButtons()
        {
            var buttons = new List<ButtonEntity>();
            buttons.Add(new ButtonEntity()
            {
                ButtonControlId = "btnGenerateTable",
                ButtonName = "生成CRUD",
                CssClass = "btn btn-primary",
                OnClick = "generaetCRUD()",
                ButtonType = ButtonEntityType.Toolbar
            });
            return buttons;
        }
    }

    public class DataFieldDataSource : IFieldConverter
    {
        public object Converter(string columnName, object columnValue, IDictionary<string, object> rowData)
        {
            if (rowData["FieldLength"] != null && !rowData["FieldLength"].ToString().IsNullOrEmpty())
            {
                return "{0}({1})".FormatTo(columnValue, rowData["FieldLength"]);
            }
            return columnValue;
        }

        public string ID
        {
            get;
            set;
        }
    }
}
