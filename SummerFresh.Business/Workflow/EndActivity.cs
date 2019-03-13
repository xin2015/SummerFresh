using SummerFresh.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business.Workflow
{
    public class EndActivity:Activity
    {
        public override bool CanExit(WorkflowContext context)
        {
            return true;
        }

        public override void Enter(WorkflowContext context)
        {
            var workitem = new WorkItem();

            string procId = context.FlowInstance.Id;
            var newestItem = context.FlowInstance.WorkItems.OrderByDescending(o => o.ItemId).First();
            workitem.AutoFinish = true;
            workitem.Status = WorkItemStatus.AutoFinished;
            workitem.ItemId = newestItem.ItemId + 1;
            workitem.ItemSeq = newestItem.ItemSeq + 1;
            workitem.InstanceId = context.FlowInstance.Id;
            workitem.ReceiveTime = DateTime.Now;
            workitem.FinishTime = DateTime.Now;
            workitem.CurrentActivity = Name;
            workitem.PreTaskId = context.CurrentTask.ItemId;
            workitem.PasserUserId = context.CurrentUser.UserId;
            Dao.Get().Insert<WorkItem>(workitem);
            context.FlowInstance.FlowTag = FlowStatus.Finished;
            context.FlowInstance.EndTime = DateTime.Now;
            Dao.Get().Update<FlowInstance>(context.FlowInstance);
        }

        public override void Execute(WorkflowContext context)
        {
            
        }

        public override void Exit(WorkflowContext context)
        {
            
        }
    }
}
