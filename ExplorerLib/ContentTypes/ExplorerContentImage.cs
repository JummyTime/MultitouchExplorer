using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml;

namespace ExplorerLib.ContentTypes
{
    public class ExplorerContentImage : ExplorerContentMediaBase
    {
        private Image image;

        public ExplorerContentImage(XmlNode image_node) : base(image_node)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri(getPath()));
            image = new Image();
            image.Source = bitmapImage;
        }

        public Image getImage()
        {
            return image;
        }
    }
}