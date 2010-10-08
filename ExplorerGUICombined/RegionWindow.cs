using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ExplorerLib;
using ExplorerLib.ContentTypes;
using libSMARTMultiTouch.Controls;

namespace ExplorerGUICombined
{
    public class RegionWindow : DraggableBorder
    {
        private ExplorerRegion mapRegion;

        public delegate void HandleRegionClose(RegionWindow window);

        public RegionWindow(ExplorerRegion map_region)
        {
            mapRegion = map_region;

            Canvas background = new Canvas();
            background.Background = new SolidColorBrush(Color.FromRgb(255,255,255));
            background.Width = 300;
            background.Height = 160;

            Label title = new Label();
            title.Content = map_region.getName();
            title.FontSize = 30;
            title.FontWeight = FontWeights.Bold;
            title.MaxWidth = 280;
            Canvas.SetLeft(title, 5);
            Canvas.SetTop(title, 5);
            background.Children.Add(title);

            ExplorerContentText descriptionContentText = mapRegion.getDefaultContentText();
            if(descriptionContentText != null)
            {
                TextBox desc = new TextBox();
                desc.TextWrapping = TextWrapping.Wrap;
                desc.BorderThickness = new Thickness(0);
                desc.Text = descriptionContentText.getText();
                desc.FontSize = 16;
                desc.MaxWidth = 275;
                Canvas.SetLeft(desc, 15);
                Canvas.SetTop(desc, 45);
                background.Children.Add(desc); 
            }
            
            ExplorerContentImage defaultContentImage = map_region.getDefaultContentImage();
            if (defaultContentImage != null)
            {

                background.Width = 500;
                Image defaultImage = defaultContentImage.getImage();
                Canvas.SetRight(defaultImage, 5);
                Canvas.SetTop(defaultImage, 5);
                defaultImage.MaxWidth = 250;
                defaultImage.MaxHeight = 150;
                defaultImage.MinHeight = 150;
                background.Children.Add(defaultImage);
            }

            Child = background;
            ScaleTransform.ScaleX = .75;
            ScaleTransform.ScaleY = .75;


        }
    }
}
