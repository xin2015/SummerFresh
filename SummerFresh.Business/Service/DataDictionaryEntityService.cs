using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business.Service
{
    public class DataDictionaryEntityService:CustomEntityService
    {
        public override IList<ButtonEntity> AddButtons()
        {
            var buttons =  base.AddButtons();
            buttons.Add(new ButtonEntity() 
            { 
                ButtonControlId = "btnEditItems", 
                ButtonName = "字典项", 
                CssClass = "btn btn-warning btn-sm",
                OnClick = "$.showModalDialog({{ url: 'Entity/List/DataDictionaryItem?DictionaryId={0}', overlayClose: true,title:'数据字典【{1}】的字典项', width: 700, height: 500 }});",
                ButtonType= ButtonEntityType.TableRow,
                DataFields="DictionaryId,DictionaryName"
            });
            return buttons;
        }
    }
}
