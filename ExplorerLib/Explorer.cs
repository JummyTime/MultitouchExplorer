using System.Xml;
using System;

namespace ExplorerLib
{
    public class Explorer
    {
        private ExplorerContentMap rootMap;

        public Explorer(String file_name)
        {
            XmlTextReader xmlReader = new XmlTextReader(file_name);
            if (xmlReader.Read())
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlReader);

                if (xmlDoc.ChildNodes.Count == 1 && xmlDoc.ChildNodes.Item(0).Name == "map")
                {
                    rootMap = new ExplorerContentMap(xmlDoc.ChildNodes.Item(0));
                }
                else
                {
                    throw new ExplorerLoadXMLException("Multiple Root Tags or Root tag not 'explorer'", null);
                }

            }
            else
            {
                throw new ExplorerLoadXMLException("XML Document could not be read", null);
            }           
        }

        public ExplorerContentMap getRootContentMap()
        {
            return rootMap;
        }
    }
}
