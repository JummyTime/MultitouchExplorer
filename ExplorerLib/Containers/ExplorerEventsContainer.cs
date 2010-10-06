using System;
using System.Collections.Generic;
using System.Xml;

namespace ExplorerLib.Containers
{
    public class ExplorerEventsContainer
    {
        private readonly List<ExplorerEvent> eventList = new List<ExplorerEvent>();

        public ExplorerEventsContainer(XmlNode events_node)
        {
            foreach (XmlNode childNode in events_node.ChildNodes)
            {
                if (childNode.Name == "event")
                {
                    eventList.Add(new ExplorerEvent(childNode));
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

            foreach (ExplorerEvent childEvent in eventList)
            {
                if (childEvent.meetsFilterRequirements(filter))
                {
                    eventsFiltered.Add(childEvent);
                }
            }

            return eventsFiltered;
        }
    }
}