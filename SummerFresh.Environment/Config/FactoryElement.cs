using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using SummerFresh.Environment.Variable;
using SummerFresh.Util;
using SummerFresh.Basic;
namespace SummerFresh.Environment.Config
{
    public class FactoryElement : ExtensibleElement
    {
        public const string TypeNameProperty = "type";
        public const string NameProperty     = "name";
        public const string ElementName      = "factory";

        private IVariableFactory _instance;

        [ConfigurationProperty(NameProperty, IsRequired = false)]
        public string Name
        {
            get { return (string) this[NameProperty]; }
            set { this[NameProperty] = value; }
        }

        [ConfigurationProperty(TypeNameProperty, IsRequired = true)]
        public string TypeName
        {
            get { return (string)this[TypeNameProperty]; }
            set { this[TypeNameProperty] = value; }
        }

        public IVariableFactory Instance
        {
            get
            {
                if (null == _instance)
                {
                    var type = Type.GetType(TypeName);
                    _instance = Activator.CreateInstance(type) as IVariableFactory;
                    if(_instance == null)
                    {
                        throw new Exception("Type '{0}' not found!".FormatTo(TypeName));
                    }
                }
                return _instance;
            }
        }
    }
}