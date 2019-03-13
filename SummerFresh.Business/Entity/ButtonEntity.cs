using SummerFresh.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    [UnValidateInputeClass]
    [Table("APP_Button")]
    public class ButtonEntity
    {
        [DisplayName("主键ID")]
        [PrimaryKey]
        public string ButtonId { get; set; }

        [DisplayName("按钮控件ID")]
        public string ButtonControlId { get; set; }

        [DisplayName("按钮控件名称")]
        public string ButtonName { get; set; }

        [DisplayName("排序号")]
        public int ButtonSortNumber { get; set; }

        [DisplayName("按钮所属")]
        public string BelongTo { get; set; }

        [DisplayName("按钮样式类")]
        public string CssClass { get; set; }

        [DisplayName("按钮点击事件")]
        public string OnClick { get; set; }

        [DisplayName("数据字段")]
        public string DataFields { get; set; }

        [DisplayName("按钮类型")]
        [EnumDataSource(typeof(ButtonEntityType))]
        public ButtonEntityType ButtonType { get; set; }

        [DisplayName("扩展属性")]
        public string HtmlAttributes { get; set; }

        [FormField(Editable=false)]
        [TableField(IsShow=false)]
        [Column(Update=false,Insert=false)]
        public Func<IDictionary<string, object>, bool> onRowDataBind { get; set; }


        public override bool Equals(object obj)
        {
            var other = obj as ButtonEntity;
            if (other != null)
                return this.ButtonControlId.Equals(other.ButtonControlId);
            return false;
        }
    }

    /// <summary>
    /// 按钮类型枚举
    /// </summary>
    public enum ButtonEntityType
    {
        /// <summary>
        /// 工具栏
        /// </summary>
        [Description("工具栏")]
        Toolbar,

        /// <summary>
        /// 表格行
        /// </summary>
        [Description("表格行")]
        TableRow,

        /// <summary>
        /// 表格行右键
        /// </summary>
        [Description("表格行右键")]
        TableRowContextMenu
    }

    public enum DefaultButton
    {
        Insert,
        Edit,
        Delete,
        View,
        BatchDelete,
        Export,
        Import,
        Enabled,
        Disabled
    }
}
