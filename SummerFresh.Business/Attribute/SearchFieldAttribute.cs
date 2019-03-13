using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    public class SearchFieldAttribute:Attribute
    {
        public SearchFieldAttribute()
        {
            IsSearchControl = true;
        }

        public int Rank { get; set; }

        public bool IsSearchControl { get; set; }
    }
}
