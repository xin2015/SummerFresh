using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    public interface IAttributeAccessor
    {
        IDictionary<string, string> Attributes { get;}
    }
}
