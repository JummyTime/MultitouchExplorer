using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using libSMARTMultiTouch.Controls;
using libSMARTMultiTouch.Input;
using Image = System.Windows.Controls.Image;

namespace ExplorerGUIMike
{
    public class DraggableBackgroundMap : DraggableBorder
    {
        private Image mapImage;
        private Canvas parentCanvas;
        public DraggableBackgroundMap(Image map_image)
        {
            mapImage = map_image;

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
        }

        void clickHelper_OnSingleClick(Point original, Point scaled)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 10;
            ellipse.Height = 10;
            ellipse.Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            Canvas.SetLeft(ellipse, original.X - 5);
            Canvas.SetTop(ellipse, original.Y - 5);
            parentCanvas.Children.Add(ellipse);
        }

    }
}
