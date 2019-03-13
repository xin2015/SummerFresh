using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace SummerFresh.Business
{
    /// <summary>
    /// 类型数据源
    /// </summary>
    [DisplayName("类型数据源")]
    public class TypeDataSource:KeyValueDataSourceBase
    {
        public Type BaseType { get; set; }

        public override IList<SelectListItem> SelectItems()
        {
            var types = TypeHelper.GetAllSubType(BaseType);
            var result = new List<SelectListItem>();
            foreach(var type in types)
            {
                result.Add(new SelectListItem() { Text = type.FullName, Value = type.FullName });
            }
            return result;
        }
    }
}
