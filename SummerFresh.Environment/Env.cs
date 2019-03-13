using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SummerFresh.Util;

namespace SummerFresh.Environment
{
    public static class Env
    {
        private static readonly ILog log = LogManager.GetCurrentClassLogger();

        public static IEnvironmentContainer Container
        {
            get { return EnvironmentFactory.Container; }
        }

        public static string Parse(string expression)
        {
            return Parse(expression,null);
        }

        public static string Parse(string expression, bool? isArrayQuoted = false,char quoteChar = '\'')
        {
            return EnvironmentFactory.Parser.Parse(expression);
        }

        public static string Variable(string name)
        {
            return Variable(name,null);
        }

        public static string Variable(string name,bool? isArrayQuoted = false,char quoteChar = '\'')
        {
            return StringConverter.From(Resolve(name),isArrayQuoted,quoteChar);
        }

        public static object Resolve(string name)
        {
            object value;
            TryResolve(name, out value);
            return value;
        }

        public static bool TryResolve(string name, out object value)
        {
            return Container.TryResolve(name, out value);
        }
    }
}