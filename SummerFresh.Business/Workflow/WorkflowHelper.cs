using SummerFresh.Business.Entity;
using SummerFresh.Data;
using SummerFresh.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using SummerFresh.Basic;
using System.Transactions;
namespace SummerFresh.Business.Workflow
{
    public class WorkflowHelper
    {
        public WorkflowHelper(string loginId)
        {
            CurrentUser = BSDContext<UserEntity>.Instance.FirstOrDefault(o => o.LoginId.Equals(loginId, StringComparison.OrdinalIgnoreCase));
        }
        public WorkflowHelper(IPrincipal user)
            : this(user.Identity.Name)
        {

        }

        public WorkflowHelper(UserEntity user)
        {
            CurrentUser = user;
        }

        public WorkItem CurrentTask { get; private set; }

        public Activity CurrentActivity { get; private set; }

        public Workflow CurrentWorkflow { get; private set; }

        public UserEntity CurrentUser { get; private set; }

        public FlowInstance FlowInstance
        {
            get;
            private set;
        }

        public void StartWorkflow(int definitionId, string procName, string dataLocator, int impoLevel = 0, int secret = 0)
        {
            CurrentWorkflow = Build(definitionId);
            var instance = new FlowInstance();
            instance.CreatorUserId = CurrentUser.UserId;
            instance.StartTime = DateTime.Now;
            instance.FlowTag = FlowStatus.Begin;
            instance.InstanceName = procName;
            instance.DataLocator = dataLocator;
            Dao.Get().Insert<FlowInstance>(FlowInstance);

            var newItem = new WorkItem();
            newItem.CurrentActivity = CurrentWorkflow.Root.Name;
            newItem.ItemSeq = 1;
            newItem.ItemId = 1;
            newItem.Status = WorkItemStatus.Sent;
            newItem.InstanceId = instance.Id;
            newItem.ReceiveTime = DateTime.Now;
            newItem.PartUserId = CurrentUser.UserId;
            Dao.Get().Insert<WorkItem>(newItem);
            CurrentActivity = CurrentWorkflow.Root;
            CurrentTask = newItem;
        }

        public void UpdateInstance(string procName, string dataLocator, int impoLevel, int secret)
        {
            Validate();
            FlowInstance.InstanceName = procName;
            FlowInstance.DataLocator = dataLocator;
            Dao.Get().Update<FlowInstance>(FlowInstance);
        }

        public Workflow Build(int definitionId)
        {
            var returnValue = WorkflowBuilder.Build(definitionId);
            ValidateWorkflow(returnValue);
            return returnValue;
        }

        /// <summary>
        /// 检查流程定义是否有效
        /// </summary>
        /// <param name="workflow"></param>
        private void ValidateWorkflow(Workflow workflow)
        {
            foreach (var acti in workflow.Activities)
            {
                if (!(acti is EndActivity))
                {
                    if (acti.Transitions == null || acti.Transitions.Count == 0)
                    {
                        throw new Exception(string.Format("流程定义错误 ，步骤【{0}】没有向外的迁移", acti.Name));
                    }
                }
                if (!ExistTranToActi(workflow, acti))
                {
                    throw new Exception(string.Format("流程定义错误，没有指向步骤【{0}】的迁移", acti.Name));
                }
            }
            foreach (var tran in workflow.Transitions)
            {
                if (tran.To == null || tran.From == null)
                {
                    throw new Exception("无效迁移");
                }
            }
        }

        /// <summary>
        /// 是否存在指向某步骤的迁移
        /// </summary>
        /// <param name="workflow"></param>
        /// <param name="acti"></param>
        /// <returns></returns>
        private bool ExistTranToActi(Workflow workflow, Activity acti)
        {
            //如果该步骤是开始步骤，则无指向该步骤的迁移也正常。
            if (acti == workflow.Root) return true;
            foreach (var tran in workflow.Transitions)
            {
                if (tran.To == acti)
                {
                    return true;
                }
            }
            return false;
        }

        public void OpenWorkflow(string dataLocator)
        {
            var instance = Dao.Get().SelectAll<FlowInstance>().FirstOrDefault(o => o.DataLocator.Equals(dataLocator, StringComparison.OrdinalIgnoreCase));
            if (instance == null)
            {
                throw new Exception("不存在datalocator为：{0}的流程实例".FormatTo(dataLocator));
            }
            OpenWorkflow(instance, 1);
        }

        public void OpenWorkflow(string procID, int taskID)
        {
            var instance = Dao.Get().Select<FlowInstance>(procID);
            if (instance == null)
            {
                throw new Exception("不存在实例号为：{0}的流程实例".FormatTo(procID));
            }
            OpenWorkflow(instance, taskID);
        }

