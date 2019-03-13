using SummerFresh.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SummerFresh.Business.Workflow
{
    public class Activity
    {
        /// <summary>
        /// 步骤名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 步骤所属的流程对象
        /// </summary>
        public Workflow Owner { get; set; }

        /// <summary>
        /// 步骤参与者求解方式
        /// </summary>
        public Actor Actor { get; set; }

        /// <summary>
        /// 步骤向外迁移的集合
        /// </summary>
        public IList<Transition> Transitions { get; set; }

        /// <summary>
        /// 指示在该步骤是否需要弹出选人界面
        /// </summary>
        public bool NeedChoice { get; set; }

        /// <summary>
        /// 是否自动运行
        /// </summary>
        public virtual bool AutoRun { get { return false; } }


        public ResponseRuleType ResponseRuleType
        {
            get;
            set;
        }

        /// <summary>
        /// 能否进入步骤
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual bool CanEnter(WorkflowContext context)
        {
            return true;
        }

        /// <summary>
        /// 能否运行步骤
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual bool CanExecute(WorkflowContext context)
        {
            return true;
        }

        /// <summary>
        /// 能否退出步骤
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual bool CanExit(WorkflowContext context)
        {
            bool returnValue = true;
            if (ResponseRuleType != ResponseRuleType.OneResponse)
            {
                var workitems = context.FlowInstance.WorkItems.Where(o => o.ItemSeq == context.CurrentTask.ItemSeq);
                if (ResponseRuleType == ResponseRuleType.AllResponse)
                {
                    //如果审批方式 是 全部响应，则判断 相同的TaskSeq下还有无未结束的工作项，如果有则不允许退出步骤。
                    int workItemsCount = workitems.Count(o => o.Status < WorkItemStatus.Finished && o.ItemId != context.CurrentTask.ItemId);
                    if (workItemsCount > 0)
                    {
                        returnValue = false;
                    }
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Enter方法，通常用来生成一条或多条用户的待办
        /// </summary>
        /// <param name="context"></param>
        public virtual void Enter(WorkflowContext context)
        {
            var users = Actor.Resolve(context).OrderBy(o => o.Rank);
            string procId = context.FlowInstance.Id;
            var instance = context.FlowInstance.WorkItems.OrderByDescending(o => o.ItemId).First();
            DateTime dt = DateTime.Now;
            int i = 1;
            foreach (var user in users)
            {
                var workflowItem = new WorkItem();
                workflowItem.PartUserId = user.UserId;
                workflowItem.InstanceId = procId;
                workflowItem.ItemId = instance.ItemId + i;
                workflowItem.ItemSeq = instance.ItemSeq + 1;
                workflowItem.Status = WorkItemStatus.Sent; //如果是 串行审批  工作项的状态在这里是不一样的。
                workflowItem.ReceiveTime = dt;
                workflowItem.PreTaskId = context.CurrentTask.ItemId;
                workflowItem.CurrentActivity = this.Name;
                workflowItem.PasserUserId = context.CurrentUser.UserId;
                Dao.Get().Insert(workflowItem);
                i++;
            }
        }

        /// <summary>
        /// Execute方法，通常用来将待办置为已办
        /// </summary>
        /// <param name="context"></param>
        public virtual void Execute(WorkflowContext context)
        {
            context.CurrentTask.Status = WorkItemStatus.Finished;
            if (!context.CurrentTask.PartUserId.Equals(context.CurrentUser.UserId, StringComparison.CurrentCultureIgnoreCase))
            {
                context.CurrentTask.AssigneeUserId = context.CurrentUser.UserId;
            }
            if (context.UserChoice != null && context.UserChoice.Count > 0)
            {
                foreach (var uc in context.UserChoice)
                {
                    context.CurrentTask.UserChoice = uc.Choice + ",";
                }
                context.CurrentTask.UserChoice = context.CurrentTask.UserChoice.TrimEnd(',');
            }
            context.CurrentTask.FinishTime = DateTime.Now;
            Dao.Get().Update<WorkItem>(context.CurrentTask);
        }

        /// <summary>
        /// Exit方法，通常用来处理除当前工作项外同批次其它工作项的状态
        /// </summary>
        /// <param name="context"></param>
        public virtual void Exit(WorkflowContext context)
        {
            if (ResponseRuleType == ResponseRuleType.OneResponse)
            {
                //如果审批方式 是 任一人响应，则判断 相同的TaskSeq下还有无未结束的工作项，如果有 则 自动结束
                //var itemHelper = ObjectHelper.GetObject<IWorkflowItemService>();
                var workitems = context.FlowInstance.WorkItems.Where(o => o.ItemSeq == context.CurrentTask.ItemSeq && o.ItemId != context.CurrentTask.ItemId && o.Status < WorkItemStatus.Finished).ToList();
                var workitemsCount = workitems.Count;
                if (workitems != null && workitemsCount > 0)
                {
                    foreach (var workItem in workitems)
                    {
                        workItem.FinishTime = DateTime.Now;
                        workItem.AutoFinish = true;
                        workItem.Status = WorkItemStatus.AutoFinished;
                        workItem.AssigneeUserId = context.CurrentUser.UserId;
                        workItem.UserChoice = "任一人响应自动结束";
                        Dao.Get().Update<WorkItem>(workItem);
                    }
                }
            }
        }
    }

    public enum ResponseRuleType
    {
        /// <summary>
        /// 只要一个审批人选择“同意”，就可以退出这个环节
        /// </summary>
        [Description("任一人响应")]
        OneResponse,
        /// <summary>
        /// 只有在所有人都批示的情况下，才可以退出这个环节
        /// </summary>
        [Description("全部人响应")]
        AllResponse,
    }
}
