
using SummerFresh.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SummerFresh.Business.Entity
{

    [Tree(IdParameterName="ParentId")]
    [Table("SYS_Department",ShowCheckbox=false)]
    public class DepartmentEntity : CustomEntity
    {
        [PrimaryKey]
        [DisplayName("组织ID")]
        [TableField(IsShow = false)]
        public string DepartmentId { get; set; }

        [TitleField]
        [DisplayName("组织名称")]
        [SearchField]
        [Validator("required,length[1,10]")]
        public string DepartmentName { get; set; }

        [DisplayName("组织编码")]
        [TableField(Sortable = true)]
        [Validator("required,length[1,10]")]
        public string DepartmentCode { get; set; }

        //[SearchField(IsSearchControl=false)]
        [SearchField]
        [DisplayName("所属父级")]
        [Validator("required")]
        [CustomEntityDataSource(typeof(DepartmentEntity))]
        [FormField(ControlType = ControlType.DropDownList)]
        public string ParentId { get; set; }

        [DisplayName("排序号")]
        [TableField(Sortable = true)]
        [DefaultSortField(Business.OrderByType.ASC)]
        public int Rank { get; set; }

        [DisplayName("状态")]
        [DictionaryDataSource("Status")]
        [FormField(ControlType = ControlType.DropDownList,DefaultValue="Enabled")]
        public string Status { get; set; }
    }
}