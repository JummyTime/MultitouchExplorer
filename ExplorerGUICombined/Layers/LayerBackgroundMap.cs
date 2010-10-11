using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ExplorerGUICombined.Behaviors;
using ExplorerGUICombined.ContentCards;
using ExplorerLib;
using ExplorerLib.ContentTypes;
using libSMARTMultiTouch.Controls;
using Image = System.Windows.Controls.Image;

namespace ExplorerGUICombined.Layers
{
    public class LayerBackgroundMap : DraggableBorder
    {
        private TouchCanvas parentCanvas;
        private ExplorerContentMap parentMap;

        private Dictionary<ExplorerRegion,Polygon> highlightedRegions = new Dictionary<ExplorerRegion, Polygon>(); 

        public delegate void RegionClick(ExplorerRegion region, Color highlight_color, Point position);
        public event RegionClick OnRegionClick;

        public LayerBackgroundMap(Canvas parent_canvas, ExplorerContentMap parent_map)
        {
            parent_canvas.Children.Add(this);

            parentMap = parent_map;
            Image mapImage = parentMap.getImage();

            parentCanvas = new TouchCanvas();
            parentCanvas.Background = new ImageBrush(mapImage.Source);
            parentCanvas.MinWidth = mapImage.Source.Width;
            parentCanvas.MaxWidth = mapImage.Source.Width;
            parentCanvas.MinHeight = mapImage.Source.Height;
            parentCanvas.MaxHeight = mapImage.Source.Height;

            Child = parentCanvas;
            IsRSTEnabled = true;
            IsRotateEnabled = false; //Prevent rotation since bounds algo doesnt take that into consideration
            IsScaleEnabled = true;
            IsFlickEnabled = false; //Prevent and twitchy behavior
            IsStayInboundsEnabled = false; //Prevent the shifting when zoomed in
            IsMoveToTopOnTouchEnabled = false; //Prevent the layer from coming above other layers

            BehaviorSingleClickHelper clickHelper = new BehaviorSingleClickHelper();
            clickHelper.OnSingleClick += clickHelper_OnSingleClick;

            Attach(clickHelper);
            Attach(new BehaviorBoundMapToScreen(parent_canvas, mapImage.Source.Width, mapImage.Source.Height));
        }

        void clickHelper_OnSingleClick(Point relative_point, Point screen_point)
        {
            ExplorerPoint explorerPoint = new ExplorerPoint(relative_point.X, relative_point.Y);
            ExplorerRegion region = parentMap.getRegionContainingPoint(explorerPoint);
            if(region != null)
            {
                if(!highlightedRegions.ContainsKey(region))
                {   
                    Color randomColor = RandomColorManager.getRandomColor();
                    OnRegionClick(region, randomColor, relative_point);
                }
                
            }
            else
            {

                Ellipse ellipse = new Ellipse();
                ellipse.Width = 10;
                ellipse.Height = 10;
                ellipse.Stroke = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
                Canvas.SetLeft(ellipse, relative_point.X - 5);
                Canvas.SetTop(ellipse, relative_point.Y - 5);
                parentCanvas.Children.Add(ellipse);

                Console.WriteLine("<point x=\"" + relative_point.X + "\" y=\"" + relative_point.Y + "\" />");

            }

        }

        public void drawRegionOverlay(ExplorerRegion region, Color highlight_color, Point touch_relative_point)
        {
            Polygon highlight = getRegionOverlay(region, highlight_color);

            highlightedRegions.Add(region, highlight);
            parentCanvas.Children.Add(highlight);
        }

        public void removeRegionOverlay(ExplorerRegion region)
        {
            if (highlightedRegions.ContainsKey(region))
            {
                parentCanvas.Children.Remove(highlightedRegions[region]);
                highlightedRegions.Remove(region);
            }
        }

        public void removeRegionOverlay(ContentCardBase card)
        {
            if (card is ContentCardRegion)
            {
                //Card offscreen is region, get rid of the region
                removeRegionOverlay(((ContentCardRegion)card).getExplorerRegion());
            };
        }

        private Polygon getRegionOverlay(ExplorerRegion region, Color color)
        {
            Polygon regionPolygon = new Polygon();
            foreach (ExplorerPoint point in region.getPoints())
            {
                regionPolygon.Points.Add(new Point(point.getX(), point.getY()));
            }

            regionPolygon.Stroke = new SolidColorBrush(color);
            regionPolygon.StrokeThickness = 5;

            return regionPolygon;
        }

    }
}
