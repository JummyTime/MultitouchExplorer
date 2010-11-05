using System;
using System.Xml;

namespace ExplorerLib.ContentTypes
{
    public class ExplorerContentText : ExplorerContentBase
    {
        private String textContent;

        public ExplorerContentText(ExplorerConfiguration configuration, XmlNode text_node)
            : base(configuration, text_node)
        {
            textContent = text_node.InnerText;
        }

        public String getText()
        {
            return textContent;
        }
    }
}