using System;
using System.Xml;
using ExplorerLib.Exceptions;

namespace ExplorerLib.ContentTypes
{
    public abstract class ExplorerContentBase
    {
        private String localID;
        private String name;
        private String shortName;

        public ExplorerContentBase(XmlNode content_node)
        {
            foreach (XmlAttribute attribute in content_node.Attributes)
            {
                
                if (attribute.Name == "localid")
                {
                    localID = attribute.Value;
                }
                else if (attribute.Name == "name")
                {
                    name = attribute.Value;
                }
                else if (attribute.Name == "shortname")
                {
                    shortName = attribute.Value;
                }
            }

            if (localID == null)
            {
                //Warning, no localid provided. Generate a UUID and use that
                localID = Guid.NewGuid().ToString();
            }
            if (name == null)
            {
                throw new ExplorerParseXMLException(
                    "Name not supplied for content tag. Node: '" + content_node.OuterXml + "'", null);
            }

            if (shortName == null)
            {
                shortName = name;
            }
        }


        public String getLocalId()
        {
            return localID;
        }


        public String getName()
        {
            return name;
        }


        public String getShortName()
        {
            return shortName;
        }
    }
}