        private void OpenWorkflow(FlowInstance instance, int taskId)
        {
            CurrentTask = instance.WorkItems.FirstOrDefault(o => o.ItemId == taskId);
            if (CurrentTask == null)
            {
                throw new Exception("taskId");
            }
            FlowInstance = instance;
            CurrentWorkflow = Build(instance.DefinitionId);
            CurrentActivity = CurrentWorkflow[CurrentTask.CurrentActivity];
            if (CurrentActivity == null)
            {
                throw new Exception(string.Format("流程定义中不存在步骤名为{0}的步骤", CurrentTask.CurrentActivity));
            }
        }

        public void Run(IList<WorkflowChoice> userChoice = null)
        {
            Validate();
            var context = GetWorkflowContext();
            if (userChoice != null)
            {
                context.UserChoice = userChoice;
            }
            CurrentWorkflow.Run(context);
        }

        public void SetOpinion(string opinionContent, int opinionArea)
        {
            Validate();
            CurrentTask.OpinionContent = opinionContent;
            CurrentTask.OpinionType = opinionArea;
            Dao.Get().Update<WorkItem>(CurrentTask);
        }

        public NextStep GetStepUser(string activityName)
        {
            Validate();
            var returnValue = new NextStep();
            var activity = CurrentWorkflow[activityName];
            if (activity == null)
            {
                throw new Exception("流程中不存在步骤名为：{0} 的步骤".FormatTo(activityName));
            }
            returnValue.NeedUser = true;
            returnValue.StepName = activity.Name;
            returnValue.Label = activity.Name;
            GetNextStepUser(returnValue, activity, GetWorkflowContext());

            return returnValue;
        }

        private void GetNextStepUser(NextStep nextStep, Activity activity, WorkflowContext context)
        {
            Actor actor = activity.Actor;
            nextStep.Users = new List<NextStepUser>();
            nextStep.AllowFree = false;
            nextStep.OnlySingleSel = false;
            nextStep.AutoSelectAll = false;
            nextStep.AllowSelect = true;
            actor.Owner = activity;
            if (actor != null)
            {
                try
                {
                    var groupByActors = actor.Resolve(context).GroupBy(o => o.DepartmentId);
                    foreach (var actors in groupByActors)
                    {
                        foreach (UserEntity u in actors.OrderBy(o => o.Rank))
                        {
                            nextStep.Users.Add(new NextStepUser() { StepName = activity.Name, ID = u.UserId, Name = u.UserName, OrgId = u.DepartmentId, OrgName = u.DepartmentId, Rank = u.Rank, });
                        }
                    }
                }
                catch (Exception ex) { nextStep.Message = ex.Message; }
            }
            if (activity is EndActivity)
            {
                nextStep.NeedUser = false;
            }
        }

        public bool ShowUserSelect()
        {
            Validate();
            if (CurrentActivity is Activity)
            {
                var context = GetWorkflowContext();
                return (CurrentActivity as Activity).CanExit(context);
            }
            else
            {
                return false;
            }
        }

        private WorkflowContext GetWorkflowContext()
        {
            var context = new WorkflowContext();
            context.CurrentUser = CurrentUser;
            context.FlowInstance = FlowInstance;
            context.CurrentTask = CurrentTask;
            return context;
        }


        private void Validate()
        {
            if (CurrentWorkflow == null || CurrentTask == null || FlowInstance == null)
            {
                throw new Exception("未打开流程，或工作项不存在");
            }
        }

        public void SetReadTime()
        {
            Validate();
            var task = CurrentTask;
            if (task.ReadTime == null)
            {
                task.Status = WorkItemStatus.Readed;
                task.ReadTime = DateTime.Now;
                if (task.ItemId >= 10000)
                {
                    task.FinishTime = DateTime.Now;
                    task.Status = WorkItemStatus.Finished;
                }
                Dao.Get().Update<WorkItem>(task);
            }
        }
    }

    public class NextStep
    {
        public string MultipleSelectTag { get; set; }

        public string StepName { get; set; }

        public string Label { get; set; }

        public int Rank { get; set; }

        public string LabelDescription { get; set; }

        public bool NeedUser { get; set; }

        public bool AllowSelect { get; set; }

        public bool AllowFree { get; set; }

        public bool OnlySingleSel { get; set; }

        public bool AutoSelectAll { get; set; }

        public List<NextStepUser> Users { get; set; }

        public string Message { get; set; }
    }

    public class NextStepUser
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string OrgId { get; set; }

        public string OrgName { get; set; }

        public int Rank { get; set; }

        public int OrgRank { get; set; }

        public string StepName { get; set; }
    }
}
