using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using System.ComponentModel;

namespace SummerFresh.Controls
{
    [DisplayName("图标按钮")]
    public class IconTableButton : TableButton
    {
        [DisplayName("图标")]
        public string IconClass { get; set; }

        public override string RenderInner()
        {
            if (IconClass.IsNullOrEmpty())
            {
                return base.RenderInner();
            }
            return "<i class=\"{0}\"></i> {1}".FormatTo(IconClass, base.RenderInner());
        }
    }
}
