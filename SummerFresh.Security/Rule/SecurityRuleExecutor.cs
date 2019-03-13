using System.Collections.Generic;
using SummerFresh.Data;

namespace SummerFresh.Security.Rule
{
    /// <summary>
    /// 数据库查询权限规则
    /// </summary>
    public class SecurityRuleExecutor : ISqlActionExecutor
    {

        /*
         规则：@security{text}, text -> operation?defaultRule

         operation   : 唯一表示一个权限的操作代码
         defaultRule : 可选，表示当此操作没有指定规则或者没有此操作权限是返回的规则表达式
         
         */
        public string Execute(ISqlAction action, 
                              ISqlParameters inParams, 
                              IDictionary<string, object> outParams)
        {
            string text = action.Text;

            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            else
            {
                string operation;
                string defaultRule;

                int index = text.IndexOf('?');
                if (index > 0)
                {
                    operation = text.Substring(0, index);
                    defaultRule = text.Substring(index + 1);
                }
                else
                {
                    operation   = text;
                    defaultRule = string.Empty;
                }

                string rule = GetPermissionRule(operation);

                return string.IsNullOrEmpty(rule) ? defaultRule : rule;
            }
        }

        protected virtual string GetPermissionRule(string operation)
        {
            return SecurityContext.Provider.GetPermissionRule(operation);
        } 
    }
}
