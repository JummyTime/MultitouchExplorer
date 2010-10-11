using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using libSMARTMultiTouch.Input;

namespace ExplorerGUICombined.Layers
{
    public class LayerDocks
    {
        private Canvas parentCanvas;
        private GUIConfiguration guiConfig;

        private Dock topDock, bottomDock;

        public delegate void ContentCardDroppedOnDock(ContentCardBase card);
        public event ContentCardDroppedOnDock OnContentCardDroppedOnDock;

        public LayerDocks(Canvas parent_canvas, GUIConfiguration gui_config)
        {
            parentCanvas = parent_canvas;
            guiConfig = gui_config;

            bottomDock = new Dock(guiConfig);
            bottomDock.Loaded += bottomDock_Loaded;
            bottomDock.SizeChanged += bottomDock_SizeChanged;
            parentCanvas.Children.Add(bottomDock);

            topDock = new Dock(guiConfig);
            topDock.Loaded += topDock_Loaded;
            topDock.SizeChanged += topDock_SizeChanged;
            parentCanvas.Children.Add(topDock);
            

        }

        void bottomDock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas.SetLeft(bottomDock, parentCanvas.ActualWidth / 2.0 - bottomDock.ActualWidth / 2.0);
        }

        void topDock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas.SetLeft(topDock, parentCanvas.ActualWidth / 2.0 - topDock.ActualWidth / 2.0);
        }

        private void bottomDock_Loaded(object sender, RoutedEventArgs e)
        {
            bottomDock.RotateTransform.Angle = 0;
            Canvas.SetBottom(bottomDock, -bottomDock.ActualHeight);
            Canvas.SetLeft(bottomDock, parentCanvas.ActualWidth / 2.0 - bottomDock.ActualWidth / 2.0);
        }

        private void topDock_Loaded(object sender, RoutedEventArgs e)
        {
            topDock.RotateTransform.Angle = 180;
            Canvas.SetTop(topDock, -topDock.ActualHeight);
            Canvas.SetLeft(topDock, parentCanvas.ActualWidth / 2.0 - topDock.ActualWidth / 2.0);
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
            if (!topDock.isDockInUse()) topDock.AnimateTranslate(0, topDock.ActualHeight, .8, .2, new TimeSpan(0, 0, 0, 0, 400));
            if (!bottomDock.isDockInUse()) bottomDock.AnimateTranslate(0, -bottomDock.ActualHeight, .8, .2, new TimeSpan(0, 0, 0, 0, 400));
        }

        public void hideUnusedDocks()
        {

            if(!topDock.isDockInUse()) topDock.AnimateTranslate(0, -topDock.ActualHeight, .8, .2, new TimeSpan(0, 0, 0, 0, 400));
            if(!bottomDock.isDockInUse()) bottomDock.AnimateTranslate(0, bottomDock.ActualHeight, .8, .2, new TimeSpan(0, 0, 0, 0, 400));
            
        }



    }


}
