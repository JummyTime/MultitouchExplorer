using System;
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

        public ExplorerContentMap(ExplorerConfiguration configuration, XmlNode map_node)
            : base(configuration, map_node)
        {
            foreach (XmlNode childNode in map_node.ChildNodes)
            {
                if (childNode.Name == configuration.getEventsTag())
                {
                    eventsContainer = new ExplorerEventsContainer(configuration, childNode);
                }
                else if (childNode.Name == configuration.getContentsTag())
                {
                    contentsContainer = new ExplorerContentsContainer(configuration, childNode);
                }
                else if (childNode.Name == configuration.getRegionsTag())
                {
                    regionsContainer = new ExplorerRegionsContainer(configuration, this, childNode);
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

        public Dictionary<String, ExplorerContentBase> getContentsDictionary()
        {
            return contentsContainer.getContentsDictionary();
        }
    }
}