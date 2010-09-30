using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ExplorerLib
{
    public class ExplorerContentText : ExplorerContentBase
    {
        public ExplorerContentText(XmlNode text_node) : base(text_node)
        {
        }
    }
}
