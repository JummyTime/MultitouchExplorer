using System;
using System.Windows.Controls;
using System.Xml;

namespace ExplorerLib.ContentTypes
{
    public class ExplorerContentVideo : ExplorerContentMediaBase
    {
        private MediaElement mediaElement;

        public ExplorerContentVideo(XmlNode video_node) : base(video_node)
        {
            mediaElement = new MediaElement();
            mediaElement.Source = new Uri(getPath());
        }

        public MediaElement getMediaElement()
        {
            return mediaElement;
        }
    }
}