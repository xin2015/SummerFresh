using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Data.Attributes;

namespace SummerFresh.Business.Entity
{
    [Table("EquipmentName")]
    public class EquipmentNameEntity:CustomEntity
    {
        [PrimaryKey]
        [TableField(IsShow = false)]
        public string Id { get; set; }

        [TitleField]
        [DisplayName("名称")]
        public string Name { get; set; }

        [DisplayName("分类")]
        [CustomEntityDataSource(typeof(EquipmentTypeEntity))]
        [FormField(ControlType = ControlType.DropDownList)]
        public string EquipmentTypeId { get; set; }
    }
}
