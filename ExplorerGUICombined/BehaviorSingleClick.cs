using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Interactivity;
using libSMARTMultiTouch;
using Application = System.Windows.Application;

namespace ExplorerGUICombined
{
    public class BehaviorSingleClickHelper : Behavior<InteractiveFrameworkElement>
    {
        private struct ClickResources
        {
            public Point startPoint;
            public DateTime startTime;
        }

        private Dictionary<int, ClickResources> activeClicks = new Dictionary<int, ClickResources>();
        public delegate void HandleSingleClickOnMap(Point original, Point scaled);

        public event HandleSingleClickOnMap OnSingleClick;

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.TouchDown += AssociatedObject_TouchDown;
            AssociatedObject.TouchUp += AssociatedObject_TouchUp;
        }

        void AssociatedObject_TouchDown(object sender, libSMARTMultiTouch.Input.TouchContactEventArgs e)
        {
            Point screenPoint = e.TouchContact.GetPosition(Application.Current.MainWindow);

            activeClicks.Remove(e.TouchContact.ID);
            activeClicks.Add(e.TouchContact.ID, new ClickResources() { startPoint = screenPoint, startTime = DateTime.Now });
        }
        void AssociatedObject_TouchUp(object sender, libSMARTMultiTouch.Input.TouchContactEventArgs e)
        {

            if(activeClicks.ContainsKey(e.TouchContact.ID))
            {
                Point startScreenPoint = activeClicks[e.TouchContact.ID].startPoint;
                DateTime startTime = activeClicks[e.TouchContact.ID].startTime;

                activeClicks.Remove(e.TouchContact.ID);


                if((DateTime.Now - startTime).TotalMilliseconds < 500)
                {
                    Point screenPoint = e.TouchContact.GetPosition(Application.Current.MainWindow);
                    if((screenPoint - startScreenPoint).Length < 5)
                    {
                        Point originalPoint = e.TouchContact.GetPosition(AssociatedObject.FrameworkElement);
                        Point scaledPoint = new Point();
                        scaledPoint.X = originalPoint.X / AssociatedObject.ScaleTransform.ScaleX * 2;
                        scaledPoint.Y = originalPoint.Y / AssociatedObject.ScaleTransform.ScaleY * 2;

                        if (OnSingleClick != null)
                        {
                            OnSingleClick(originalPoint, scaledPoint);
                        }
                    }
                   
                }
                
            }

           
        }
    }
}
