using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Data.Attributes;

namespace SummerFresh.Business.Entity
{
    [Table("StationType")]
    public class StationTypeEntity:CustomEntity
    {
        [PrimaryKey]
        [DisplayName("编号")]
        public virtual string StationTypeId
        {
            get;
            set;
        }

        [TitleField]
        [DisplayName("站点类型名称")]
        public virtual string StationTypeName
        {
            get;
            set;
        }

        [DisplayName("描述")]
        public virtual string Description
        {
            get;
            set;
        }
    }
}
