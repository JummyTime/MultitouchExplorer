using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using libSMARTMultiTouch.Controls;

namespace ExplorerGUICombined
{
    public class Dock : InteractiveBorder
    {
        private GUIConfiguration guiConfig;
        private bool dockInUse;
        private List<ContentCardBase> contents = new List<ContentCardBase>();

        private StackPanel emptyDockContents;
        public Dock(GUIConfiguration gui_config)
        {
            guiConfig = gui_config;

            emptyDockContents = new StackPanel();
            emptyDockContents.Orientation = Orientation.Horizontal;
            emptyDockContents.Margin = new Thickness(5, 5, 5, 5);

            TextBox dropToAdd = new TextBox();
            dropToAdd.Background = new SolidColorBrush(Colors.Transparent);
            dropToAdd.BorderBrush = new SolidColorBrush(Colors.Transparent);
            dropToAdd.Text = "Drop Here To Add";
            dropToAdd.Foreground = new SolidColorBrush(Colors.White);
            dropToAdd.FontSize = 18;
            dropToAdd.VerticalAlignment = VerticalAlignment.Center;
            emptyDockContents.Children.Add(dropToAdd);

            Image iconImage = new Image();
            iconImage.Source = guiConfig.getAddToDockImage();
            iconImage.Width = 40;
            iconImage.Height = 40;
            iconImage.Margin = new Thickness(5,0,5,0);
            emptyDockContents.Children.Add(iconImage);

            MinHeight = 80;
            MaxHeight = 80;
            Background = new SolidColorBrush(Color.FromArgb(200, 0, 0, 0));
            BorderBrush = new SolidColorBrush(Colors.White);
            BorderThickness = new Thickness(2, 2, 2, 0);
            Child = emptyDockContents;
        }

        public bool isDockInUse()
        {
            return dockInUse;
        }

        public void addToDock(ContentCardBase card)
        {

            StackPanel dockCanvas = new StackPanel();
            dockCanvas.Orientation = Orientation.Horizontal;
            dockCanvas.Margin = new Thickness(5, 5, 5, 5);
            contents.AddRange(card.getCardContents());
            if(contents.Count > 0)
            {

                dockInUse = true;
                foreach (ContentCardBase childCard in contents)
                {
                    StackPanel currentIcon = new StackPanel();
                    Image icon = childCard.getCardIcon();
                    icon.MaxHeight = 50;
                    icon.MinHeight = 50;
                    icon.MaxWidth = 75;
                    icon.Margin = new Thickness(5, 0, 5, 0);
                    currentIcon.Children.Add(icon);

                    TextBox iconLabel = new TextBox();
                    iconLabel.Background = new SolidColorBrush(Colors.Transparent);
                    iconLabel.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    iconLabel.Foreground = new SolidColorBrush(Colors.White);
                    iconLabel.Text = childCard.getCardIconText();
                    iconLabel.FontSize = 10;
                    iconLabel.MaxWidth = 75;
                    iconLabel.MaxLines = 2;
                    iconLabel.HorizontalAlignment = HorizontalAlignment.Center;

                    currentIcon.Children.Add(iconLabel);
                    dockCanvas.Children.Add(currentIcon);
                }

                Child = dockCanvas;
            }
            else
            {
                dockInUse = false;
                Child = emptyDockContents;
            }

            
        }
        
    }
}
