using SummerFresh.Business;
using SummerFresh.Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
namespace SummerFresh.Controls
{
    public static class ControlDefaultSetting
    {
        public static IList<TableButton> GetDefaultTableButton()
        {
            var result = new List<TableButton>();
            result.Add(new TableButton() { CssClass = "btn btn-default btn-sm", ID = "btnEdit", Name = "编辑", TableButtonType = TableButtonType.TableRow });
            result.Add(new TableButton() { CssClass = "btn btn-default btn-sm", ID = "btnDelete", Name = "删除", TableButtonType = TableButtonType.TableRow });
            return result;
        }

        public static IList<Button> GetDefaultToolbarButton()
        {
            var result = new List<Button>();
            result.Add(new Button() { CssClass = "btn btn-primary", ID = "btnInsert", Label = "新增", ButtonType = ButtonType.Button });
            result.Add(new Button() { CssClass = "btn btn-default", ID = "btnBatchDelete", Label = "批量删除", ButtonType = ButtonType.Button });
            return result;
        }

        public static IList<Button> GetDefaultFormButton()
        {
            var result = new List<Button>();
            result.Add(new Button() { CssClass = "btn btn-success btn-big", ID = "btnSubmit", Label = "保存", ButtonType = ButtonType.Submit });
            //result.Add(new Button() { CssClass = "btn btn-danger btn-big", ID = "btnReset", Label = "重置", ButtonType = ButtonType.Reset });
            return result;
        }

        public static IList<Button> GetDefaultSearchButton()
        {
            var result = new List<Button>();
            result.Add(new Button() { CssClass = "btn btn-primary search-button", ID = "btnSearchSubmit", Label = "查询", ButtonType = ButtonType.Submit });
            return result;
        }

        public static FormControlBase GetFormControl(DataFieldEntity field)
        {
            FormControlBase result = null;
            switch (field.FieldType.ToLower())
            {
                case "datetime":
                    result = new DatePicker();
                    break;
                case "bit":
                    result = new DropDownList()
                    {
                        DataSource = GetDataSource(field) as IKeyValueDataSource
                    };
                    break;
                default:
                    result = new TextBox();
                    break;
            }
            if(field.FieldName.StartsWith("DD"))
            {
                result = new DropDownList()
                {
                    DataSource = GetDataSource(field) as IKeyValueDataSource
                };
            }
            result.ID = field.FieldName;
            result.Name = field.FieldName;
            result.Label = field.FieldComment;
            result.Rank = field.Rank;
            result.Enable = true;
            result.Visiable = true;
            return result;
        }

        public static TableColumn GetTableColumn(DataFieldEntity field)
        {
            var column = new TableColumn()
            {
                ID = "column{0}".FormatTo(field.FieldName),
                FieldName = field.FieldName,
                ColumnName = field.FieldComment,
                IsKey = field.FieldIsKey,
                Sortable = field.FieldSortable,
                Visiable = true,
                TextAlign = CellTextAlign.Center,
                ShowLength = 10,
                Rank = (field.Rank * 3)
            };
            if(field.FieldType.Equals("datetime", StringComparison.OrdinalIgnoreCase))
            {
                column.DataFormatString = "{0:yyyy-MM-dd}";
            }
            column.ColumnConverter = GetDataSource(field);
            return column;
        }

        public static IFieldConverter GetDataSource(DataFieldEntity field)
        {
            IFieldConverter result = null;
            if (field.FieldType.ToLower() == "bit")
            {
                result = new DictionaryDataSource() { DictionaryCode = "YesOrNo" };
            }
            if (field.FieldName.IndexOf("UserId") >= 0 || field.FieldName.IndexOf("Creator") >= 0)
            {
                result = new EntityDataSource() { EntityTypeFullName = "SummerFresh.Business.Entity.UserEntity" };
            }
            if (field.FieldName.IndexOf("DepartmentId") >= 0 ||  field.FieldName.IndexOf("DeptId") >= 0)
            {
                result = new EntityDataSource() { EntityTypeFullName = "SummerFresh.Business.Entity.DepartmentEntity" };
            }
            if(field.FieldName.StartsWith("DD"))
            {
                string code = field.FieldName.Substring(2);
                result = new DictionaryDataSource() { DictionaryCode = code };
            }
            return result;
        }
    }
}
