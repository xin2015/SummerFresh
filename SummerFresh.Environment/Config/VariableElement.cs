using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SummerFresh.Environment.Config
{
    public class VariableElement : ExtensibleElement
    {
        private const string NameProperty     = "name";
        private const string ScopeProperty    = "scope";
        private const string ValueProperty    = "value";
        private const string TypeNameProperty = "type";
        private const string FactoryProperty  = "factory";

        private Type  _type;
        private Scope _scope;

        [ConfigurationProperty(NameProperty,IsRequired = true)]
        public string Name
        {
            get { return (string)this[NameProperty]; }
            set { this[NameProperty] = value; }
        }

        public Scope Scope
        {
            get { return _scope; }
        }

        [ConfigurationProperty(ScopeProperty,DefaultValue = "session", IsRequired = false)]
        public string ScopeValue
        {
            get { return (string)this[ScopeProperty]; }
            set { this[ScopeProperty] = value; }
        }

        [ConfigurationProperty(ValueProperty,IsRequired = false)]
        public string Value
        {
            get { return (string) this[ValueProperty]; }
            set { this[ValueProperty] = value; }
        }

        [ConfigurationProperty(TypeNameProperty,IsRequired = false)]
        public string TypeName
        {
            get { return (string) this[TypeNameProperty]; }
            set { this[TypeNameProperty] = value; }
        }

        public Type Type
        {
            get
            {
                if (_type == null)
                {
                    _type = string.IsNullOrEmpty(TypeName) ? null : Type.GetType(TypeName);
                }
                return _type;
            }
        }

        [ConfigurationProperty(FactoryProperty, IsRequired = false)]
        public string Factory
        {
            get { return (string) this[FactoryProperty]; }
            set { this[FactoryProperty] = value; }
        }

        protected override void  OnDeserializedElement()
        {
 	         Validate();
        }

        protected void Validate()
        {
            if (string.IsNullOrEmpty(Value) && string.IsNullOrEmpty(Factory) && string.IsNullOrEmpty(TypeName))
            {
                throw new ConfigurationErrorsException(
                    string.Format("'{0}' or '{1}' or '{2}' attribute could not be empty,one of them must be specified",
                                  ValueProperty,FactoryProperty,TypeNameProperty));
            }

            if (!string.IsNullOrEmpty(ScopeValue))
            {
                try
                {
                    _scope = (Scope)Enum.Parse(typeof(Scope), ScopeValue.Trim(), true);
                }
                catch (ArgumentException)
                {
                    throw new ConfigurationErrorsException(
                        string.Format("invalid scope value '{0}'",ScopeValue));
                }
            }
            else
            {
                _scope = Scope.Session;
            }
        }
    }
}