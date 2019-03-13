using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using SummerFresh.Basic;
namespace SummerFresh.Environment.Config
{
    public abstract class ExtensibleElement : ConfigurationElement
    {
        protected string                      _xmlContent;
        protected XElement                    _xmlElement;
        protected IDictionary<string, string> _extendedAttributes = new Dictionary<string, string>();
        
        public virtual string XmlContent
        {
            get { return _xmlContent; }
        }

        public virtual XElement XmlElement
        {
            get { return _xmlElement; }
        }

        public virtual IDictionary<string, string> ExtendedAttributes
        {
            get { return _extendedAttributes; }
        }

        public virtual void Deserialize(XElement element)
        {
            using (XmlReader reader = element.CreateReader())
            {
                reader.Read();
                Deserialize(reader);
            }
        }

        public virtual void Deserialize(XmlReader reader)
        {
            DeserializeElement(reader,false);
        }

        protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
        {
            _xmlContent = reader.ReadOuterXml();
            _xmlElement = XElement.Load(new StringReader(_xmlContent));
            
            _xmlElement.Attributes("xmlns").ForEach(a => a.Remove());

            //.Net的配置架构不允许Xml节点中出现CDATA或者文本节点(<node>text</node>)
            XElement removeChildsElement = new XElement(_xmlElement);
            IEnumerable<XElement> childs;
            while((childs = removeChildsElement.Elements()).Count() > 0)
            {
                childs.First().Remove();
            }

            using (XmlReader newReader = removeChildsElement.CreateReader())
            {
                newReader.Read();
                base.DeserializeElement(newReader,serializeCollectionKey);
            }

            OnDeserializedElement();
        }

        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            if (!"xmlns".Equals(name,StringComparison.OrdinalIgnoreCase))
            {
                _extendedAttributes.Add(name, value);
            }
            //ignore any unknow attributes
            return true;
        }

        protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
        {
            //ignore any unknow elements
            return true;
        }

        protected virtual void OnDeserializedElement()
        {
            
        }
    }
}
