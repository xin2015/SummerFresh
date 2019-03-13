using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using SummerFresh.Environment.Variable;

namespace SummerFresh.Environment.Config
{
    [ConfigurationCollection(typeof(FactoryElement), AddItemName = FactoryElement.ElementName)]
    public class FactoryElementCollection : ConfigurationElementCollection
    {
        public void Add(FactoryElement element)
        {
            BaseAdd(element);
        }

        public new FactoryElement this[string name]
        {
            get { return (FactoryElement) base.BaseGet(name); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new FactoryElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FactoryElement) element).Name;
        }
    }
}
