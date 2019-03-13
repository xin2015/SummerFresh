using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SummerFresh.Environment.Config;

namespace SummerFresh.Environment
{
    public interface IEnvironmentProvider
    {
        /// <summary>
        /// 环境变量的前缀，相当于命名空间，初始化时被设置为配置文件中prefix属性的值
        /// </summary>
        string Prefix { get; set; }

        /// <summary>
        /// 传入配置文件中关联此Provider的Xml节点作为<see cref="SummerFresh.Environment.Config.ProviderElement">ProviderElement</see>对象，由Provider进行初始化配置.
        /// <para></para>
        /// <para><code lang="C#">ProviderElement</code>中的配置架构(Schema)由实现Provider决定。</para>
        /// </summary>
        /// <param name="container"></param>
        /// <param name="providerElement">配置文件中关联此Provider的的XML节点。</param>
        /// <returns>返回是否注册到container中，返回false意味着不进行注册且GetVariable方法不会被container调用</returns>
        bool Configure(IEnvironmentContainer container,ProviderElement providerElement);

        /// <summary>
        /// 获取环境变量对象
        /// </summary>
        /// <param name="name">环境变量的名称</param>
        /// <returns>不存在返回null</returns>
        IEnvironmentVariable GetVariable(string name);
    }
}