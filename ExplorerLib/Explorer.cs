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
                    System.Console.WriteLine("ERROR: Multiple Root Tags or Root tag not 'explorer'");
                }

            }
            else
            {
                System.Console.WriteLine("ERROR: XML Document could not be read");
            }           
        }

        public ExplorerContentMap getRootContentMap()
        {
            return rootMap;
        }
    }
}
