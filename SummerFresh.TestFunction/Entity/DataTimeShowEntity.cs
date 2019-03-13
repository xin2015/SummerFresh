using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Data.Attributes;

namespace SummerFresh.Business.Entity
{
    [Table("VLiveDataShow")]
    public class DataTimeShowEntity:CustomEntity
    {

        [FunctionDataSource(typeof(NetStatusIconDataSource))]
        [DisplayName("状态")]
        [TableField(DefaultValue="<div class='status0'></div>")]
        public string NetStatus { get; set; }

        [DisplayName("站点")]
        public string PositionName { get; set; }

        //[TableField(IsShow = false)]
        //public DateTime TimePoint { get; set; }

        [TableField(DefaultValue= "NA")]
        [FunctionDataSource(typeof(IconTimePointDataSource))]
        public string SO2 { get; set; }

        //[TableField(IsShow = false)]
        //public string SO2Mark { get; set; }

        [TableField(DefaultValue= "NA")]
        [FunctionDataSource(typeof(IconTimePointDataSource))]
        public string NO2 { get; set; }

        //[TableField(IsShow = false)]
        //public string NO2Mark { get; set; }


        [TableField(DefaultValue= "NA")]
        [FunctionDataSource(typeof(IconTimePointDataSource))]
        public string O3 { get; set; }


        [TableField(DefaultValue= "NA")]
        [FunctionDataSource(typeof(IconTimePointDataSource))]
        public string CO { get; set; }

        //[TableField(IsShow = false)]
        //public string COMark { get; set; }

        [TableField(DefaultValue= "NA")]
        [FunctionDataSource(typeof(IconTimePointDataSource))]
        public string PM10 { get; set; }

        //[TableField(IsShow = false)]
        //public string PM10Mark { get; set; }

        [TableField(DefaultValue= "NA")]
        [FunctionDataSource(typeof(IconTimePointDataSource))]
        [DisplayName("PM2.5")]
        public string PM2_5 { get; set; }

        //[TableField(IsShow = false)]
        //public string PM2_5Mark { get; set; }

        [TableField(DefaultValue= "NA")]
        [FunctionDataSource(typeof(IconTimePointDataSource))]
        public string 风速 { get; set; }

        //[TableField(IsShow = false)]
        //public string 风速Mark { get; set; }

        [TableField(DefaultValue= "NA")]
        [FunctionDataSource(typeof(IconTimePointDataSource))]
        public string 风向 { get; set; }

        //[TableField(IsShow = false)]
        //public string 风向Mark { get; set; }

        [TableField(DefaultValue= "NA")]
        [FunctionDataSource(typeof(IconTimePointDataSource))]
        public string 气压 { get; set; }

        //[TableField(IsShow = false)]
        //public string 气压Mark { get; set; }


        [TableField(DefaultValue= "NA")]
        [FunctionDataSource(typeof(IconTimePointDataSource))]
        public string 气温 { get; set; }

        //[TableField(IsShow = false)]
        //public string 气温Mark { get; set; }


        [TableField(DefaultValue= "NA")]
        [FunctionDataSource(typeof(IconTimePointDataSource))]
        public string 湿度 { get; set; }

        //[TableField(IsShow = false)]
        //public string 湿度Mark { get; set; }

        [TableField(DefaultValue= "NA")]
        [FunctionDataSource(typeof(IconTimePointDataSource))]
        public string 降水量 { get; set; }

        //[TableField(IsShow = false)]
        //public string 降水量Mark { get; set; }


        //[TableField(IsShow = false)]
        //public decimal SO2Value { get; set; }

        //[TableField(IsShow = false)]
        //public decimal NO2Value { get; set; }

        //[TableField(IsShow = false)]
        //public decimal O3Value { get; set; }


        //[TableField(IsShow = false)]
        //public decimal PM2_5Value { get; set; }

        //[TableField(IsShow = false)]
        //public decimal PM10Value { get; set; }

        //[TableField(IsShow = false)]
        //public decimal COValue { get; set; }


        //[TableField(IsShow = false)]
        //public decimal 风速Value { get; set; }

        //[TableField(IsShow = false)]
        //public decimal 风向Value { get; set; }

        //[TableField(IsShow = false)]
        //public decimal 降水量Value { get; set; }

        //[TableField(IsShow = false)]
        //public decimal 气温Value { get; set; }

        //[TableField(IsShow = false)]
        //public decimal 气压Value { get; set; }

        //[TableField(IsShow = false)]
        //public decimal 湿度Value { get; set; }




        //[TableField(IsShow = false)]
        //public string StationCode { get; set; }
        //public DateTime SO2 { get; set; }
        //public DateTime NO2 { get; set; }
        //public DateTime PM2_5 { get; set; }
        //public DateTime PM10 { get; set; }
        //public DateTime CO { get; set; }
        //public DateTime O3 { get; set; }
        //public DateTime 风速 { get; set; }
        //public DateTime 风向 { get; set; }
        //public DateTime 降水量 { get; set; }
        //public DateTime 气温 { get; set; }
        //public DateTime 气压 { get; set; }
        //public DateTime 湿度 { get; set; }

        //[TableField(IsShow = false)]
        //public string City_Code { get; set; }
        
        //[DisplayName("站点")]
        //public string PositionName { get; set; }
        //public string NetStatus { get; set; }
        //public DateTime TimePoint { get; set; }

        //[TableField(IsShow = false)]
        //public string OffLineSO2 { get; set; }

        //[TableField(IsShow = false)]
        //public string OffLineNO2 { get; set; }

        //[TableField(IsShow = false)]
        //public string OffLinePM2_5 { get; set; }

        //[TableField(IsShow = false)]
        //public string OffLinePM10 { get; set; }

        //[TableField(IsShow = false)]
        //public string OffLineCO { get; set; }

        //[TableField(IsShow = false)]
        //public string OffLineO3 { get; set; }

        //[TableField(IsShow = false)]
        //public string OffLine风速 { get; set; }

        //[TableField(IsShow = false)]
        //public string OffLine风向 { get; set; }

        //[TableField(IsShow = false)]
        //public string OffLine降水量 { get; set; }

        //[TableField(IsShow = false)]
        //public string OffLine气温 { get; set; }

        //[TableField(IsShow = false)]
        //public string OffLine气压 { get; set; }

        //[TableField(IsShow = false)]
        //public string OffLine湿度 { get; set; }
    }
}
