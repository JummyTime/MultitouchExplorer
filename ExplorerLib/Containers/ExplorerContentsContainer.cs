using System.Collections.Generic;
using System.Xml;
using ExplorerLib.ContentTypes;
using ExplorerLib.Exceptions;

namespace ExplorerLib.Containers
{
    public class ExplorerContentsContainer
    {
        private List<ExplorerContentBase> contentList = new List<ExplorerContentBase>();

        public ExplorerContentsContainer(XmlNode contents_node)
        {
            foreach (XmlNode childNode in contents_node.ChildNodes)
            {
                if (childNode.Name == "image")
                {
                    contentList.Add(new ExplorerContentImage(childNode));
                }
                else if (childNode.Name == "video")
                {
                    contentList.Add(new ExplorerContentVideo(childNode));
                }
                else if (childNode.Name == "text")
                {
                    contentList.Add(new ExplorerContentText(childNode));
                }
                else if (childNode.Name == "map")
                {
                    contentList.Add(new ExplorerContentMap(childNode));
                }
                else
                {
                    throw new ExplorerParseXMLException("Unknown child node: " + childNode.Name, null);
                }
            }
        }
    }
}