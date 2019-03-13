using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business.Workflow
{
    public class Condition
    {

        public Transition Owner
        {
            get;
            set;
        }

        public string Choice
        {
            get;
            set;
        }

        public bool NotOperation
        {
            get;
            set;
        }

        public bool PreResolve(WorkflowContext context)
        {
            return true;
        }

        public bool Resolve(WorkflowContext context)
        {
            bool result = false;
            if (string.IsNullOrEmpty(Choice))
            {
                result = true;
            }
            else
            {
                if (context.UserChoice != null && context.UserChoice.Count > 0)
                {
                    if (Choice.IndexOf(',') > 0)
                    {
                        var choices = Choice.Split(',');
                        foreach (var ch in choices)
                        {
                            foreach (var uc in context.UserChoice)
                            {
                                if (uc.Choice.Equals(ch, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    result = true;
                                    break;
                                }
                            }
                            if (result)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (var uc in context.UserChoice)
                        {
                            if (uc.Choice.Equals(Choice, StringComparison.CurrentCultureIgnoreCase))
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
            }
            if (NotOperation)
            {
                result = !result;
            }
            return result;
        }
    }
}
