using System;
using System.IO;
using System.Windows.Media.Imaging;
using System.Xml;

namespace ExplorerGUICombined
{
    public class GUIConfiguration
    {
        private BitmapImage addToDockImage;
        public GUIConfiguration(String file_name)
        {
            XmlTextReader xmlReader = new XmlTextReader(file_name);
            if (xmlReader.Read())
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlReader);

                if (xmlDoc.ChildNodes.Count == 1 && xmlDoc.ChildNodes.Item(0).Name == "config")
                {
                    foreach (XmlNode node in xmlDoc.ChildNodes.Item(0).ChildNodes)
                    {
                        if (node.Name == "item")
                        {
                            String key = node.Attributes["key"].Value;
                            String val = node.Attributes["value"].Value;
                            if (key == "add_to_dock_icon")
                            {
                                System.Console.WriteLine("add to dock image: " + val);
                                addToDockImage = new BitmapImage(new Uri(Path.GetFullPath(val)));
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Invalid root XML node in GUI configuration");
                }
            }
        }

        public BitmapImage getAddToDockImage()
        {
            return addToDockImage;
        }
    }
}
