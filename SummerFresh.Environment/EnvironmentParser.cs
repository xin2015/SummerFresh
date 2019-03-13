using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SummerFresh.Environment
{
    public class EnvironmentParser : IEnvironmentParser
    {
        public static readonly Regex Pattern = new Regex(@"\$(?<Variable>.+?)\$",RegexOptions.Compiled);

        public bool HasVariable(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                return false;
            }
            else
            {
                return Pattern.Matches(expression).Count > 0;
            }
        }

        public string Parse(string expression, bool? isArrayQuoted = null, char quoteChar = '\'')
        {
            if (string.IsNullOrEmpty(expression))
            {
                return expression;
            }

            MatchCollection matches = Pattern.Matches(expression);

            if (matches.Count == 0)
            {
                return expression;
            }

            StringBuilder builder = new StringBuilder();

            int lastIndex = 0;
            foreach (Match match in matches)
            {
                if (match.Index > lastIndex)
                {
                    builder.Append(expression.Substring(lastIndex, match.Index - lastIndex));
                }

                lastIndex = match.Index + match.Length;

                String variable = match.Groups["Variable"].Value;
                object value;
                string defaultValue = null;

                int index = variable.IndexOf('?');
                if (index > 0)
                {
                    defaultValue = variable.Length > index + 1 ? variable.Substring(index + 1) : null;
                    variable = variable.Substring(0, index);
                }

                string content = null;
                if (Env.TryResolve(variable, out value))
                {
                    content = StringConverter.From(value, isArrayQuoted, quoteChar);
                }

                if (string.IsNullOrEmpty(content) && !string.IsNullOrEmpty(defaultValue))
                {
                    builder.Append(defaultValue);
                }
                else
                {
                    builder.Append(content);
                }
            }

            if (lastIndex < expression.Length)
            {
                builder.Append(expression.Substring(lastIndex));
            }

            return builder.ToString();
        }
    }

    internal static class StringConverter
    {
        public static string From(object value)
        {
            return From(value,false);
        }

        public static string From(object value,bool? quoted = true,char quoteChar = '\'')
        {
            if (null == value)
            {
                return String.Empty;
            }
            else if (value is string)
            {
                return (string)value;
            }
            else if (value is IEnumerable)
            {
                StringBuilder builder = new StringBuilder();
                foreach (object single in ((IEnumerable)value))
                {
                    if (!quoted.HasValue)
                    {
                        quoted = IsQuoted(single.GetType());
                    }

                    builder.Append(",");

                    if (quoted.Value)
                    {
                        builder.Append(quoteChar);
                    }

                    builder.Append(From(single));

                    if (quoted.Value)
                    {
                        builder.Append(quoteChar);
                    }
                }
                return builder.Length > 0 ? builder.Remove(0, 1).ToString() : string.Empty;
            }
            else
            {
                return value.ToString();
            }
        }

        private static bool IsQuoted(Type type)
        {
            //非数字，布尔值之外的都需要加单引号
            return typeof(char).Equals(type) || !type.IsPrimitive;
        }
    }
}