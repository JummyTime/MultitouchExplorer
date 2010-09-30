using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ExplorerLib
{
    public abstract class ExplorerContentBase
    {
        private String name = "";
        public ExplorerContentBase(XmlNode content_node)
        {
            foreach (XmlAttribute attribute in content_node.Attributes)
            {
                if (attribute.Name == "name")
                {
                    name = attribute.Value;
                    Console.WriteLine("ContentBase Creation: " + name);
                }
            }
        }

        public String getName()
        {
            return name;
        }
    }
}
