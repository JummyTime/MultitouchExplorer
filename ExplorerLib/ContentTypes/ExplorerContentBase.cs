using System;
using System.Xml;
using ExplorerLib.Exceptions;

namespace ExplorerLib.ContentTypes
{
    public abstract class ExplorerContentBase
    {
        private String localID = "";

        public ExplorerContentBase(XmlNode content_node)
        {
            foreach (XmlAttribute attribute in content_node.Attributes)
            {
                
                if (attribute.Name == "localid")
                {
                    localID = attribute.Value;
                }
            }

            if (localID == null)
            {
                //Warning, no localid provided. Generate a UUID and use that
                localID = Guid.NewGuid().ToString();
            }
        }


        public String getLocalId()
        {
            return localID;
        }
    }
}