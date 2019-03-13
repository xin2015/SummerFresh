using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Data.Attributes;
using SummerFresh.Business.Service;

namespace SummerFresh.Business.Entity
{
    [Table("VLiveDataShow",ShowCheckbox=false,ShowIndex=false)]
    [EntityService(typeof(DailyReviewEntityService))]
    public class DailyReviewEntity : CustomEntity
    {
        [DisplayName("站点")]
        public string PositionName { get; set; }

        [Category("气态污染物")]
        public string SO2 { get; set; }

        [Category("气态污染物")]
        public string NO2 { get; set; }

        [Category("气态污染物")]
        public string O3 { get; set; }

        [Category("气态污染物")]
        public string CO { get; set; }

        [Category("气态污染物")]
        public string PM10 { get; set; }

        [Category("气态污染物")]
        public string PM2_5 { get; set; }

        [Category("气象参数")]
        public string 风速 { get; set; }

        [Category("气象参数")]
        public string 风向 { get; set; }

        [Category("气象参数")]
        public string 气压 { get; set; }


        [Category("气象参数")]
        public string 气温 { get; set; }


        [Category("气象参数")]
        public string 湿度 { get; set; }

        [Category("气象参数")]
        public string 降水量 { get; set; }

        [TableField(IsShow = false)]
        [FormField(Editable = false, ControlType = ControlType.DatePicker,DefaultValue="$DateTime$")]
        [SearchField]
        [DisplayName("日期")]
        [DefaultSortField(OrderByType.ASC)]
        public string Timepoint { get; set; }
    }

    public class DailyReviewEntityService : CustomEntityService
    {
        public override IList<ButtonEntity> AddButtons()
        {
            return new List<ButtonEntity> 
                {
                    new ButtonEntity(){ ButtonType= ButtonEntityType.Toolbar, CssClass="btn",  ButtonControlId="btnPreDay", ButtonName="前一天", OnClick="SkipDay(-1)"},
                    new ButtonEntity(){ ButtonType= ButtonEntityType.Toolbar, CssClass="btn", ButtonControlId="btnNextDay", ButtonName="后一天", OnClick="SkipDay(1)"},
                };
        }
    }
}
