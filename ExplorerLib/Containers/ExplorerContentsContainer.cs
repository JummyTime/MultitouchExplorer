using System;
using System.Collections.Generic;
using System.Xml;
using ExplorerLib.ContentTypes;
using ExplorerLib.Exceptions;

namespace ExplorerLib.Containers
{
    public class ExplorerContentsContainer
    {
        private readonly Dictionary<String, ExplorerContentBase> contentList = new Dictionary<String, ExplorerContentBase>();

        public ExplorerContentsContainer(ExplorerConfiguration configuration, XmlNode contents_node)
        {
            foreach (XmlNode childNode in contents_node.ChildNodes)
            {
                ExplorerContentBase newItem;
                if (childNode.Name == configuration.getImageTag())
                {
                    newItem = new ExplorerContentImage(configuration, childNode);
                }
                else if (childNode.Name == configuration.getVideoTag())
                {
                    newItem = new ExplorerContentVideo(configuration, childNode);
                }
                else if (childNode.Name == configuration.getTextTag())
                {
                    newItem = new ExplorerContentText(configuration, childNode);
                }
                else if (childNode.Name == configuration.getMapTag())
                {
                    newItem = new ExplorerContentMap(configuration, childNode);
                }
                else
                {
                    throw new ExplorerParseXMLException("Unknown child node: " + childNode.Name, null);
                }

                contentList.Add(newItem.getLocalId(), newItem);
            }
        }

        public ExplorerContentBase getByLocalId(String local_id)
        {
            if(contentList.ContainsKey(local_id))
            {
                return contentList[local_id];
            }

            return null;
        }

        public Dictionary<String, ExplorerContentBase> getContentsDictionary()
        {
            return contentList;
        }
    }
}