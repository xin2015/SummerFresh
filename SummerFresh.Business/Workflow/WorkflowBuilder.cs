using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business.Workflow
{
    public static class WorkflowBuilder
    {
        private static IList<Workflow> allWorkflow;

        static WorkflowBuilder()
        {
            allWorkflow = new List<Workflow>();
            var definitions = TypeHelper.GetAllSubTypeInstance<WorkflowDefinition>();
            foreach(var d in definitions)
            {
                allWorkflow.Add(d.Definition());
            }
        }
        public static Workflow Build(int definitionId)
        {
            var d = allWorkflow.FirstOrDefault(o => o.Id.Equals(definitionId));
            return d;
        }
    }

    public class WorkflowDefinition
    {
        public virtual Workflow Definition()
        {
            var workflow = new Workflow() { Id = 0, Name = "测试流程" };
            var acti1= workflow.AddActivity("开始");
            var acti2 = workflow.AddActivity("审批");
            var acti3 = workflow.AddActivity("归档");
            var end = workflow.AddEndActivity();
            workflow.Connect(acti1, acti2);
            workflow.Connect(acti2, acti3);
            workflow.Connect(acti3, end);
            return workflow;
        }
    }
}
