using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ExplorerLib;
using ExplorerLib.ContentTypes;
using libSMARTMultiTouch.Controls;
using libSMARTMultiTouch.Input;
using Image = System.Windows.Controls.Image;

namespace ExplorerGUIMike
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
            IsRSTEnabled = false;
            IsRNTEnabled = true;
            IsRotateEnabled = false;
            IsFlickEnabled = false;

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
                    regionPolygon.Points.Add(new Point(point.getX() / 2 * ScaleTransform.ScaleX, point.getY() / 2 * ScaleTransform.ScaleY));
                }

                regionPolygon.Fill = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));
                parentCanvas.Children.Add(regionPolygon);

            } 
        }

        void clickHelper_OnSingleClick(Point original, Point scaled)
        {
            ExplorerPoint explorerPoint = new ExplorerPoint(scaled.X, scaled.Y);
            ExplorerRegion region = parentMap.getRegionContainingPoint(explorerPoint);
            if(region != null)
            {
                Console.WriteLine(region.getName());
            }

            
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 6;
            ellipse.Height = 6;
            Canvas.SetLeft(ellipse, original.X - 3);
            Canvas.SetTop(ellipse, original.Y - 3);
            parentCanvas.Children.Add(ellipse);
            Console.WriteLine("<point x=\"" + scaled.X + "\" y=\"" + scaled.Y + "\" />");
             

        }

    }
}
