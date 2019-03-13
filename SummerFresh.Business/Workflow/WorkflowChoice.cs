using SummerFresh.Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business.Workflow
{
    public class WorkflowChoice
    {
        /// <summary>
        /// 选择的步骤，多个步骤用逗号隔开
        /// </summary>
       public string Choice { get; set; }

        /// <summary>
        /// 选择的参与者
        /// </summary>
       public IList<UserEntity> Participant { get; set; }
    }
}
