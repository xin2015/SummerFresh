using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class UnValidateInputeClassAttribute : Attribute
    {

    }
}
