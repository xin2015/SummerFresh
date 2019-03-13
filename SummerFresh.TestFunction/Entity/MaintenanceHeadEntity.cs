using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Data.Attributes;

namespace SummerFresh.Business.Entity
{
    [Table("Maintenance_Head")]
    public class MaintenanceHeadEntity : CustomEntity
    {
        [TableField(IsShow=false)]
        [PrimaryKey]
        public virtual string ID
        {
            get;
            set;
        }

        [DisplayName("名称")]
        [SearchField]
        public virtual string Person_Name
        {
            get;
            set;
        }

        [Validator(@"\d{11}")]
        [DisplayName("手机号码")]
        public virtual string Mobile_Phone
        {
            get;
            set;
        }

        [Validator(@"[_a-z\d\-\./]+@[_a-z\d\-]+(\.[_a-z\d\-]+)*(\.(info|biz|com|edu|gov|net|am|bz|cn|cx|hk|jp|tw|vc|vn))$")]
        [DisplayName("邮箱")]
        public virtual string Email
        {
            get;
            set;
        }

        [FormField(Editable = false)]
        [TableField(IsShow=false,DefaultValue="1990-01-01")]
        public virtual DateTime Create_Date
        {
            get;
            set;
        }

        [FormField(Editable = false)]
        [TableField(IsShow=false)]
        public virtual string Create_By
        {
            get;
            set;
        }

        [FormField(Editable = false)]
        [TableField(IsShow = false)]
        public virtual string Revised_By
        {
            get;
            set;
        }

        [FormField(Editable = false)]
        [TableField(IsShow = false,DefaultValue="1990-01-01")]
        public virtual DateTime Revised_Date
        {
            get;
            set;
        }

        [FormField(Editable=false)]
        [DefaultValue(1)]
        [TableField(IsShow = false)]
        public virtual int Status
        {
            get;
            set;
        }

        [DisplayName("职务")]
        [TableField(IsShow = false)]
        public virtual string Duty
        {
            get;
            set;
        }

        [FormField(Editable = false)]
        [TableField(IsShow = false)]
        public virtual string Remark
        {
            get;
            set;
        }

        [FormField(Editable = false)]
        [TableField(IsShow = false)]
        public virtual int OrganizationID
        {
            get;
            set;
        }

    }
}
