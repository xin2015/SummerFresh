using SummerFresh.Data;
using SummerFresh.Data.Attributes;
using SummerFresh.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SummerFresh.Business.Entity
{
    [DisplayName("数据字典")]
    [Table("SYS_DataDictionary")]
    public class DataDictionaryEntity : CustomEntity
    {
        [PrimaryKey]
        [TableField(IsShow = false)]
        public string DictionaryId { get; set; }

        [DisplayName("数据字典编码")]
        public string DictionaryCode { get; set; }

        [TitleField]
        [DisplayName("数据字典名称")]
        [SearchField]
        public string DictionaryName { get; set; }

        [DisplayName("数据字典描述")]
        [TableField(ShowLength = 10)]
        [FormField(ControlType = ControlType.TextArea)]
        public string Description { get; set; }

        [FormField(Editable = false)]
        [TableField(IsShow = false)]
        public IList<DataDictionaryItemEntity> DictionaryItems
        {
            get
            {
                string key = NamingCenter.GetCacheKey(CacheType.DATA_TABLE, "SYS_DataDictionaryItems");
                var result = CacheHelper.GetFromCache<IList<DataDictionaryItemEntity>>(key, () =>
                {
                    return Dao.Get().SelectAll<DataDictionaryItemEntity>();
                });
                return result.Where(o => o.DictionaryId == DictionaryId).OrderBy(o => o.Rank).ToList();
            }
        }
    }
}