using System;
using System.Collections.Generic;
using System.Xml;
using ExplorerLib.Exceptions;

namespace ExplorerLib
{
    public class ExplorerEvent
    {
        private String eventName;
        private HashSet<String> eventTags = new HashSet<String>();
        private DateTime startDate = new DateTime(0);
        private DateTime endDate = new DateTime(0);

        public ExplorerEvent(ExplorerConfiguration configuration, XmlNode event_node)
        {
            foreach (XmlAttribute attribute in event_node.Attributes)
            {
                if (attribute.Name == configuration.getEventNameAttr())
                {
                    eventName = attribute.Value;
                }

                else if (attribute.Name == configuration.getEventStartTimeAttr())
                {
                    try
                    {
                        startDate = Convert.ToDateTime(attribute.Value);
                    }
                    catch (FormatException ex)
                    {
                        throw new ExplorerParseXMLException("Start Date is invalid for '" + eventName + "'", ex);
                    }
                }
                else if (attribute.Name == configuration.getEventEndTimeAttr())
                {
                    try
                    {
                        endDate = Convert.ToDateTime(attribute.Value);
                    }
                    catch (FormatException ex)
                    {
                        throw new ExplorerParseXMLException("End Date is invalid for '" + eventName + "'", ex);
                    }
                }
                else
                {
                    throw new ExplorerParseXMLException("Unknown child node: " + attribute.Name, null);
                }
            }

            if (eventName == null)
            {
                throw new ExplorerParseXMLException(
                    "Event Name not supplied for event tag. Node: '" + event_node.OuterXml + "'", null);
            }


            foreach (XmlNode childNode in event_node.ChildNodes)
            {
                if (childNode.Name == configuration.getEventTagTag())
                {
                    eventTags.Add(childNode.InnerText.ToLower());
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