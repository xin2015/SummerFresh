using SummerFresh.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    public interface IAuthorityComponent
    {
        void Authority(IDictionary<string, UISecurityBehaviour> behaviour);
    }
}
