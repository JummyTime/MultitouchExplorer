using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using libSMARTMultiTouch.Input;

namespace ExplorerGUICombined.Layers
{
    public class LayerDocks
    {
        private Canvas parentCanvas;

        private Dock bottomDock;
        private List<DockIcon> iconsHeldDown = new List<DockIcon>();

        public delegate void ContentCardDroppedOnDock(ContentCardBase card);
        public event ContentCardDroppedOnDock OnContentCardDroppedOnDock;

        public LayerDocks(Canvas parent_canvas)
        {
            parentCanvas = parent_canvas;

            bottomDock = new Dock(parentCanvas);
            bottomDock.Loaded += bottomDock_Loaded;
            parentCanvas.Children.Add(bottomDock);

        }

        private void bottomDock_Loaded(object sender, RoutedEventArgs e)
        {
            bottomDock.RotateTransform.Angle = 45;
            bottomDock.TranslateTransform.X = parentCanvas.ActualWidth / 2.0 - bottomDock.ActualWidth / 2.0;
            bottomDock.TranslateTransform.Y = parentCanvas.ActualHeight - bottomDock.ActualHeight;
        }

        public void checkDraggedOn(ContentCardBase card, TouchContact touch_contact)
        {
            //Check if the window is ontop
            // Execute a hit test at the current location
            VisualTreeHelper.HitTest(parentCanvas,
                delegate(DependencyObject target)
                {
                    if (target is Dock)
                    {
                        Dock dockOver = target as Dock;
                        dockOver.addToDock(card);
                        if (OnContentCardDroppedOnDock != null)
                        {
                            OnContentCardDroppedOnDock(card);
                        }

                        return HitTestFilterBehavior.Stop;
                    }
                    return HitTestFilterBehavior.Continue;
                },
                delegate
                {
                    return HitTestResultBehavior.Continue;
                },
                new PointHitTestParameters(touch_contact.Position));
        }

        public void showUnusedDocks()
        {
            TimeSpan timeToShow = new TimeSpan(0, 0, 0, 0, 400);
            if (!bottomDock.isDockInUse()) bottomDock.TranslateTransform.Y -= bottomDock.ActualHeight;
        }

        public void hideUnusedDocks()
        {
            TimeSpan timeToShow = new TimeSpan(0, 0, 0, 0, 400);
            if (!bottomDock.isDockInUse()) bottomDock.TranslateTransform.Y += bottomDock.ActualHeight;
            
        }



    }


}
