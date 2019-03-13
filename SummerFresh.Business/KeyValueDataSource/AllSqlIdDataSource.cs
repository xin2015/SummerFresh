using SummerFresh.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace SummerFresh.Business
{
    [DisplayName("所有SqlId语句")]
    public class AllSqlIdDataSource : KeyValueDataSourceBase
    {
        public override IList<SelectListItem> SelectItems()
        {
            var result = new List<SelectListItem>();
            string key = string.Empty;
            foreach (var d in DaoFactory.GetSqlSource().Sqls)
            {
                key = d.Key.Trim();
                if (key.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase)
                    || key.StartsWith("INSERT", StringComparison.OrdinalIgnoreCase)
                    || key.StartsWith("UPDATE", StringComparison.OrdinalIgnoreCase)
                    || key.StartsWith("DELETE", StringComparison.OrdinalIgnoreCase)
                    || key.StartsWith("EXEC", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                result.Add(new SelectListItem()
                {
                    Text = key,
                    Value = key
                });

            } 
            return result;
        }
    }
}
