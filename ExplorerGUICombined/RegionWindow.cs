using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ExplorerLib;
using libSMARTMultiTouch.Controls;
using libSMARTMultiTouch.Input;

namespace ExplorerGUICombined
{
    public class RegionWindow : DraggableBorder
    {
        private ExplorerRegion mapRegion;

        public delegate void HandleRegionClose(RegionWindow window);
        public event HandleRegionClose OnRegionClose;

        public RegionWindow(ExplorerRegion map_region)
        {
            mapRegion = map_region;

            TouchCanvas canvas = new TouchCanvas();
            TextBox title = new TextBox();
            title.Text = map_region.getName();
            title.FontSize = 16;
            canvas.Children.Add(title);
            DraggableBorder db1 = new DraggableBorder();
            DraggableBorder db2 = new DraggableBorder();
            DraggableBorder db3 = new DraggableBorder();
            InteractiveBorder ib = new InteractiveBorder();
            canvas.Background = new SolidColorBrush(Colors.WhiteSmoke);
            db1.Background = new SolidColorBrush(Colors.SlateBlue);
            db2.Background = new SolidColorBrush(Colors.SlateGray);
            db3.Background = new SolidColorBrush(Colors.Tomato);
            ib.Background = new SolidColorBrush(Colors.Red);
            canvas.Width = 300;
            canvas.Height = 200;
            db1.Width = 50;
            db1.Height = 50;
            db2.Width = 50;
            db2.Height = 50;
            db3.Width = 50;
            db3.Height = 50;
            ib.Width = 30;
            ib.Height = 30;
            canvas.Children.Add(db1);
            canvas.Children.Add(db2);
            canvas.Children.Add(db3);
            canvas.Children.Add(ib);
            Canvas.SetLeft(db1, 25);
            Canvas.SetTop(db1, 100);
            Canvas.SetLeft(db2, 100);
            Canvas.SetTop(db2, 100);
            Canvas.SetLeft(db3, 175);
            Canvas.SetTop(db3, 100);
            Canvas.SetTop(ib, 0);
            Canvas.SetRight(ib, 0);

            ib.TouchDown += new TouchContactEventHandler(ib_TouchDown);

            Child = canvas;

        }

        void ib_TouchDown(object sender, TouchContactEventArgs e)
        {
            if(OnRegionClose != null)
            {
                OnRegionClose(this);
            }
        }
    }
}
