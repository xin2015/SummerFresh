using SummerFresh.Data;
using System;

namespace SummerFresh.Environment
{
    /// <summary>
    /// 向Dao注册环境变量参数，可以在SQL中使用Http上下文的所有参数作为变量#Form:Name#,#QueryString:Name#,#Control:ControlID#
    /// </summary>
    public abstract class AbstractHttpContextParameters : ISqlParameters,IEnvironmentVariable
    {
        public object Resolve(string name)
        {
            if (IsSupport(name))
            {
                object value;
                return TryResolve(name, out value) ? value : null;
            }

            return null;
        }

        public bool TryResolve(string name, out object value)
        {
            if (IsSupport(name))
            {
                Name = name;
                value = Evaluate();
                return value != null;
            }

            value = null;
            return false;
        }

        protected abstract bool IsSupport(string name);

        public string Name
        {
            get;
            set;
        }

        public Scope Scope
        {
            get { return Scope.None; }
            set { }
        }

        public abstract object Evaluate();
    }
}
