using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SummerFresh.Business.Entity
{
    public class LiveDataShowEntity
    {

        [DisplayName("站点")]
        public string PositionName { get; set; }

        //[TableField(IsShow=false)]
        //public DateTime TimePoint { get; set; }

        //[TableField(IsShow = false)]
        //[FunctionDataSource(typeof(PollutantValueDataSource))]
        //[DisplayName("状态")]
        //public string NetStatus { get; set; }

        [TableField(DefaultValue= "NA")]
        [FunctionDataSource(typeof(PollutantValueDataSource))]
        public string SO2 { get; set; }

        //[TableField(IsShow = false)]
        //public string SO2Mark { get; set; }

        [TableField(DefaultValue= "NA")]
        [FunctionDataSource(typeof(PollutantValueDataSource))]
        public string NO2 { get; set; }

        //[TableField(IsShow = false)]
        //public string NO2Mark { get; set; }

        [TableField(DefaultValue= "NA")]
        [FunctionDataSource(typeof(PollutantValueDataSource))]
        public string O3 { get; set; }

        //[TableField(IsShow = false)]
        //public string O3Mark { get; set; }

        [TableField(DefaultValue= "NA")]
        [FunctionDataSource(typeof(PollutantValueDataSource))]
        public string CO { get; set; }

        //[TableField(IsShow = false)]
        //public string COMark { get; set; }

        [TableField(DefaultValue= "NA")]
        [FunctionDataSource(typeof(PollutantValueDataSource))]
        public string PM10 { get; set; }

        //[TableField(IsShow = false)]
        //public string PM10Mark { get; set; }

        [TableField(DefaultValue= "NA")]
        [FunctionDataSource(typeof(PollutantValueDataSource))]
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
        [FunctionDataSource(typeof(PollutantValueDataSource))]
        public string 风向 { get; set; }

        //[TableField(IsShow = false)]
        //public string 风向Mark { get; set; }

        [TableField(DefaultValue= "NA")]
        [FunctionDataSource(typeof(PollutantValueDataSource))]
        public string 气压 { get; set; }

        //[TableField(IsShow = false)]
        //public string 气压Mark { get; set; }


        [TableField(DefaultValue= "NA")]
        [FunctionDataSource(typeof(PollutantValueDataSource))]
        public string 气温 { get; set; }

        //[TableField(IsShow = false)]
        //public string 气温Mark { get; set; }


        [TableField(DefaultValue= "NA")]
        [FunctionDataSource(typeof(PollutantValueDataSource))]
        public string 湿度 { get; set; }

        //[TableField(IsShow = false)]
        //public string 湿度Mark { get; set; }

        [TableField(DefaultValue= "NA")]
        [FunctionDataSource(typeof(PollutantValueDataSource))]
        public string 降水量 { get; set; }

        //[TableField(IsShow = false)]
        //public string 降水量Mark { get; set; }
    }
}
