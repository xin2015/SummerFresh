using SummerFresh.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SummerFresh.Basic;
using System.ComponentModel;
namespace SummerFresh.Business
{
    [DisplayName("所有缓存键值")]
    public class AllCacheKeyDataSource : ListDataSourceBase
    {
        public override IList<IDictionary<string, object>> GetList()
        {
            var returnValue = new List<IDictionary<string, object>>();
            string queryString = HttpContext.Current.Request["SearchKey"];
            foreach (var key in CacheHelper.AllKeys)
            {
                if (queryString.IsNullOrEmpty() || key.ToLower().IndexOf(queryString.ToLower()) >= 0)
                {
                    var dict = new Dictionary<string, object>();
                    dict["Key"] = key;
                    dict["Value"] = CacheHelper.GetValue(key).ToString();
                    returnValue.Add(dict);
                }
            }
            return returnValue;
        }
    }
}
