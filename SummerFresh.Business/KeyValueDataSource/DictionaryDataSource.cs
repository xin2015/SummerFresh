using SummerFresh.Business.Entity;
using SummerFresh.Data;
using SummerFresh.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SummerFresh.Basic;
using System.ComponentModel;
namespace SummerFresh.Business
{
    /// <summary>
    /// 数据字典数据源
    /// </summary>
    [DisplayName("数据字典数据源")]
    public class DictionaryDataSource : KeyValueDataSourceBase
    {
        [DisplayName("字典编号")]
        [DataTableDataSource(TableName = "SYS_DataDictionary", DataTextField = "DictionaryName", DataValueField = "DictionaryCode")]
        [FormField(ControlType = ControlType.DropDownList)]
        public string DictionaryCode { get; set; }
        public override IList<SelectListItem> SelectItems()
        {
            var returnValue = new List<SelectListItem>();
            var dic = DataDictionaryHelper.GetDictionary(DictionaryCode);
            if (dic != null)
            {
                foreach (var d in dic.DictionaryItems)
                {
                    returnValue.Add(new SelectListItem() { Text = d.DictionaryItemText, Value = d.DictionaryItemCode });
                }
                return returnValue;
            }
            throw new ArgumentOutOfRangeException("不存在DictionaryCode为{0}的字典".FormatTo(DictionaryCode));
        }
    }
}
