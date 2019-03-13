using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Data.Attributes;

namespace SummerFresh.Business.Entity
{
    [Table("VHourDataShowApp",ShowIndex=false,ShowCheckbox=false)]
    [EntityService(typeof(QCHistoryEntityService))]
    public class StationHourDataEntity:CustomEntity
    {
        [DisplayName("站点")]
        [DefaultSortField(OrderByType.ASC)]
        [SearchField]
        public virtual string PositionName
        {
            get;
            set;
        }

        [DisplayName("时间")]
        [SearchField]
        public virtual DateTime TimePoint
        {
            get;
            set;
        }
        [TableField( IsShow=false)]
        public virtual string UniqueCode
        {
            get;
            set;
        }

        [TableField(IsShow = false)]
        public virtual string Type
        {
            get;
            set;
        }

        [TableField(IsShow = false)]
        public virtual string StationCode
        {
            get;
            set;
        }

        [Column("SO2Mark")]
        [TableField(IsShow = false)]
        public virtual string SO2_Mark
        {
            get;
            set;
        }

        //[TableField(DataFormatString = "<SO2>{0}({1})</SO2> <script> $(\"SO2\").each(function(){{  if($(this).html().indexOf(\"()\")>0) $(this).html($(this).html().replace(\"()\",\"\")); }})</script>", DataFormatFields = "SO2,SO2Mark")]
        //[TableField(DataFormatString = "<SO2>{0}({1})</SO2> <script> if($(\"SO2\").html().indexOf(\"()\")>0){{ alert( $(\"SO2\").html());$(\"SO2\").html($(\"SO2\").html().replace(\"()\",\"\")); }}</script>", DataFormatFields = "SO2,SO2Mark")]
        public virtual decimal SO2
        {
            get;
            set;
        }

        [TableField(IsShow = false)]
        public virtual string PrimaryPollutant
        {
            get;
            set;
        }

        [Column("PM2_5Mark")]
        [TableField(IsShow = false)]
        public virtual string PM2_5_Mark
        {
            get;
            set;
        }
        [DisplayName("PM2.5")]
        public virtual decimal PM2_5
        {
            get;
            set;
        }

        [Column("PM10Mark")]
        [TableField(IsShow = false)]
        public virtual string PM10_Mark
        {
            get;
            set;
        }

        public virtual decimal PM10
        {
            get;
            set;
        }

        [TableField(IsShow = false)]
        public virtual string O3_Mark
        {
            get;
            set;
        }

        //[Column("O3_8hMark")]
        //[TableField(IsShow = false)]
        //public virtual string O3_8h_Mark
        //{
        //    get;
        //    set;
        //}

        //public virtual decimal O3_8h
        //{
        //    get;
        //    set;
        //}

        public virtual decimal O3
        {
            get;
            set;
        }

        [Column("NO2Mark")]
        [TableField(IsShow = false)]
        public virtual string NO2_Mark
        {
            get;
            set;
        }

        public virtual decimal NO2
        {
            get;
            set;
        }

        [TableField(IsShow = false)]
        public virtual string Level
        {
            get;
            set;
        }

        [TableField(IsShow = false)]
        public virtual int Id
        {
            get;
            set;
        }

        [TableField(IsShow = false)]
        public virtual string Description
        {
            get;
            set;
        }

        [Column("COMark")]
        [TableField(IsShow = false)]
        public virtual string CO_Mark
        {
            get;
            set;
        }

        public virtual decimal CO
        {
            get;
            set;
        }

        [TableField(IsShow = false)]
        public virtual string City
        {
            get;
            set;
        }

        [TableField(IsShow = false)]
        public virtual int AQI
        {
            get;
            set;
        }

    }
}
