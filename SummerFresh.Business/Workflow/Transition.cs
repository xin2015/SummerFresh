using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business.Workflow
{
    public class Transition
    {
        public Workflow Owner
        {
            get;
            set;
        }

        public Condition Condition
        {
            get;
            set;
        }

        public string Label
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public Activity To
        {
            get;
            set;
        }

        public Activity From
        {
            get;
            set;
        }

        public int Rank
        {
            get;
            set;
        }

        public bool Resolve(WorkflowContext context)
        {
            var result = true;
            if (Condition != null)
            {
                result = Condition.Resolve(context);
                if (NotOperation)
                {
                    result = !result;
                }
            }
            return result;
        }


        public bool PreResolve(WorkflowContext context)
        {
            var result = true;
            if (Condition != null)
            {
                result = Condition.PreResolve(context);
                if (NotOperation)
                {
                    result = !result;
                }
            }
            return result;
        }


        public bool CanPushBack
        {
            get;
            set;
        }

        public bool NotOperation
        {
            get;
            set;
        }
    }
}
