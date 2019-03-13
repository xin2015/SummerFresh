using SummerFresh.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using System.Web;
using System.Text.RegularExpressions;

namespace SummerFresh.Controls
{
    public class Literal : PageControlBase
    {
        private string _html;

        [FormField(ControlType = ControlType.TextArea)]
        public string Html
        {
            get
            {
                return _html;
            }
            set
            {
                if (Regex.IsMatch(value, "<.+?>"))
                {
                    _html = HttpUtility.HtmlEncode(value);
                }
                else
                {
                    _html = value;
                }
            }
        }

        public override string RenderContent()
        {
            return HttpUtility.HtmlDecode(Html);
        }
    }
}
