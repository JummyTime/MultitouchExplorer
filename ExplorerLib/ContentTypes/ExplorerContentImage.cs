using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml;

namespace ExplorerLib.ContentTypes
{
    public class ExplorerContentImage : ExplorerContentMediaBase
    {
        private BitmapImage bitmapImage;

        public ExplorerContentImage(XmlNode image_node) : base(image_node)
        {

            bitmapImage = new BitmapImage(getPathUri());
        }

        public Image getImage()
        {
            Image image = new Image();
            image.Source = bitmapImage;
            return image;
        }
    }
}