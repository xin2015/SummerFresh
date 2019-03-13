using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Environment
{
    public interface IEnvironmentParser
    {
        /// <summary>
        /// 判断表达式中是否包含环境变量
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>存在1个或1个以上返回true，否则返回false</returns>
        bool HasVariable(string expression);

        /// <summary>
        /// 解析表达式并把其中的环境变量替换为相应的内容
        /// </summary>
        /// <param name="expression">原始表达式</param>
        /// <param name="isArrayQuoted">
        /// 如果环境变量是一个数组则需要转换为逗号分隔的字符串，此参数表示是否需要在每个值的两边加上引号
        /// </param>
        /// <param name="quoteChar">引号字符，默认为单引号</param>
        /// <returns>替换后的字符串</returns>
        string Parse(string expression, bool? isArrayQuoted = null,char quoteChar = '\'');
    }
}