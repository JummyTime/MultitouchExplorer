using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ExplorerLib;
using ExplorerLib.ContentTypes;
using libSMARTMultiTouch.Controls;
using Image = System.Windows.Controls.Image;

namespace ExplorerGUICombined
{
    public class DraggableBackgroundMap : DraggableBorder
    {
        private Canvas parentCanvas;
        private ExplorerContentMap parentMap;
        public DraggableBackgroundMap(ExplorerContentMap parent_map)
        {
            parentMap = parent_map;
            Image mapImage = parentMap.getImage();

            parentCanvas = new Canvas();
            parentCanvas.Background = new ImageBrush(mapImage.Source);
            parentCanvas.MinWidth = mapImage.Source.Width;
            parentCanvas.MaxWidth = mapImage.Source.Width;
            parentCanvas.MinHeight = mapImage.Source.Height;
            parentCanvas.MaxHeight = mapImage.Source.Height;

            Child = parentCanvas;
            IsRSTEnabled = true;
            IsRotateEnabled = false;
            IsScaleEnabled = true;
            IsFlickEnabled = false;
            IsStayInboundsEnabled = false;

            BehaviorSingleClickHelper clickHelper = new BehaviorSingleClickHelper();
            clickHelper.OnSingleClick += clickHelper_OnSingleClick;
            Attach(clickHelper);

            Attach( new BehaviorBoundMapToScreen(mapImage.Source.Width, mapImage.Source.Height));

            foreach(ExplorerRegion region in parent_map.getRegions())
            {
                Console.WriteLine("Adding " + region.getName());
                Polygon regionPolygon = new Polygon();
                foreach(ExplorerPoint point in region.getPoints())
                {
                    regionPolygon.Points.Add(new Point(point.getX(), point.getY()));
                }

                regionPolygon.Fill = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));
                parentCanvas.Children.Add(regionPolygon);
            } 
        }

        void clickHelper_OnSingleClick(Point original)
        {
            ExplorerPoint explorerPoint = new ExplorerPoint(original.X, original.Y);
            ExplorerRegion region = parentMap.getRegionContainingPoint(explorerPoint);
            if(region != null)
            {
                Console.WriteLine(region.getName());
                RegionWindow window = new RegionWindow(region);
                window.OnRegionClose += new RegionWindow.HandleRegionClose(window_OnRegionClose);
                Canvas.SetLeft(window, original.X + 5);
                Canvas.SetTop(window, original.Y + 5);
                parentCanvas.Children.Add(window);
            }
            else
            {

                Ellipse ellipse = new Ellipse();
                ellipse.Width = 10;
                ellipse.Height = 10;
                ellipse.Stroke = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
                Canvas.SetLeft(ellipse, original.X - 5);
                Canvas.SetTop(ellipse, original.Y - 5);
                parentCanvas.Children.Add(ellipse);

            }

            Console.WriteLine(ScaleTransform.ScaleX + ", " + ScaleTransform.ScaleY);
            Console.WriteLine("<point x=\"" + original.X + "\" y=\"" + original.Y + "\" />");
             

        }

        void window_OnRegionClose(RegionWindow window)
        {
            parentCanvas.Children.Remove(window);
        }

    }
}
