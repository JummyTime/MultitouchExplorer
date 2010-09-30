using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ExplorerLib
{
    public class ExplorerContentVideo : ExplorerContentMediaBase
    {
        public ExplorerContentVideo(XmlNode video_node) : base(video_node)
        {
        }
    }
}
