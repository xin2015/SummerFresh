using System.Collections.Generic;

namespace SummerFresh.Data
{
    public interface ISqlAction
    {
        string Name { get; }
        string Text { get; }
    }

    public interface ISqlActionExecutor
    {
        string Execute(ISqlAction action, ISqlParameters inParams,IDictionary<string,object> outParams);
    }

    public class NativeSqlActionExecutor : ISqlActionExecutor
    {
        public string Execute(ISqlAction action, ISqlParameters inParams, IDictionary<string, object> outParams)
        {
            string content = action.Text.Trim();
            if (string.IsNullOrEmpty(content)||content.Length <=2)
            {
                return content;
            }

            if (content.StartsWith("$")&&content.EndsWith("$"))
            {
                content = content.Trim('$');
                return inParams.Resolve(content).ToString();
            }

            return content;
        }
    }
}