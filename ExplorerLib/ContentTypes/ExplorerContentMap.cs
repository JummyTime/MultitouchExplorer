using System.Xml;
using ExplorerLib.Containers;

namespace ExplorerLib.ContentTypes
{
    public class ExplorerContentMap : ExplorerContentImage
    {
        private ExplorerEventsContainer eventsContainer;
        private ExplorerContentsContainer contentsContainer;
        private ExplorerRegionsContainer regionsContainer;

        public ExplorerContentMap(XmlNode map_node)
            : base(map_node)
        {
            foreach (XmlNode childNode in map_node.ChildNodes)
            {
                if (childNode.Name == "events")
                {
                    eventsContainer = new ExplorerEventsContainer(childNode);
                }
                else if (childNode.Name == "content")
                {
                    contentsContainer = new ExplorerContentsContainer(childNode);
                }
                else if (childNode.Name == "regions")
                {
                    regionsContainer = new ExplorerRegionsContainer(this, childNode);
                }
            }
        }
    }
}