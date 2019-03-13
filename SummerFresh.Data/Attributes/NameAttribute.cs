using System;

namespace SummerFresh.Data.Attributes
{
    public abstract class NameAttribute : Attribute
    {
        protected NameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}