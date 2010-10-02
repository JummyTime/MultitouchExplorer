using System;
using System.Xml;

namespace ExplorerLib
{
    public abstract class ExplorerContentBase
    {
        private String name = null;
        public ExplorerContentBase(XmlNode content_node)
        {
            foreach (XmlAttribute attribute in content_node.Attributes)
            {
                if (attribute.Name == "name")
                {
                    name = attribute.Value;
                }
            }

            if (name == null)
            {
                throw new ExplorerParseXMLException("Name not supplied for content tag. Node: '" + content_node.OuterXml + "'", null);
            }
        }

        public String getName()
        {
            return name;
        }
    }
}
