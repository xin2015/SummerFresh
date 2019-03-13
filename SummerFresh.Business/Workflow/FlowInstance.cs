using SummerFresh.Data;
using SummerFresh.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business.Workflow
{
    [Table("WFL_Instance")]
    public class FlowInstance
    {
        [PrimaryKey]
        public string Id { get; set; }

        public int DefinitionId { get; set; }

        public string InstanceName
        {
            get;
            set;
        }

        public string CreatorUserId
        {
            get;
            set;
        }


        public FlowStatus FlowTag
        {
            get;
            set;
        }

        public DateTime? StartTime
        {
            get;
            set;
        }

        public DateTime? EndTime
        {
            get;
            set;
        }

        public string DataLocator
        {
            get;
            set;
        }

        public IList<WorkItem> WorkItems
        {
            get
            {
                return Dao.Get().SelectAll<WorkItem>().Where(o => o.InstanceId.Equals(Id, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }
    }

    public enum FlowStatus
    {
        /// <summary>
        /// 开始
        /// </summary>
        Begin,

        /// <summary>
        /// 运行中
        /// </summary>
        Running,

        /// <summary>
        /// 完成
        /// </summary>
        Finished
    }
}
