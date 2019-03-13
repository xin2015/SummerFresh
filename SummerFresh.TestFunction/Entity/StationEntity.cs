using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Data.Attributes;

namespace SummerFresh.Business.Entity
{
    [Table("Station")]
    public class StationEntity:CustomEntity
    {
        [PrimaryKey]
        [TableField(IsShow=false)]
        public virtual string StationId
        {
            get;
            set;
        }

        [TitleField]
        [DisplayName("站点名称")]
        [SearchField]
        public virtual string PositionName
        {
            get;
            set;
        }

        [DisplayName("所属城市")]
        public virtual string Area
        {
            get;
            set;
        }

        [DisplayName("唯一编号")]
        public virtual string UniqueCode
        {
            get;
            set;
        }

        [DisplayName("站点编号")]
        [TableField(IsShow = false)]
        public virtual string StationCode
        {
            get;
            set;
        }

        [DisplayName("站点图片")]
        [TableField(IsShow = false)]
        public virtual string StationPic
        {
            get;
            set;
        }

        [DisplayName("经度")]
        [TableField(IsShow = false)]
        public virtual string Longitude
        {
            get;
            set;
        }

        [DisplayName("纬度")]
        [TableField(IsShow = false)]
        public virtual string Latitude
        {
            get;
            set;
        }

        [DisplayName("地址")]
        [TableField(IsShow = false)]
        public virtual string Address
        {
            get;
            set;
        }

        [DisplayName("监测污染物")]
        [TableField(IsShow = false)]
        public virtual string PollutantCodes
        {
            get;
            set;
        }

        [DisplayName("站点类型")]
        [CustomEntityDataSource(typeof(StationTypeEntity))]
        [TableField(IsShow = false)]
        [FormField(ControlType= ControlType.DropDownList)]
        public virtual string StationTypeId
        {
            get;
            set;
        }

        [DisplayName("站点状态")]
        //[FunctionDataSource(typeof(bool))]
        public virtual bool Status
        {
            get;
            set;
        }

        [DisplayName("建站日期")]
        [TableField(IsShow = false)]
        public virtual DateTime BuildDate
        {
            get;
            set;
        }

        [DisplayName("号码")]
        [TableField(IsShow = false)]
        public virtual string Phone
        {
            get;
            set;
        }

        [DisplayName("管理者")]
        [TableField(IsShow = false)]
        public virtual string Manager
        {
            get;
            set;
        }

        [DisplayName("描述")]
        [TableField(IsShow=false)]
        public virtual string Description
        {
            get;
            set;
        }

        [DisplayName("是否对照点")]
        [TableField(IsShow = false)]
        public virtual bool IsContrast
        {
            get;
            set;
        }
        [DisplayName("是否监控")]
        [TableField(IsShow = false)]
        public virtual bool IsMonitor
        {
            get;
            set;
        }
		
    }
}
