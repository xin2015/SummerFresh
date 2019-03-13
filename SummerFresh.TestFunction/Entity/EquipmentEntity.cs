using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Data.Attributes;

namespace SummerFresh.Business.Entity
{
    [Table("Equipment")]
    public class EquipmentEntity : CustomEntity
    {
        [PrimaryKey]
        [TableField(IsShow=false)]
        public virtual string Id
        {
            get;
            set;
        }

        [DisplayName("出厂编号")]
        public virtual string ManufacturingCode
        {
            get;
            set;
        }

        [DisplayName("固定资产编号")]
        public virtual string FixedAssetCode
        {
            get;
            set;
        }
        [CustomEntityDataSource(typeof(EquipmentModelEntity))]
        [FormField(ControlType= ControlType.DropDownList)]
        [DisplayName("仪器类型")]
        public virtual string EquipmentModelId
        {
            get;
            set;
        }

        [TableField(IsShow=false)]
        public virtual string Remark
        {
            get;
            set;
        }

        [DisplayName("购置日期")]
        [TableField(IsShow=false)]
        public virtual DateTime PurchaseDate
        {
            get;
            set;
        }

        [TableField(IsShow=false)]
        public virtual int State
        {
            get;
            set;
        }

        [TableField(IsShow=false)]
        public virtual bool IsDeleted
        {
            get;
            set;
        }

        [CustomEntityDataSource(typeof(StationEntity))]
        [FormField(ControlType = ControlType.DropDownList)]
        [DisplayName("站点")]
        public virtual string StationCode
        {
            get;
            set;
        }

        [DisplayName("污染物")]
        public virtual string PollutantCodes
        {
            get;
            set;
        }

        //public virtual EquipmentModel EquipmentModel
        //{
        //    get;
        //    set;
        //}

    }
}
