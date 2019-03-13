using SummerFresh.Util;
using SummerFresh.Data;

namespace SummerFresh.Environment
{
    /// <summary>
    /// 为Dao的SQL语句提供获取环境变量作为SQL参数的功能
    /// </summary>
    public class EnvironmentParameters : ISqlParameters
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public const string Prefix = "Env:";

        public object Resolve(string name)
        {
            object value;
            return TryResolve(name, out value) ? value : null;
        }

        public bool TryResolve(string name, out object value)
        {
            if (name.ToUpper().StartsWith(Prefix.ToUpper()))
            {
                string varName = name.Substring(4);
                Log.Debug("Resolving Parmeter '{0}' As Variable", varName);

                return Env.TryResolve(varName, out value);
            }
            value = null;
            return false;
        }
    }
}