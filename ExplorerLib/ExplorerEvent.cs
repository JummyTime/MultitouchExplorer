using System;
using System.Collections.Generic;
using System.Xml;

namespace ExplorerLib
{
    public class ExplorerEvent
    {
        private String eventName = null;
        private HashSet<String> eventTags = new HashSet<String>();
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

            if (eventName == null)
            {
                throw new ExplorerParseXMLException("Event Name not supplied for event tag. Node: '" + event_node.OuterXml + "'", null);
            }


            foreach (XmlNode childNode in event_node.ChildNodes)
            {
                if (childNode.Name == "tag")
                {
                    eventTags.Add(childNode.InnerText.ToLower());
                }
                else if(childNode.Name == "start_date")
                {
                    try
                    {
                        startDate = Convert.ToDateTime(childNode.InnerText);
                    }
                    catch(FormatException ex)
                    {
                        throw new ExplorerParseXMLException("Start Date is invalid for '" + eventName + "'", ex);
                    }
                }
                else if (childNode.Name == "end_date")
                {
                    try
                    {
                        endDate = Convert.ToDateTime(childNode.InnerText);
                    }
                    catch(FormatException ex)
                    {
                        throw new ExplorerParseXMLException("End Date is invalid for '" + eventName + "'", ex);
                    }
                }
                else
                {
                    throw new ExplorerParseXMLException("Unknown child node: " + childNode.Name, null);
                }
            }
        }

        public Boolean containsTag(String tag)
        {
            return eventTags.Contains(tag.ToLower());
        }

        public Boolean meetsFilterRequirements(ExplorerEventFilter filter)
        {
            if (filter.TagFilter != "" && !containsTag(filter.TagFilter))
            {
                return false;
            }

            return true;

        }

        public HashSet<String> getTags()
        {
            return eventTags;
        }

        public override String ToString()
        {
            return getName() + " starts on " + getStartDate() + " and ends on " + getEndDate();
        }

        public String getName()
        {
            return eventName;
        }

        public DateTime getStartDate()
        {
            return startDate;
        }

        public DateTime getEndDate()
        {
            return endDate;
        }
    }
}
