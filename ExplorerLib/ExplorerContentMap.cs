using System.Xml;
using System.Collections.Generic;
using System;

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

        public List<ExplorerEvent> getChildEvents(String tag_filter, int max_hops)
        {
            List<ExplorerEvent> allEvents = new List<ExplorerEvent>();
            if(tag_filter == "")
            {
                allEvents.AddRange(eventList);
            }
            else
            {
                foreach(ExplorerEvent childEvent in allEvents)
                {
                    if(childEvent.containsTag(tag_filter))
                    {
                        allEvents.Add(childEvent);
                    }
                }
            }

            if (max_hops > 0 || max_hops == -1)
            {
                max_hops--;
                foreach (ExplorerContentBase contentItem in contentList)
                {
                    if (contentItem.GetType() == typeof(ExplorerContentMap))
                    {
                        allEvents.AddRange(((ExplorerContentMap)contentItem).getChildEvents(tag_filter, max_hops));
                    }
                }
            }

            return allEvents;
        }

        public List<ExplorerEvent> getChildEvents(String tag_filter)
        {
            return getChildEvents(tag_filter, -1);
        }

        public List<ExplorerEvent> getChildEvents(int max_hops)
        {
            return getChildEvents("", max_hops);
        }

        public List<ExplorerEvent> getChildEvents()
        {
            return getChildEvents("", -1);
        }
    }
}
