using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Controls
{
    class ChartOptionAttribute : Attribute
    {
        public string Name { get; set; }

        public ChartOptionAttribute()
        {

        }

        public ChartOptionAttribute(string Name)
        {
            this.Name = Name;
        }
    }
}
