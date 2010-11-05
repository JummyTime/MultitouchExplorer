using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using libSMARTMultiTouch.Controls;
using libSMARTMultiTouch.Input;

namespace ExplorerGUICombined
{
    public class Dock : InteractiveBorder
    {
        private List<DockIcon> contents = new List<DockIcon>();

        private StackPanel emptyDockContents;
        private Canvas fullDockContentsFiller = new Canvas();
        private Canvas parentCanvas;

        public Dock(Canvas parent_canvas)
        {
            parentCanvas = parent_canvas;

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
            //iconImage.Source = "";// guiConfig.getAddToDockImage();
            iconImage.Width = 40;
            iconImage.Height = 40;
            iconImage.Margin = new Thickness(5,0,5,0);
            //emptyDockContents.Children.Add(iconImage);

            MinHeight = 80;
            MaxHeight = 80;
            Background = new SolidColorBrush(Color.FromArgb(200, 0, 0, 0));
            BorderBrush = new SolidColorBrush(Colors.White);
            BorderThickness = new Thickness(2, 2, 2, 0);
            Child = emptyDockContents;
            SizeChanged += Dock_SizeChanged;
        }

        void Dock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Console.WriteLine("SIZE CHANGED: " + e.PreviousSize + " -> " + e.NewSize);
            TranslateTransform.X += (e.PreviousSize.Width - e.NewSize.Width)/2.0;
            TranslateTransform.Y -= 100;
        }

        public bool isDockInUse()
        {
            return contents.Count > 0;
        }

        public void addToDock(ContentCardBase card)
        {
            List<ContentCardBase> cardContents = card.getCardContents();
            if(cardContents.Count > 0)
            {
                Child = fullDockContentsFiller;
                foreach (ContentCardBase childCard in cardContents)
                {
                    DockIcon icon = new DockIcon(parentCanvas, childCard);
                    icon.Loaded += icon_Loaded;
                    contents.Add(icon);
                    parentCanvas.Children.Add(icon);
                } 

            }
        }

        void icon_Loaded(object sender, RoutedEventArgs e)
        {
            double totalWidth = 0;
            foreach(DockIcon icon in contents)
            {
                totalWidth += icon.ActualWidth;
            }

            fullDockContentsFiller.Width = totalWidth;
        }
        
    }
}
