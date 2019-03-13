using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using System.Web.Mvc;
using System.ComponentModel;
namespace SummerFresh.Business
{
    [DisplayName("分隔字符数据源")]
    public class StringSplitDataSource : KeyValueDataSourceBase
    {
        [Validator("required")]
        [Description("可以用逗号，竖线，分号等分隔")]
        public string SplitString { get; set; }

        public override IList<SelectListItem> SelectItems()
        {
            var result = new List<SelectListItem>();
            if (!SplitString.IsNullOrEmpty())
            {
                var items = SplitString.Split(new char[] { ',', '|', ';' });
                items.ForEach(o =>
                {
                    result.Add(new SelectListItem()
                    {
                        Text = o,
                        Value = o
                    });
                });
            }
            return result;
        }
    }
}
