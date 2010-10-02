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
                else
                {
                    throw new ExplorerParseXMLException("Unknown child node: " + childNode.Name, null);
                }
            }
        }

        public HashSet<String> getChildEventTags(ExplorerEventFilter filter)
        {
            HashSet<String> childEventTags = new HashSet<String>();
            List<ExplorerEvent> childEvents = getChildEvents(filter);
            foreach (ExplorerEvent childEvent in childEvents)
            {
                childEventTags.UnionWith(childEvent.getTags());
            }

            return childEventTags;
        }

        public List<ExplorerEvent> getChildEvents(ExplorerEventFilter filter)
        {
            List<ExplorerEvent> eventsFiltered = new List<ExplorerEvent>();
            
            foreach(ExplorerEvent childEvent in eventList)
            {
                if (childEvent.meetsFilterRequirements(filter))
                {
                    eventsFiltered.Add(childEvent);
                }
            }

            int originalMaxHops = filter.MaxHops;

            if (filter.MaxHops != 0)
            {
                if (filter.MaxHops != -1) filter.MaxHops--;
                foreach (ExplorerContentBase contentItem in contentList)
                {
                    if (contentItem is ExplorerContentMap)
                    {
                        eventsFiltered.AddRange(((ExplorerContentMap)contentItem).getChildEvents(filter));
                    }
                }
            }

            filter.MaxHops = originalMaxHops; //So the object is back at the original state

            return eventsFiltered;
        }
    }
}
