using System;

namespace SummerFresh.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct,AllowMultiple = false,Inherited = true)]
    public class TableAttribute : NamedAttribute
    {
        public TableAttribute(string name) : base(name)
        {
            AllowPaging = true;
            AutoGenerateColum = false;
            ShowIndex = true;
            ShowCheckbox = true;
        }

        public bool ShowIndex { get; set; }

        public bool ShowCheckbox { get; set; }

        public bool AllowPaging { get; set; }

        public bool AutoGenerateColum { get; set; }
    }
}