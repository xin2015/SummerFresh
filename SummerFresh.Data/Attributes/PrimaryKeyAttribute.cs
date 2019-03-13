using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PrimaryKeyAttribute : Attribute
    {

    }
}
