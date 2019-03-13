using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SummerFresh.Environment.Config
{
    [ConfigurationCollection(typeof(ProviderElement),AddItemName = ProviderElement.ElementName)]
    public class ProviderElementCollection : ConfigurationElementCollection
    {
        public void Add(ProviderElement element)
        {
            BaseAdd(element);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ProviderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ProviderElement) element).Key;
        }
    }
}