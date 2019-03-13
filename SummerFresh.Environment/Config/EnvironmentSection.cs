using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using SummerFresh.Environment.Variable;

namespace SummerFresh.Environment.Config
{
    public class EnvironmentSection : ConfigurationSection
    {
        public const string SectionName    = "environment";
        public const string XmlNamespace   = "http://schemas.SummerFresh.net/netframework/environment";

        [ConfigurationProperty("xmlns",DefaultValue = XmlNamespace,IsRequired = false)]
        public string Xmlns
        {
            get { return (string)this["xmlns"]; }
            set { this["xmlns"] = value; }
        }

        [ConfigurationProperty("",IsDefaultCollection = true)]
        public ProviderElementCollection Providers 
        { 
            get { return (ProviderElementCollection) base[""]; }
        }

        [ConfigurationProperty("factories", IsDefaultCollection = false)]
        public FactoryElementCollection Factories
        {
            get { return (FactoryElementCollection)base["factories"]; }
        }

        public IVariableFactory GetFactory(string name)
        {
            FactoryElement element = Factories[name];

            return null == element ? null : element.Instance;
        }

        protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
        {
            if (FactoryElement.ElementName.Equals(elementName))
            {
                FactoryElement element = new FactoryElement();
                element.Deserialize(reader);
                Factories.Add(element);
                return true;
            }
            else
            {
                return base.OnDeserializeUnrecognizedElement(elementName, reader);    
            }
        }

        /*
        private void Load()
        {
            _variables = new List<VariableElement>();

            try
            {
                using (TextReader reader = new StreamReader(_file))
                {
                    XElement root = XElement.Load(reader);

                    foreach(XElement element in root.Elements("variable"))
                    {
                        VariableElement variable = new VariableElement()
                                                       {
                                                           Content = element.Value,
                                                           Properties = new Dictionary<string, string>()
                                                       };

                        var properties = variable.Properties;

                        element.Attributes().ForEach(attr =>
                            {
                                string name  = attr.Name.LocalName.Trim();
                                string value = attr.Value.Trim();

                                if(name.EqualsIgnoreCase("name"))
                                {
                                    variable.Name = value;
                                }
                                else if(name.EqualsIgnoreCase("provider"))
                                {
                                    variable.Provider = value;
                                }
                                else
                                {
                                    properties.Add(name,value);
                                }

                                //check element
                                CheckVariableAttribute("name",variable.Name);
                                CheckVariableAttribute("provider", variable.Provider);
                            });

                        _variables.Add(variable);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Load environment config file error : {0}",_file),e);
            }
        }

        private static void CheckVariableAttribute(string name,object value)
        {
            if(null == value || (value is string && string.IsNullOrEmpty((string)value)))
            {
                throw new XmlException(string.Format("the '{0}' attribute of <variable> element must not empty",name));
            }
        }
        */
    }
}
