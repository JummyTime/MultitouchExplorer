using System.Xml;
using ExplorerLib.Containers;

namespace ExplorerLib
{
    public class ExplorerRegion
    {
        private ExplorerEventsContainer eventsContainer;
        private ExplorerPointsContainer pointsContainer;
        private ExplorerContentsContainer contentsContainer;

        public ExplorerRegion(XmlNode region_node)
        {
            foreach (XmlNode childNode in region_node.ChildNodes)
            {
                if (childNode.Name == "events")
                {
                    eventsContainer = new ExplorerEventsContainer(childNode);
                }
                else if (childNode.Name == "points")
                {
                    pointsContainer = new ExplorerPointsContainer(childNode);
                }
                else if (childNode.Name == "contents")
                {
                    contentsContainer = new ExplorerContentsContainer(childNode);
                }
            }
        }

        public bool regionContainsPoint(ExplorerPoint point)
        {
            return false;
        }
    }
}