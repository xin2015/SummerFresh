using SummerFresh.Business.Entity;
using SummerFresh.Data;
using SummerFresh.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    public static class DataDictionaryHelper
    {
        public static DataDictionaryItemEntity GetDictionaryItem(string dictionaryCode, string itemCode)
        {
            var dict = GetDictionary(dictionaryCode);
            if (dict != null) 
            {
                return dict.DictionaryItems.FirstOrDefault(o => o.DictionaryItemCode.Equals(itemCode, StringComparison.CurrentCultureIgnoreCase));
            }
            return null;
        }

        public static DataDictionaryEntity GetDictionary(string dictionaryCode)
        {
            string key = NamingCenter.GetCacheKey(CacheType.DATA_TABLE, "SYS_DataDictionary");
            var dic = CacheHelper.GetFromCache<IList<DataDictionaryEntity>>(key, () =>
            {
                var result = Dao.Get().SelectAll<DataDictionaryEntity>();
                return result;
            });
            return dic.FirstOrDefault(o => o.DictionaryCode == dictionaryCode);
        }
    }
}
