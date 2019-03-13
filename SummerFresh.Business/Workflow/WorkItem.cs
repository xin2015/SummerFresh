using SummerFresh.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business.Workflow
{
    [Table("WFL_WorkItem")]
    public class WorkItem
    {
        [PrimaryKey]
        [Column(DataType = "varchar", Length = "30")]
        public string InstanceId
        {
            get;
            set;
        }

        [PrimaryKey]
        public int ItemId
        {
            get;
            set;
        }

        public int ItemSeq
        {
            get;
            set;
        }

        [Column(DataType = "varchar", Length = "10")]
        public WorkItemStatus Status
        {
            get;
            set;
        }

        [Column(DataType = "varchar", Length = "1000")]
        public string OpinionContent
        {
            get;
            set;
        }


        public int OpinionType
        {
            get;
            set;
        }

        public int PreTaskId
        {
            get;
            set;
        }

        [Column(DataType = "varchar", Length = "50")]
        public string PartUserId
        {
            get;
            set;
        }

        [Column(DataType = "varchar", Length = "50")]
        public string AssigneeUserId
        {
            get;
            set;
        }

        public string CurrentActivity
        {
            get;
            set;
        }

        public bool AutoFinish
        {
            get;
            set;
        }

        [Column(DataType = "varchar", Length = "50")]
        public string PasserUserId
        {
            get;
            set;
        }

        public DateTime? ReceiveTime
        {
            get;
            set;
        }

        public DateTime? ReadTime
        {
            get;
            set;
        }

        public DateTime? FinishTime
        {
            get;
            set;
        }

        public string UserChoice { get; set; }

        public override bool Equals(object obj)
        {
            var target = obj as WorkItem;
            return target.InstanceId.Equals(this.InstanceId) && target.ItemId.Equals(ItemId);
        }

        public override int GetHashCode()
        {
            return this.InstanceId.GetHashCode() + this.ItemId.GetHashCode();
        }
    }

    public enum WorkItemStatus
    {
        /// <summary>
        /// 送达
        /// </summary>
        Sent,

        /// <summary>
        /// 已阅
        /// </summary>
        Readed,

        /// <summary>
        /// 完成
        /// </summary>
        Finished,

        /// <summary>
        /// 自动完成
        /// </summary>
        AutoFinished
    }
}
