using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using ExplorerLib;
using ExplorerLib.ContentTypes;

namespace ExplorerGUICombined.ContentCards
{
    public class ContentCardRegion : ContentCardBase
    {
        private Canvas baseCanvas = new Canvas();
        private ExplorerRegion mapRegion;
        public ContentCardRegion(Canvas parent_canvas, Color highlight_color, ExplorerRegion map_region): base(parent_canvas, highlight_color)
        {
            mapRegion = map_region;

            StackPanel background = new StackPanel();
            background.Orientation = Orientation.Horizontal;
            background.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            background.Margin = new Thickness(3);
            StackPanel detailsSide = new StackPanel();
            detailsSide.Orientation = Orientation.Vertical;

            Label title = new Label();
            title.Content = map_region.getName();
            title.FontSize = 15;
            title.MaxWidth = 150;
            title.FontWeight = FontWeights.Bold;
            detailsSide.Children.Add(title);
            
            ExplorerContentText descriptionContentText = mapRegion.getDefaultContentText();
            if (descriptionContentText != null)
            {
                TextBox desc = new TextBox();
                desc.TextWrapping = TextWrapping.Wrap;
                desc.BorderThickness = new Thickness(0);
                desc.Text = descriptionContentText.getText();
                desc.FontSize = 10;
                desc.MaxWidth = 150;
                detailsSide.Children.Add(desc);
            }

            background.Children.Add(detailsSide);
            ExplorerContentImage defaultContentImage = map_region.getDefaultContentImage();
            if (defaultContentImage != null)
            {
                Image defaultImage = defaultContentImage.getImage();
                defaultImage.MaxHeight = 50;
                defaultImage.MinHeight = 50;
                defaultImage.Margin = new Thickness(5, 0, 0, 0);
                background.Children.Add(defaultImage);
            }


            setContent(background);
        }

        public ExplorerRegion getExplorerRegion()
        {
            return mapRegion;
        }

        public override List<ContentCardBase> getCardContents()
        {
            List<ContentCardBase> contents = new List<ContentCardBase>();
            foreach(ExplorerContentBase contentItem in mapRegion.getContentsDictionary().Values)
            {
                if(contentItem is ExplorerContentImage)
                {
                    contents.Add(new ContentCardImage(parentCanvas, highlightColor, (ExplorerContentImage)contentItem));
                }
            }

            return contents;
        }

        public override Image getCardIcon()
        {
            throw new NotImplementedException();
        }

        public override string getCardIconText()
        {
            throw new NotImplementedException();
        }
    }
}
