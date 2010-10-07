using System.Collections.Generic;
using System.Xml;
using ExplorerLib.Containers;

namespace ExplorerLib.ContentTypes
{
    public class ExplorerContentMap : ExplorerContentImage
    {
        private ExplorerEventsContainer eventsContainer;
        private ExplorerContentsContainer contentsContainer;
        private readonly ExplorerRegionsContainer regionsContainer;

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

        public ExplorerRegion getRegionContainingPoint(ExplorerPoint p)
        {
            return regionsContainer.getRegionContainingPoint(p);
        }

        public List<ExplorerRegion> getRegions()
        {
            return regionsContainer.getRegions();
        }

    }
}