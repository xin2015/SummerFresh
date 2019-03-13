using SummerFresh.Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    public interface IAutoGenerate
    {
        IControl Generate(IList<DataFieldEntity> fields, string moduleName);
    }
}
