using System;
using System.Collections.Generic;
using System.Xml;
using ExplorerLib.Containers;
using ExplorerLib.Exceptions;

namespace ExplorerLib
{
    public class ExplorerRegion
    {
        private ExplorerEventsContainer eventsContainer;
        private ExplorerPointsContainer pointsContainer;
        private ExplorerContentsContainer contentsContainer;

        private String name;
        public ExplorerRegion(XmlNode region_node)
        {
            foreach (XmlAttribute attribute in region_node.Attributes)
            {
                if (attribute.Name == "name")
                {
                    name = attribute.Value;
                }
            }

            if (name == null)
            {
                throw new ExplorerParseXMLException(
                    "Name not supplied for content tag. Node: '" + region_node.OuterXml + "'", null);
            }

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

        //C# Port example C code at http://alienryderflex.com/polygon/
        public bool regionContainsPoint(ExplorerPoint point)
        {
            Boolean oddNodes = false;
            List<ExplorerPoint> points = getPoints();
            int polySides = points.Count;
            int j = polySides - 1;
            double workingPointX = point.getX();
            double workingPointY = point.getY();
            for(int i = 0; i < polySides; i++)
            {
                if ((points[i].getY() < workingPointY && points[j].getY() >= workingPointY) || (points[j].getY() < workingPointY && points[i].getY() >= workingPointY))
                {
                    if(points[i].getX() + (workingPointY-points[i].getY())/(points[j].getY()-points[i].getY())*(points[j].getX() - points[i].getX()) < workingPointX)
                    {
                        oddNodes = !oddNodes;
                    }
                }
                j = i;
            }

            return oddNodes;
        }

        public String getName()
        {
            return name;
        }

        public List<ExplorerPoint> getPoints()
        {
            return pointsContainer.getPoints();
        }
    }
}