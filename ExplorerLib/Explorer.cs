using System;
using System.Xml;
using ExplorerLib.ContentTypes;
using ExplorerLib.Exceptions;

namespace ExplorerLib
{
    public class Explorer
    {
        private ExplorerContentMap rootMap;
        private ExplorerConfiguration configuration;

        public Explorer(String config_name, String file_name)
        {
            configuration = new ExplorerConfiguration(config_name);

            XmlTextReader xmlReader = new XmlTextReader(file_name);
            if (xmlReader.Read())
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlReader);

                if (xmlDoc.ChildNodes.Count == 1 && xmlDoc.ChildNodes.Item(0).Name == configuration.getMapTag())
                {
                    rootMap = new ExplorerContentMap(configuration, xmlDoc.ChildNodes.Item(0));
                }
                else
                {
                    throw new ExplorerLoadXMLException("Multiple Root Tags or Root tag not '" + configuration.getMapTag() + "'", null);
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

        public ExplorerConfiguration getConfiguration()
        {
            return configuration;
        }
    }
}