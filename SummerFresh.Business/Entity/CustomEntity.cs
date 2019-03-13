using SummerFresh.Basic.FastReflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using System.ComponentModel;
using SummerFresh.Data.Attributes;
using System.Web.Mvc;
namespace SummerFresh.Business.Entity
{
    [ModelBinder(typeof(CustomEntityModelBinder))]
    public class CustomEntity
    {

    }



}
