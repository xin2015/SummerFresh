using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Data.Attributes;

namespace SummerFresh.Business.Entity
{
    [Table("EquipmentBrand")]
    public class EquipmentBrandEntity:CustomEntity
    {
        [PrimaryKey]
        [TableField(IsShow = false)]
        public string Id { get; set; }

        [TitleField]
        [DisplayName("品牌")]
        public string Name { get; set; }

        [DisplayName("名称")]
        [CustomEntityDataSource(typeof(EquipmentNameEntity))]
        [FormField(ControlType = ControlType.DropDownList)]
        public string EquipmentNameId { get; set; }
    }
}
