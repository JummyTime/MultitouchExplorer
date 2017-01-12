using System;
using System.Collections.Generic;
using System.Xml;
using ExplorerLib.Containers;
using ExplorerLib.ContentTypes;
using ExplorerLib.Exceptions;

namespace ExplorerLib
{
    public class ExplorerRegion
    {
        private ExplorerEventsContainer eventsContainer;
        private ExplorerPointsContainer pointsContainer;
        private ExplorerContentsContainer contentsContainer;

        private String name;
        private String defaultImageLocalId;
        private String defaultTextLocalId;
        public ExplorerRegion(ExplorerConfiguration configuration, XmlNode region_node)
        {
            foreach (XmlAttribute attribute in region_node.Attributes)
            {
                if (attribute.Name == "name")
                {
                    name = attribute.Value;
                }
                else if(attribute.Name == "defaultimageid")
                {
                    defaultImageLocalId = attribute.Value;
                }
                else if (attribute.Name == "defaulttextid")
                {
                    defaultTextLocalId = attribute.Value;
                }
            }

            if (name == null)
            {
                throw new ExplorerParseXMLException(
                    "Name not supplied for content tag. Node: '" + region_node.OuterXml + "'", null);
            }

            foreach (XmlNode childNode in region_node.ChildNodes)
            {
                if (childNode.Name == configuration.getEventsTag())
                {
                    eventsContainer = new ExplorerEventsContainer(configuration, childNode);
                }
                else if (childNode.Name == configuration.getPointsTag())
                {
                    pointsContainer = new ExplorerPointsContainer(configuration, childNode);
                }
                else if (childNode.Name == configuration.getContentsTag())
                {
                    contentsContainer = new ExplorerContentsContainer(configuration, childNode);
                }
            }

            if(defaultImageLocalId != null)
            {
                ExplorerContentBase baseContentItem = contentsContainer.getByLocalId(defaultImageLocalId);
                if(baseContentItem == null)
                {
                    defaultImageLocalId = null;
                    throw new ExplorerParseXMLException(
                    "Default Image ID provided does not exist in node. Node: '" + region_node.OuterXml + "'", null);
                }

                if(!(baseContentItem is ExplorerContentImage))
                {
                    defaultImageLocalId = null;
                    throw new ExplorerParseXMLException(
                    "Default Image ID provided does not reference an image. Node: '" + region_node.OuterXml + "'", null);
                }
            }

            if (defaultTextLocalId != null)
            {
                ExplorerContentBase baseContentItem = contentsContainer.getByLocalId(defaultTextLocalId);
                if (baseContentItem == null)
                {
                    defaultTextLocalId = null;
                    throw new ExplorerParseXMLException(
                    "Default text ID provided does not exist in node. Node: '" + region_node.OuterXml + "'", null);
                }

                if (!(baseContentItem is ExplorerContentText))
                {
                    defaultTextLocalId = null;
                    throw new ExplorerParseXMLException(
                    "Default text ID provided does not reference an text. Node: '" + region_node.OuterXml + "'", null);
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

        public ExplorerContentImage getDefaultContentImage()
        {
            if (defaultImageLocalId == null) return null;
            return (ExplorerContentImage)contentsContainer.getByLocalId(defaultImageLocalId);
        }
        public ExplorerContentText getDefaultContentText()
        {
            if (defaultTextLocalId == null) return null;
            return (ExplorerContentText)contentsContainer.getByLocalId(defaultTextLocalId);
        }

        public Dictionary<String, ExplorerContentBase> getContentsDictionary()
        {
            return contentsContainer.getContentsDictionary();
        }
    }
}