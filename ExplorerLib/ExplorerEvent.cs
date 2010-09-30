using System;
using System.Collections.Generic;
using System.Xml;

namespace ExplorerLib
{
    public class ExplorerEvent
    {
        private String eventName;
        private List<String> eventTags = new List<String>();
        private DateTime startDate = new DateTime(0);
        private DateTime endDate = new DateTime(0);

        public ExplorerEvent(XmlNode event_node)
        {
            foreach (XmlAttribute attribute in event_node.Attributes)
            {
                if (attribute.Name == "name")
                {
                    eventName = attribute.Value;
                }
            }

            foreach (XmlNode childNode in event_node.ChildNodes)
            {
                if (childNode.Name == "tag")
                {
                    eventTags.Add(childNode.InnerText);
                }
                else if(childNode.Name == "start_date")
                {
                    startDate = Convert.ToDateTime(childNode.InnerText);
                }
                else if (childNode.Name == "end_date")
                {
                    endDate = Convert.ToDateTime(childNode.InnerText);
                }
            }
        }
    }
}
