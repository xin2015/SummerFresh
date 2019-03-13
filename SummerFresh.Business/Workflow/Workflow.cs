using SummerFresh.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace SummerFresh.Business.Workflow
{
    public class Workflow
    {
        public Workflow()
        {
            Activities = new List<Activity>();
            Transitions = new List<Transition>();
        }
        public virtual Activity Root
        {
            get;
            set;
        }

        public virtual IList<Activity> Activities
        {
            get;
            set;
        }

        public virtual IList<Transition> Transitions
        {
            get;
            set;
        }

        public virtual Activity this[string ActivityName]
        {
            get
            {
                foreach (var activity in Activities)
                {
                    if (activity.Name.Equals(ActivityName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return activity;
                    }
                }
                return null;
            }
        }

        public virtual int Id { get; set; }

        public virtual string Name
        {
            get;
            set;
        }

        public Activity AddActivity(string name)
        {
            return AddActivity(name, string.Empty, string.Empty);
        }

        public Activity AddActivity(string name, string deptName, string roleName)
        {
            var activity = new Activity() { Name = name, NeedChoice = true };
            activity.Actor = new Actor() { DeptName = deptName, RoleName = roleName };
            activity.Owner = this;
            Activities.Add(activity);
            return activity;
        }

        public Activity AddEndActivity()
        {
            var activity = new EndActivity() { Name = "结束", Owner = this };
            Activities.Add(activity);
            return activity;
        }

        public void Connect(Activity from, Activity to)
        {
            var tran = new Transition();
            tran.To = to;
            tran.From = from;
            tran.CanPushBack = true;
            tran.Label = to.Name;
            tran.Condition = new Condition() { Choice = to.Name };
            to.Owner.Transitions.Add(tran);
        }

        public virtual void Run(WorkflowContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("流程运行需要上下文");
            }
            if (context.FlowInstance.FlowTag > FlowStatus.Finished)
            {
                throw new Exception(string.Format("目前流程处于{0}状态，无法继续流转。", context.FlowInstance.FlowTag.ToString()));
            }
            if (context.CurrentTask.Status >= WorkItemStatus.Finished)
            {
                throw new Exception("当前工作项状态不允许运行");
            }
            using (TransactionScope tran = new TransactionScope())
            {
                if (context.FlowInstance.FlowTag == FlowStatus.Begin)
                {
                    context.FlowInstance.FlowTag = FlowStatus.Running;
                    Dao.Get().Update<FlowInstance>(context.FlowInstance);
                }
                InnerRun(this[context.CurrentTask.CurrentActivity], context);
                tran.Complete();
            }
        }

        protected virtual void InnerRun(Activity activity, WorkflowContext context)
        {
            if (!activity.CanExecute(context))
            {
                return;
            }
            activity.Execute(context);
            if (activity.CanExit(context))
            {
                return;
            }
            activity.Exit(context);
            foreach (var tran in activity.Transitions)
            {
                if (tran.Resolve(context))
                {
                    if (!tran.To.CanEnter(context))
                    {
                        continue;
                    }
                    tran.To.Enter(context);
                    if (tran.To.AutoRun)
                    {
                        InnerRun(tran.To, context);
                    }
                }
            }
        }
    }
}
