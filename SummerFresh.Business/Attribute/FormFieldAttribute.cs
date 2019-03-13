using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    [AttributeUsage(AttributeTargets.Property,Inherited=true)]
    public class FormFieldAttribute : Attribute
    {
        public FormFieldAttribute()
        {
            ControlType = ControlType.None;
            Editable = true;
        }
        public string FormDisplayName { get; set; }

        public ControlType ControlType { get; set; }

        public bool Editable { get; set; }

        public object DefaultValue { get; set; }

        public string ExtendField { get; set; }
    }
}
