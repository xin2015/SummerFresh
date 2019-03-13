using System;
using System.Collections.Generic;

namespace SummerFresh.Environment
{
    public class HttpContextParameters : AbstractHttpContextParameters
    {

        private static readonly List<AbstractHttpContextParameters> parametersList = new List<AbstractHttpContextParameters>();

        static HttpContextParameters()
        {
            parametersList.Add(new FormParameters());
            parametersList.Add(new QueryStringParameters());
        }

        protected override bool IsSupport(string name)
        {
            return true;
        }

        public override object Evaluate()
        {
            Object value = null;
            foreach (var parameter in parametersList)
            {
                parameter.TryResolve(Name, out value);
                if (value != null)
                {
                    return value;
                }
            }

            return value;
        }
    }
}
