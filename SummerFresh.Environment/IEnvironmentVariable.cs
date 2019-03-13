using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Environment
{
    /// <summary>
    /// 表示变量值在首次计算后不再重新计算的周期
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item>
    /// <description>Application : 表示在整个应用的运行期间都不需要重新计算</description></item>
    /// <item>
    /// <description>Session     : 表示在整个用户会话没有结束前不需要重新计算</description></item>
    /// <item>
    /// <description>Request     : 表示在整个用户当前请求结束前不需要重新计算</description></item>
    /// <item>
    /// <description>None        : 表示没有生存周期，每次获取变量值都重新计算</description></item>
    /// </list>
    /// </remarks>
    public enum Scope
    {
        Application,
        Session,
        Request,
        None
    };

    /// <summary>
    /// 表示一个环境变量
    /// </summary>
    public interface IEnvironmentVariable
    {
        /// <summary>
        ///  变量名
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 缓存范围，缺省是<see cref="SummerFresh.Environment.Scope.Session">session</see>
        /// </summary>
        Scope Scope { get; set; }

        /// <summary>
        /// 重新计算变量的值，计算逻辑由实现类确定
        /// </summary>
        /// <returns>变量的值</returns>
        object Evaluate();
    }
}