using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace SummerFresh.Business
{
    [ModelBinder(typeof(CustomEntityModelBinder))]
    public interface IControl
    {
        string ID { get; set; }
    }
}
