using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ExplorerLib
{
    public class ExplorerContentText : ExplorerContentBase
    {
        private String textContent;
        public ExplorerContentText(XmlNode text_node) : base(text_node)
        {
            textContent = text_node.InnerText;
        }

        public String getTextContent()
        {
            return textContent;
        }
    }
}
