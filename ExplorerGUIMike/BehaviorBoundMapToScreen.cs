using System;
using System.Windows;
using System.Windows.Interactivity;
using libSMARTMultiTouch;
using System.Windows.Forms;
using Application = System.Windows.Application;

namespace ExplorerGUIMike
{
    public class BehaviorBoundMapToScreen : Behavior<InteractiveFrameworkElement>
    {
        private readonly double imageWidth;
        private readonly double imageHeight;

        public BehaviorBoundMapToScreen(double image_width, double image_height)
        {
            imageWidth = image_width;
            imageHeight = image_height;
        }
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.RotateTransformUpdated += AssociatedObject_RotateTransformUpdated;
            AssociatedObject.TranslateTransformUpdated += AssociatedObject_TranslateTransformUpdated;
            
            double widthMinScale = Screen.PrimaryScreen.Bounds.Width / imageWidth;
            double heightMinScale = Screen.PrimaryScreen.Bounds.Height / imageHeight;
            double minScaleAmount;
            if (widthMinScale > heightMinScale) minScaleAmount = widthMinScale;
            else minScaleAmount = heightMinScale;
            AssociatedObject.MinScale = minScaleAmount;
            AssociatedObject.TranslateTransform.X = -imageWidth / 2 + Screen.PrimaryScreen.Bounds.Width / 2;
            AssociatedObject.TranslateTransform.Y = -imageHeight / 2 + Screen.PrimaryScreen.Bounds.Height / 2;
            AssociatedObject.ScaleTransform.ScaleX = minScaleAmount;
            AssociatedObject.ScaleTransform.ScaleY = minScaleAmount;
        }

        void AssociatedObject_TranslateTransformUpdated(object sender, EventArgs e)
        {
            double currentWidth = imageWidth*AssociatedObject.ScaleTransform.ScaleX;
            double currentHeight = imageHeight * AssociatedObject.ScaleTransform.ScaleY;
            Point position = AssociatedObject.FrameworkElement.TranslatePoint(new Point(),
                                                                              Application.Current.MainWindow);

            AssociatedObject.TranslateTransformUpdated -= AssociatedObject_TranslateTransformUpdated;

            if (position.X > 0) AssociatedObject.TranslateTransform.X  -= position.X;
            if (position.Y > 0) AssociatedObject.TranslateTransform.Y -= position.Y;

            double totalBufferX = (currentWidth - Screen.PrimaryScreen.Bounds.Width);
            double totalBufferY = (currentHeight - Screen.PrimaryScreen.Bounds.Height);
            if (position.X < -totalBufferX) AssociatedObject.TranslateTransform.X -= position.X + totalBufferX;
            if (position.Y < -totalBufferY) AssociatedObject.TranslateTransform.Y -= position.Y + totalBufferY;

            AssociatedObject.TranslateTransformUpdated += AssociatedObject_TranslateTransformUpdated;
        }
           

        void AssociatedObject_RotateTransformUpdated(object sender, EventArgs e)
        {
            AssociatedObject.RotateTransformUpdated -= new EventHandler(AssociatedObject_RotateTransformUpdated);
            AssociatedObject.RotateTransform.Angle = 0;
            AssociatedObject.RotateTransformUpdated +=new EventHandler(AssociatedObject_RotateTransformUpdated);
            
        }




    }
}
