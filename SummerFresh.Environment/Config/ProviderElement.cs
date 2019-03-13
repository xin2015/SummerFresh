using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace SummerFresh.Environment.Config
{
    public class ProviderElement : ExtensibleElement
    {
        public const string TypeNameProperty = "type";
        public const string PrefixProperty   = "prefix";
        public const string ElementName      = "provider";

        private readonly string _key = Guid.NewGuid().ToString();

        internal string Key
        {
            get { return _key; }
        }

        [ConfigurationProperty(PrefixProperty, IsRequired = false)]
        public string Prefix
        {
            get { return (string) this[PrefixProperty]; }
            set { this[PrefixProperty] = value; }
        }

        [ConfigurationProperty(TypeNameProperty, IsRequired = true)]
        public string TypeName
        {
            get { return (string)this[TypeNameProperty]; }
            set { this[TypeNameProperty] = value; }
        }
    }
}