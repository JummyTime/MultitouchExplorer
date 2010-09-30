using System.Xml;
using System;

namespace ExplorerLib
{
    public class ExplorerMapFactory
    {
        public static ExplorerMap ExplorerMapFromXML(String file_name)
        {
            XmlTextReader xmlReader = new XmlTextReader(file_name);
            if (xmlReader.Read())
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlReader);
                return new ExplorerMap(xmlDoc);
            }

            return null;

        }
    }
}
