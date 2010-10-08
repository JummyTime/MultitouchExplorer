using System;
using System.IO;
using System.Xml;
using ExplorerLib.Exceptions;

namespace ExplorerLib.ContentTypes
{
    public abstract class ExplorerContentMediaBase : ExplorerContentBase
    {

        private String name;
        private String mediaPath;

        public ExplorerContentMediaBase(XmlNode content_node) : base(content_node)
        {
            foreach (XmlAttribute attribute in content_node.Attributes)
            {
                if (attribute.Name == "path")
                {
                    mediaPath = attribute.Value;
                }
                else if (attribute.Name == "name")
                {
                    name = attribute.Value;
                }
            }
            if (mediaPath == null)
            {
                throw new ExplorerParseXMLException("Path not supplied for media tag '" + getName() + "'", null);
            }

            if (name == null)
            {
                throw new ExplorerParseXMLException(
                    "Name not supplied for media content tag. Node: '" + content_node.OuterXml + "'", null);
            }
        }

        public String getPathString()
        {
            return mediaPath;
        }

        public Uri getPathUri()
        {
            return new Uri(Path.GetFullPath(mediaPath));
        }

        public String getName()
        {
            return name;
        }
    }
}