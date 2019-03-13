using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SummerFresh.Environment.Config
{
    /// <summary>
    /// 暂时没有使用，VariableElement配置读取目前通过ExtensibleElement来实现
    /// </summary>
    [ConfigurationCollection(typeof(VariableElement), AddItemName = "variable")]
    public class VariableElementCollection : ConfigurationElementCollection
    {
        public void Add(VariableElement element)
        {
            BaseAdd(element);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new VariableElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((VariableElement)element).Name;
        }
    }
}