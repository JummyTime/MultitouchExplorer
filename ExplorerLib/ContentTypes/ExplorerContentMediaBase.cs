using System;
using System.Xml;
using ExplorerLib.Exceptions;

namespace ExplorerLib.ContentTypes
{
    public abstract class ExplorerContentMediaBase : ExplorerContentBase
    {
        private String mediaPath;

        public ExplorerContentMediaBase(XmlNode content_node) : base(content_node)
        {
            foreach (XmlAttribute attribute in content_node.Attributes)
            {
                if (attribute.Name == "path")
                {
                    mediaPath = attribute.Value;
                }
            }
            if (mediaPath == "")
            {
                throw new ExplorerParseXMLException("Path not supplied for media tag '" + getName() + "'", null);
            }
        }

        public String getPath()
        {
            return mediaPath;
        }
    }
}