using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Data.Attributes;

namespace SummerFresh.Business.Entity
{
    [Table("EquipmentModel")]
    public class EquipmentModelEntity:CustomEntity
    {
        [PrimaryKey]
        [TableField(IsShow=false)]
        public string Id { get; set; }

        [TitleField]
        [DisplayName("型号")]
        public string Name { get; set; }

        [DisplayName("品牌")]
        [CustomEntityDataSource(typeof(EquipmentBrandEntity))]
        [FormField(ControlType = ControlType.DropDownList)]
        public string EquipmentBrandId { get; set; }
    }
}
