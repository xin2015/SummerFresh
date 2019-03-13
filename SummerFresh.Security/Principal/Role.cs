using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Security.Principal
{
    [Serializable]
    public class Role : IRole
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}