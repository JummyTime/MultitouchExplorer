using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ExplorerLib
{
    public class ExplorerContentImage : ExplorerContentMediaBase
    {
        public ExplorerContentImage(XmlNode image_node) : base(image_node)
        {
        }
    }
}
