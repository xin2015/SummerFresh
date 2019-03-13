using SummerFresh.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SummerFresh.Business.Entity
{
    [Table("Sys_Menu")]
    public class MenuEntity:CustomEntity
    {
        [PrimaryKey]
        [TableField(IsShow=false)]
        public string ID { get; set; }

        [TitleField]
        [DisplayName("功能名称")]
        [SearchField]
        public string Menu_Name { get; set; }

        [DisplayName("功能类型")]
        public string Menu_Type { get; set; }

        [DisplayName("URL地址")]
        public string Page_Url { get; set; }


        [CustomEntityDataSource(typeof(MenuEntity))]
        [DisplayName("所属父级")]
        [TableField(TextAlign = CellTextAlign.Center)]
        [FormField(ControlType = ControlType.DropDownList)]
        public string Parent_ID { get; set; }

        [DisplayName("排序值")]
        public int Menu_Order { get; set; }

        [DisplayName("是否菜单")]
        public int Is_Menu { get; set; }

        [DisplayName("描述")]
        [FormField(ControlType=ControlType.TextArea)]
        public string Remark { get; set; }
    }
}
