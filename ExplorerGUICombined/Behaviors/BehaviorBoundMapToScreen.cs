using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using libSMARTMultiTouch;
using System.Windows.Forms;
using Application = System.Windows.Application;

namespace ExplorerGUICombined.Behaviors
{
    public class BehaviorBoundMapToScreen : Behavior<InteractiveFrameworkElement>
    {
        private readonly double imageWidth;
        private readonly double imageHeight;
        private readonly Canvas rootScreenCanvas;
        public BehaviorBoundMapToScreen(Canvas root_screen_canvas, double image_width, double image_height)
        {
            rootScreenCanvas = root_screen_canvas; 
            imageWidth = image_width;
            imageHeight = image_height;
        }
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.TranslateTransformUpdated += AssociatedObject_TranslateTransformUpdated;
            
            double widthMinScale = Screen.PrimaryScreen.Bounds.Width / imageWidth;
            double heightMinScale = Screen.PrimaryScreen.Bounds.Height / imageHeight;
            double minScaleAmount;
            if (widthMinScale > heightMinScale) minScaleAmount = widthMinScale;
            else minScaleAmount = heightMinScale;
            AssociatedObject.MinScale = minScaleAmount;
            AssociatedObject.TranslateTransform.X = -imageWidth / 2.0 + rootScreenCanvas.ActualWidth / 2.0;
            AssociatedObject.TranslateTransform.Y = -imageHeight / 2.0 + rootScreenCanvas.ActualHeight / 2.0;
            AssociatedObject.ScaleTransform.ScaleX = minScaleAmount;
            AssociatedObject.ScaleTransform.ScaleY = minScaleAmount;
        }

        void AssociatedObject_TranslateTransformUpdated(object sender, EventArgs e)
        {
            double currentWidth = imageWidth*AssociatedObject.ScaleTransform.ScaleX;
            double currentHeight = imageHeight * AssociatedObject.ScaleTransform.ScaleY;
            Point position = AssociatedObject.FrameworkElement.TranslatePoint(new Point(),
                                                                             rootScreenCanvas);

            AssociatedObject.TranslateTransformUpdated -= AssociatedObject_TranslateTransformUpdated;

            if (position.X > 0) AssociatedObject.TranslateTransform.X  -= position.X;
            if (position.Y > 0) AssociatedObject.TranslateTransform.Y -= position.Y;

            double totalBufferX = (currentWidth - rootScreenCanvas.ActualWidth);
            double totalBufferY = (currentHeight - rootScreenCanvas.ActualHeight);
            if (position.X < -totalBufferX) AssociatedObject.TranslateTransform.X -= position.X + totalBufferX;
            if (position.Y < -totalBufferY) AssociatedObject.TranslateTransform.Y -= position.Y + totalBufferY;

            AssociatedObject.TranslateTransformUpdated += AssociatedObject_TranslateTransformUpdated;
        }
    }
}
