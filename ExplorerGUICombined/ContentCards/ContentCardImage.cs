using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ExplorerLib.ContentTypes;

namespace ExplorerGUICombined.ContentCards
{
    public class ContentCardImage : ContentCardBase
    {
        private ExplorerContentImage explorerImage;
        public ContentCardImage(Canvas parent_canvas, Color highlight_color, ExplorerContentImage explorer_image)
            : base(parent_canvas, highlight_color)
        {
            explorerImage = explorer_image;

            StackPanel background = new StackPanel();
            background.Orientation = Orientation.Vertical;
            background.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            background.Margin = new Thickness(5);

            Image image = explorerImage.getImage();
            image.MaxWidth = 300;
            image.MaxHeight = 400;
            image.Margin = new Thickness(0, 0, 0, 5);
            background.Children.Add(image);

            Label caption = new Label();
            caption.Content = explorerImage.getName();
            caption.FontSize = 20;
            caption.MaxWidth = 300;
            caption.FontWeight = FontWeights.Bold;
            background.Children.Add(caption);


            setContent(background);
        }

        public ExplorerContentImage getExplorerImage()
        {
            return explorerImage;
        }

        public override List<ContentCardBase> getCardContents()
        {
            List<ContentCardBase> contents = new List<ContentCardBase>();
            contents.Add(this); //Add myself, as if I am dragged back into the dock
            return contents;
        }

        public override Image getCardIcon()
        {
            return explorerImage.getImage();
        }

        public override string getCardIconText()
        {
            return explorerImage.getShortName();
        }
    }
}
