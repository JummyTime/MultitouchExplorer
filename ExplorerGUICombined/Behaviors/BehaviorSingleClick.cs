using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;
using libSMARTMultiTouch;
using Application = System.Windows.Application;

namespace ExplorerGUICombined.Behaviors
{
    public class BehaviorSingleClickHelper : Behavior<InteractiveFrameworkElement>
    {
        private struct ClickResources
        {
            public Point startPoint;
            public DateTime startTime;
        }

        private Dictionary<int, ClickResources> activeClicks = new Dictionary<int, ClickResources>();
        public delegate void HandleSingleClickOnMap(Point relative_point, Point screen_point);

        public event HandleSingleClickOnMap OnSingleClick;
        private readonly Canvas rootScreenCanvas;
        public BehaviorSingleClickHelper(Canvas root_screen_canvas)
        {
            rootScreenCanvas = root_screen_canvas;
        }

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

                
                Point relativePoint = e.TouchContact.GetPosition(AssociatedObject.FrameworkElement);
                Point screenPoint = e.TouchContact.GetPosition(rootScreenCanvas);
                if((DateTime.Now - startTime).TotalMilliseconds < 500)
                {
                    if((screenPoint - startScreenPoint).Length < 5)
                    {
                        if (OnSingleClick != null)
                        {
                            OnSingleClick(relativePoint, screenPoint);
                        }
                    }
                   
                }
                
            }

           
        }
    }
}
