using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    public class TabItemAttribute:Attribute
    {
        public TabItemAttribute()
        {

        }

        public TabItemAttribute(string tabItemName)
        {
            TabItemName = tabItemName;
        }
        public string TabItemName { get; set; }
    }
}
