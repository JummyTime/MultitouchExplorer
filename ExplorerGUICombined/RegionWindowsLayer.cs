using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ExplorerLib;
using libSMARTMultiTouch.Controls;

namespace ExplorerGUICombined
{
    public class RegionWindowsLayer
    {
        private Canvas parentCanvas;
        private BackgroundMapLayer mapLayer;
        public RegionWindowsLayer(Canvas parent_canvas, BackgroundMapLayer map)
        {
            parentCanvas = parent_canvas;
            mapLayer = map;
            mapLayer.OnRegionClick += mapLayer_OnRegionClick;
        }

        void mapLayer_OnRegionClick(ExplorerRegion region, Color highlight_color, Point position)
        {
            RegionWindow window = new RegionWindow(region);
            window.BorderBrush = new SolidColorBrush(highlight_color);
            window.BorderThickness = new Thickness(5);
            Canvas.SetLeft(window, 0);
            parentCanvas.Children.Add(window);
        }

    }


}
