using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace SummerFresh.Basic.FastReflection
{
    public class FastMethod
    {
        protected readonly Func<object, object[], object> _invoker;
        protected readonly MethodInfo _methodInfo;

        public FastMethod(MethodInfo methodInfo)
        {
            this._methodInfo = methodInfo;
            this._invoker = CreateInvoker(methodInfo);
        }

        public MethodInfo Info
        {
            get { return _methodInfo; }
        }

        public virtual object Invoke(object instance,params object[] args)
        {
            return _invoker(instance, args);
        }

        private static Func<object, object[], object> CreateInvoker(MethodInfo methodInfo)
        {
            // Target: ((TInstance)instance).Method((T0)parameters[0], (T1)parameters[1], ...)

            // parameters to execute
            var instanceParameter = Expression.Parameter(typeof(object), "instance");
            var parametersParameter = Expression.Parameter(typeof(object[]), "parameters");

            // build parameter list
            var parameterExpressions = new List<Expression>();
            var paramInfos = methodInfo.GetParameters();
            for (int i = 0; i < paramInfos.Length; i++)
            {
                // (Ti)parameters[i]
                BinaryExpression valueObj = Expression.ArrayIndex(
                    parametersParameter, Expression.Constant(i));
                UnaryExpression valueCast = Expression.Convert(
                    valueObj, paramInfos[i].ParameterType);

                parameterExpressions.Add(valueCast);
            }

            // non-instance for static method, or ((TInstance)instance)
            var instanceCast = methodInfo.IsStatic ? null :
                Expression.Convert(instanceParameter, methodInfo.ReflectedType);

            // static invoke or ((TInstance)instance).Method
            var methodCall = Expression.Call(instanceCast, methodInfo, parameterExpressions);

            // ((TInstance)instance).Method((T0)parameters[0], (T1)parameters[1], ...)
            if (methodCall.Type == typeof(void))
            {
                var lambda = Expression.Lambda<Action<object, object[]>>(
                        methodCall, instanceParameter, parametersParameter);

                Action<object, object[]> execute = lambda.Compile();
                return (instance, parameters) =>
                {
                    execute(instance, parameters);
                    return null;
                };
            }
            else
            {
                var castMethodCall = Expression.Convert(methodCall, typeof(object));
                var lambda = Expression.Lambda<Func<object, object[], object>>(
                    castMethodCall, instanceParameter, parametersParameter);

                return lambda.Compile();
            }
        }
    }
}
