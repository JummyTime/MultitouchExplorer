using System.Collections.Generic;
using System.Xml;
using ExplorerLib.ContentTypes;

namespace ExplorerLib.Containers
{
    public class ExplorerRegionsContainer
    {
        private ExplorerContentMap parentMap;

        private List<ExplorerRegion> regionList = new List<ExplorerRegion>();

        public ExplorerRegionsContainer(ExplorerConfiguration configuration, ExplorerContentMap parent_map, XmlNode regions_node)
        {
            parentMap = parent_map;
            foreach (XmlNode childNode in regions_node.ChildNodes)
            {
                if (childNode.Name == configuration.getRegionTag())
                {
                    regionList.Add(new ExplorerRegion(configuration, childNode));
                }
            }
        }

        public ExplorerRegion getRegionContainingPoint(ExplorerPoint p)
        {

            foreach (ExplorerRegion currentRegion in regionList)
            {
                if (currentRegion.regionContainsPoint(p)) return currentRegion;
            }

            return null;
        }

        public List<ExplorerRegion> getRegions()
        {
            return regionList;
        }

        public ExplorerContentMap getParentMap()
        {
            return parentMap;
        }
    }
}