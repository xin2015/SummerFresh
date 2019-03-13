using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Environment.Config;

namespace SummerFresh.Environment
{
    /// <summary>
    /// 环境提供者，提供环境变量的查询与处理接口
    /// </summary>
    public interface IEnvironmentContainer
    {
        EnvironmentSection Configuration { get;}

        /// <summary>
        /// 向容器直接注册一个全局的环境变量
        /// </summary>
        /// <param name="name">环境变量名称</param>
        /// <param name="value">环境变量的值</param>
        /// <remarks>
        /// 如果已经存在相同名称的环境变量，则替换已有的环境变量
        /// </remarks>
        void Register(string name, object value);

        /// <summary>
        /// 向容器直接注册一个环境变量
        /// </summary>
        /// <param name="name">环境变量名称</param>
        /// <param name="variable">环境变量对象</param>
        /// <remarks>
        /// 如果已经存在相同名称的环境变量，则替换已有的环境变量
        /// </remarks>
        void Register(string name,IEnvironmentVariable variable);

        /// <summary>
        /// 根据名称解析出环境变量的值
        /// </summary>
        /// <param name="name">环境变量的名称</param>
        /// <returns>环境变量的值，如果环境变量不存在或者值是<c>null</c>则返回<c>null</c></returns>
        /// <remarks>
        /// 环境变量的值有可能从缓存中获取，具体根据环境变量的缓存范围而定
        /// </remarks>
        object Resolve(string name);

        /// <summary>
        /// 根据名称解析出环境变量的值
        /// </summary>
        /// <param name="name">环境变量的名称</param>
        /// <param name="value">环境变量的值，有可能是<c>null</c></param>
        /// <returns>是否存在环境变量</returns>
        /// <remarks>
        /// 环境变量的值有可能从缓存中获取，具体根据环境变量的缓存范围而定
        /// </remarks>
        bool TryResolve(string name, out object value);
    }
}