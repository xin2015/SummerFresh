using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Security.Rule
{
    public class SecurityRule
    {
        /// <summary>
        /// 数字越低，优先级越高
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 规则文本
        /// </summary>
        public string Text { get; set; }
    }
}