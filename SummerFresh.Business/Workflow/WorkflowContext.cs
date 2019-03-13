using SummerFresh.Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business.Workflow
{
    public class WorkflowContext
    {
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public UserEntity CurrentUser { get; set; }

        /// <summary>
        /// 当前流程实例信息
        /// </summary>
        public FlowInstance FlowInstance { get; set; }

        /// <summary>
        /// 当前工作项
        /// </summary>
        public WorkItem CurrentTask { get; set; }

        /// <summary>
        /// 用户选择
        /// </summary>
        public IList<WorkflowChoice> UserChoice { get; set; }

        /// <summary>
        /// 流程参数
        /// </summary>
        public IDictionary<string, object> Parameter { get; set; }

        /// <summary>
        /// 并行分支标识
        /// </summary>
        public string LevelCode { get; set; }

        /// <summary>
        /// 意见内容
        /// </summary>
        public string OpinionContent { get; set; }

        /// <summary>
        /// 意见所属区域
        /// </summary>
        public int OpinionArea { get; set; }
    }
}
