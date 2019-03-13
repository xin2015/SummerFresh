using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
namespace SummerFresh.Business
{
    [DisplayName("所在枚举类")]
    public class AllEnumDataSource:KeyValueDataSourceBase
    {
        public override IList<SelectListItem> SelectItems()
        {
            var result = new List<SelectListItem>();
            var types = TypeHelper.GetAllEnum();
            foreach(var type in types)
            {
                result.Add(new SelectListItem()
                {
                    Text = type.FullName,
                    Value = type.FullName
                });
            }
            return result;
        }
    }
}
