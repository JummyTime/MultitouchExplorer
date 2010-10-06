using System.Collections.Generic;
using System.Xml;
using ExplorerLib.ContentTypes;

namespace ExplorerLib.Containers
{
    public class ExplorerRegionsContainer
    {
        private ExplorerContentMap parentMap;

        private List<ExplorerRegion> regionList = new List<ExplorerRegion>();

        public ExplorerRegionsContainer(ExplorerContentMap parent_map, XmlNode regions_node)
        {
            parentMap = parent_map;
            foreach (XmlNode childNode in regions_node.ChildNodes)
            {
                if (childNode.Name == "region")
                {
                    regionList.Add(new ExplorerRegion(childNode));
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