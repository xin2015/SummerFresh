using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Data.Attributes;
using SummerFresh.Business.Service;

namespace SummerFresh.Business.Entity
{
    [EntityService(typeof(QCHistoryEntityService))]
    [Table("QC_History",ShowIndex=false,ShowCheckbox=false)]
    public class QCHistoryEntity:CustomEntity
    {
        [PrimaryKey]
        [TableField(IsShow=false)]
        public virtual string Id
        {
            get;
            set;
        }


        [SearchField(Rank = 1)]
        [DisplayName("站点")]
        [Column(Update = false, Insert = false)]
        public string PositionName
        {
            get;
            set;
        }
        //public virtual string UniqueCode
        //{
        //    get;
        //    set;
        //}

        //public virtual DateTime Real_Start_Time
        //{
        //    get;
        //    set;
        //}

        [SearchField(Rank=3)]
        [DisplayName("开始时间")]
        public virtual DateTime Start_Time
        {
            get;
            set;
        }

        [SearchField(Rank=4)]
        [FormField( ControlType= ControlType.DateTimeRange)]
        [DisplayName("结束时间")]
        public virtual DateTime End_Time
        {
            get;
            set;
        }

        [DisplayName("任务组")]
        public virtual string Mission_Group_Name
        {
            get;
            set;
        }

        [SearchField(Rank=2)]
        [DisplayName("任务名称")]
        public virtual string Mission_Name
        {
            get;
            set;
        }

        [DisplayName("质控结果")]
        [TableField(DataFormatFields = "Result,Document_Name,Document_Address", DataFormatString = "{0} <a style='color:#00ff00' href='{2}\\{1}'>下载文件</a>")]
        public virtual string Result
        {
            get;
            set;
        }

        [DisplayName("误差")]
        public virtual decimal Inaccuracy
        {
            get;
            set;
        }

        [DisplayName("目标值")]
        public virtual decimal Target_Value
        {
            get;
            set;
        }

        [DisplayName("响应值")]
        public virtual decimal Relevant_Value
        {
            get;
            set;
        }

        [DisplayName("警告限（%）")]
        public virtual decimal Warming_Limit
        {
            get;
            set;
        }

        [DisplayName("控制限（%）")]
        public virtual decimal Control_Limit
        {
            get;
            set;
        }

        //public virtual int Send_Field_Split
        //{
        //    get;
        //    set;
        //}

        //public virtual string Send_Field
        //{
        //    get;
        //    set;
        //}

        //public virtual byte Source
        //{
        //    get;
        //    set;
        //}

        //public virtual string Document_Name
        //{
        //    get;
        //    set;
        //}

        //public virtual string Document_Address
        //{
        //    get;
        //    set;
        //}

        //public virtual DateTime Create_Time
        //{
        //    get;
        //    set;
        //}


        //public virtual string StationCode
        //{
        //    get;
        //    set;
        //}
    }

    public class QCHistoryEntityService:CustomEntityService
    {
        public override IList<ButtonEntity> AddButtons()
        {
            return null;
        }
    }
}
