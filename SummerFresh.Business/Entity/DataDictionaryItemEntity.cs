
using SummerFresh.Business.Service;
using SummerFresh.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SummerFresh.Business.Entity
{
    [DisplayName("数据字典项")]
    [Table("SYS_DataDictionaryItems",ShowIndex=false)]
    [EntityService(typeof(DataDictionaryItemEntityService))]
    public class DataDictionaryItemEntity : CustomEntity
    {
        
        public DataDictionaryItemEntity()
        {
            Status = true;
        }

        [PrimaryKey]
        [TableField(IsShow = false)]
        public string DictionaryItemId { get; set; }

        [DisplayName("数据字典项编码")]
        public string DictionaryItemCode { get; set; }

        [DisplayName("数据字典项文本")]
        public string DictionaryItemText { get; set; }

        [SearchField]
        [DisplayName("所属数据字典")]
        [CustomEntityDataSource(typeof(DataDictionaryEntity))]
        [FormField(ControlType = ControlType.DropDownList)]
        public string DictionaryId { get; set; }

        [DefaultSortField(Business.OrderByType.ASC)]
        [DisplayName("排序号")]
        public int Rank { get; set; }

        [DisplayName("状态")]
        public bool Status { get; set; }
    }

    public class DataDictionaryItemEntityService:CustomEntityService
    {

    }
}
