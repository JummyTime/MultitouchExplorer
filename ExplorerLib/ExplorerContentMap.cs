using System.Xml;
using System.Collections.Generic;

namespace ExplorerLib
{
    public class ExplorerContentMap : ExplorerContentImage
    {
        private List<ExplorerEvent> eventList = new List<ExplorerEvent>();
        private List<ExplorerContentBase> contentList = new List<ExplorerContentBase>();
        public ExplorerContentMap(XmlNode map_node) : base(map_node)
        {

            foreach (XmlNode childNode in map_node.ChildNodes)
            {
                if (childNode.Name == "events")
                {
                    fillEventList(childNode);
                }
                else if(childNode.Name == "content")
                {
                    fillContentsList(childNode);
                }
            }
        }

        private void fillEventList(XmlNode events_node)
        {
            foreach (XmlNode childNode in events_node.ChildNodes)
            {
                if (childNode.Name == "event")
                {
                    eventList.Add(new ExplorerEvent(childNode));
                }
            }
        }

        private void fillContentsList(XmlNode contents_node)
        {
            foreach (XmlNode childNode in contents_node.ChildNodes)
            {
                if (childNode.Name == "image")
                {
                    contentList.Add(new ExplorerContentImage(childNode));
                }
                else if (childNode.Name == "video")
                {
                    contentList.Add(new ExplorerContentVideo(childNode));
                }
                else if (childNode.Name == "text")
                {
                    contentList.Add(new ExplorerContentText(childNode));
                }
                else if (childNode.Name == "map")
                {
                    contentList.Add(new ExplorerContentMap(childNode));
                }
            }
        }

    }
}
