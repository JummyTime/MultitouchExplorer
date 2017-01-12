using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using ExplorerLib.Exceptions;

namespace ExplorerLib
{
    public class ExplorerConfiguration
    {
        private Dictionary<String, String> keyValuePairs = new Dictionary<String, String>();

        public ExplorerConfiguration(String file_name)
        {
            XmlTextReader xmlReader = new XmlTextReader(file_name);
            if (xmlReader.Read())
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlReader);

                if (xmlDoc.ChildNodes.Count == 1 && xmlDoc.ChildNodes.Item(0).Name == "configuration")
                {
                    foreach (XmlNode node in xmlDoc.ChildNodes.Item(0).ChildNodes)
                    {
                        String key = null;
                        String value = null;
                        foreach (XmlAttribute attribute in node.Attributes)
                        {
                            if (attribute.Name == "key")
                            {
                                key = attribute.Value;
                            }
                        }
                        if (key == null)
                        {
                            throw new ExplorerParseXMLException("Configuration tag missing key '" + node.OuterXml + "'", null);
                        }
                        
                        value = node.InnerText.ToLower();
                        keyValuePairs[key]=value;
                    }
                }
                else
                {
                    throw new ExplorerLoadXMLException("Multiple Root Tags or Root tag not 'configuration'", null);
                }
            }
            else
            {
                throw new ExplorerLoadXMLException("XML Document could not be read", null);
            }
        }

        public String getValueForKey(String key)
        {
            return keyValuePairs[key.ToLower()];
        }

        public String getValueForKey(String key, String default_value)
        {
            if (keyValuePairs.ContainsKey(key.ToLower()))
                return keyValuePairs[key.ToLower()];
            
            return default_value;
        }

        public String getMapTag() { return getValueForKey("map_tag", "map"); }
        public String getContentsTag() { return getValueForKey("contents_tag", "contents"); }
        public String getRegionsTag() { return getValueForKey("regions_tag", "regions"); }
        public String getRegionTag() { return getValueForKey("region_tag", "region"); }
        public String getEventsTag() { return getValueForKey("events_tag", "events"); }
        public String getEventTag() { return getValueForKey("event_tag", "event"); }
        public String getImageTag() { return getValueForKey("image_tag", "image"); }
        public String getTextTag() { return getValueForKey("text_tag", "text"); }
        public String getVideoTag() { return getValueForKey("video_tag", "video"); }
        public String getPointsTag() { return getValueForKey("points_tag", "points"); }
        public String getPointTag() { return getValueForKey("point_tag", "point"); }
        public String getEventTagTag() { return getValueForKey("event_tag_tag", "tag"); }

        public String getDefaultTextIdAttr() { return getValueForKey("defaulttextid_attribute", "defaulttextid"); }
        public String getDefaultImageIdAttr() { return getValueForKey("defaultimageid_attribute", "defaultimageid"); }
        public String getLocalIdAttr() { return getValueForKey("localid_attribute", "localid"); }
        public String getPointXAttr() { return getValueForKey("point_x_attribute", "x"); }
        public String getPointYAttr() { return getValueForKey("point_y_attribute", "y"); }
        public String getMediaPathAttr() { return getValueForKey("media_path_attribute", "path"); }
        public String getContentNameAttr() { return getValueForKey("content_name_attribute", "name"); }
        public String getContentShortNameAttr() { return getValueForKey("content_shortname_attribute", "shortname"); }
        public String getEventStartTimeAttr() { return getValueForKey("event_start_time_attribute", "start"); }
        public String getEventEndTimeAttr() { return getValueForKey("event_end_time_attribute", "end"); }
        public String getEventNameAttr() { return getValueForKey("event_name_attribute", "name"); }

    }
}
