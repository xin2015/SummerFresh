using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    public interface IChildren
    {
        IList<IControl> Controls { get; set; }

        void AddChildren(string property, object component);
    }
}
