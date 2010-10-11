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

        public ExplorerContentsContainer(XmlNode contents_node)
        {
            foreach (XmlNode childNode in contents_node.ChildNodes)
            {
                ExplorerContentBase newItem;
                if (childNode.Name == "image")
                {
                    newItem = new ExplorerContentImage(childNode);
                }
                else if (childNode.Name == "video")
                {
                    newItem = new ExplorerContentVideo(childNode);
                }
                else if (childNode.Name == "text")
                {
                    newItem = new ExplorerContentText(childNode);
                }
                else if (childNode.Name == "map")
                {
                    newItem = new ExplorerContentMap(childNode);
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