using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Util;
using SummerFresh.Basic;

namespace SummerFresh.Business
{
    public class ValidatorAttribute:Attribute
    {
        public string ValidateString { get; set; }

        public ValidatorAttribute(string validateString)
        {
            ValidateString = validateString;
        }

    }
}
