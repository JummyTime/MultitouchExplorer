using System.Collections.Generic;
using System.Xml;

namespace ExplorerLib.Containers
{
    public class ExplorerPointsContainer
    {
        private readonly List<ExplorerPoint> pointsList = new List<ExplorerPoint>();

        public ExplorerPointsContainer(XmlNode regions_node)
        {
            foreach (XmlNode childNode in regions_node.ChildNodes)
            {
                if (childNode.Name == "point")
                {
                    pointsList.Add(new ExplorerPoint(childNode));
                }
            }
        }
        public List<ExplorerPoint> getPoints()
        {
            return pointsList;
        }
    }

    
}