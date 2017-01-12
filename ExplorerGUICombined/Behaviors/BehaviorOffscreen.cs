using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;
using libSMARTMultiTouch;

namespace ExplorerGUICombined.Behaviors
{
    public class BehaviorOffscreen : Behavior<InteractiveFrameworkElement>
    {
        private Canvas rootScreenCanvas;

        public delegate void Offscreen();
        public event Offscreen OnOffscreen;

        public BehaviorOffscreen(Canvas root_screen_canvas)
        {
            rootScreenCanvas = root_screen_canvas;
        }
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.TranslateTransformUpdated +=AssociatedObject_TranslateTransformUpdated;
        }

        private void AssociatedObject_TranslateTransformUpdated(object sender, System.EventArgs e)
        {
            if(OnOffscreen != null)
            {
                try
                {
                    GeneralTransform transform = AssociatedObject.FrameworkElement.TransformToVisual(rootScreenCanvas);
                    Rect boundingBox =
                        transform.TransformBounds(new Rect(0, 0, AssociatedObject.FrameworkElement.ActualWidth,
                                                           AssociatedObject.FrameworkElement.ActualHeight));

                    if (boundingBox.Bottom <= 5 || boundingBox.Top >= rootScreenCanvas.ActualHeight - 5 ||
                        boundingBox.Right <= 5 || boundingBox.Left >= rootScreenCanvas.ActualWidth - 5)
                    {
                        OnOffscreen();
                    }
                }
                catch(Exception)
                {
                    Console.WriteLine("Exception caught in behavior offscreen. Just ignore.");
                }
            }
        }
    }
}
