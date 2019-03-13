using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    public interface ITargetId
    {
        string TargetId { get; set; }

        void SetTarget(IList<IControl> components);
    }
}
