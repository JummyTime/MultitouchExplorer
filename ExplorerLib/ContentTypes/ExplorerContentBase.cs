using System;
using System.Xml;
using ExplorerLib.Exceptions;

namespace ExplorerLib.ContentTypes
{
    public abstract class ExplorerContentBase
    {
        private String name;
        private String localID = "";

        public ExplorerContentBase(XmlNode content_node)
        {
            foreach (XmlAttribute attribute in content_node.Attributes)
            {
                if (attribute.Name == "name")
                {
                    name = attribute.Value;
                }
                else if (attribute.Name == "localid")
                {
                    localID = attribute.Value;
                }
            }

            if (name == null)
            {
                throw new ExplorerParseXMLException(
                    "Name not supplied for content tag. Node: '" + content_node.OuterXml + "'", null);
            }

            if (localID == null)
            {
                //Warning, no localid provided. Generate a UUID and use that
                localID = Guid.NewGuid().ToString();
            }
        }

        public String getName()
        {
            return name;
        }

        public String getLocalId()
        {
            return localID;
        }
    }
}