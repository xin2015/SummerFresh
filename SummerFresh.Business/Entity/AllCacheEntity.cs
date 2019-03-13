using SummerFresh.Business.Service;
using SummerFresh.Data.Attributes;
using SummerFresh.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SummerFresh.Business.Entity
{
    [EntityService(typeof(AllCacheEntityService))]
    public class AllCacheEntity : CustomEntity
    {
        [DisplayName("缓存键")]
        [PrimaryKey]
        public string Key { get; set; }

        [FormField(ControlType = ControlType.CheckLabelList)]
        [EnumDataSource(typeof(CacheType))]
        [DisplayName("缓存键")]
        [SearchField]
        [TableField(IsShow = false)]
        public string SearchKey { get; set; }

        [DisplayName("缓存值")]
        [TableField(ShowLength = 50)]
        public string Value { get; set; }
    }

    public class AllCacheEntityService : CustomEntityService
    {
        public override IListDataSource DataSource
        {
            get
            {
                return new AllCacheKeyDataSource();
            }
        }

        public override IList<ButtonEntity> AddButtons()
        {
            var buttons = base.AddButtons();
            buttons.Remove(buttons.FirstOrDefault(o => o.ButtonControlId == "btnInsert"));
            buttons.Remove(buttons.FirstOrDefault(o => o.ButtonControlId == "btnExport"));
            buttons.Remove(buttons.FirstOrDefault(o => o.ButtonControlId == "btnEdit"));
            return buttons;
        }

        public override int Delete(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            string key = entity.ToString();
            if (CacheHelper.GetValue(key) != null)
            {
                return CacheHelper.Remove(key) ? 1 : 0;
            }
            return 0;
        }
    }
}
