using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    public class TreeAttribute:Attribute
    {
        public string IdParameterName { get; set; }

        public string SqlId { get; set; }
    }
}
