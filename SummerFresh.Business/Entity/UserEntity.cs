using SummerFresh.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SummerFresh.Business.Entity
{
    [Tree(SqlId = "sqlid:SummerFresh.Business.Entity.DepartmentEntity.Tree", IdParameterName = "DepartmentId")]
    [Table("SYS_User")]
    public class UserEntity : CustomEntity
    {
        [PrimaryKey]
        [DisplayName("用户ID")]
        [TableField(IsShow = false)]
        public string UserId { get; set; }

        [TitleField]
        [SearchField]
        [TableField(Sortable = true)]
        [DisplayName("用户姓名")]
        [Validator("required,length[1,10],zhNumChar")]
        public string UserName { get; set; }

        [DisplayName("用户编码")]
        [Validator("required,length[1,10],noSpecialCaracters")]
        public string UserCode { get; set; }

        [DisplayName("登录账号")]
        [SearchField]
        [Validator("required,length[1,10],zhNumChar")]
        public string LoginId { get; set; }

        [DisplayName("登录密码")]
        [FormField(Editable = false)]
        [Column(Update = false)]
        [TableField(DefaultValue = "-", TextAlign = CellTextAlign.Center, IsShow = false)]
        public string Password { get; set; }


        [CustomEntityDataSource(typeof(DepartmentEntity))]
        [DisplayName("所属组织")]
        [Validator("required")]
        [TableField(TextAlign = CellTextAlign.Center)]
        [FormField(ControlType = ControlType.DropDownList, DefaultValue = "$QueryString:DepartmentId$")]
        //[FormField(ControlType = ControlType.TextValueTextBox, DefaultValue = "$QueryString:DepartmentId$", ExtendField = "SelectType:PollutantSelector;")]
        public string DepartmentId { get; set; }

        [DisplayName("状态")]
        [DictionaryDataSource("Status")]
        [FormField(ControlType=ControlType.DropDownList,DefaultValue="Enabled")]
        public string Status { get; set; }

        [DisplayName("排序号")]
        [TableField(Sortable = true,IsShow=false)]
        [DefaultSortField(Business.OrderByType.ASC)]
        public int Rank { get; set; }
    }
}