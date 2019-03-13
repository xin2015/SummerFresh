using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Basic;

namespace SummerFresh.Controls
{
    [DisplayName("图标按钮")]
    public class IconButton : Button
    {
        [DisplayName("图标")]
        public string IconClass { get; set; }

        protected override string TagName
        {
            get
            {
                return "Button";
            }
        }

        protected override string RenderInner()
        {
            if (IconClass.IsNullOrEmpty())
            {
                return Value;
            }
            return "<i class=\"{0}\"></i> {1}".FormatTo(IconClass, Value);
        }

        protected override bool SelfClosing
        {
            get
            {
                return false;
            }
        }
    }
}
