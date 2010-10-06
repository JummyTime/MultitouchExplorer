using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using libSMARTMultiTouch.Input;
using libSMARTMultiTouch.Table;
using libSMARTMultiTouch.Controls;
using libSMARTMultiTouch.Behaviors;
using ExplorerLib;
using ExplorerLib.ContentTypes;

namespace ExplorerGUIMike
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private DraggableBorder interactiveBorder;
        private BehaviorBoundMapToScreen boundMap;
        public Window1()
        {
            InitializeComponent();

            TableManager.Initialize(this, LayoutRoot);
            TableManager.IsFullScreen = true;
            try
            {
                Explorer explorer = new Explorer("../../../Sample.xml");
                ExplorerContentMap rootMap = explorer.getRootContentMap();
                Canvas c = new Canvas();
                DraggableBackgroundMap backgroundMap = new DraggableBackgroundMap(rootMap.getImage());
                c.Children.Add(backgroundMap);
                TouchInputManager.AddTouchContactDownHandler(interactiveBorder, touchDown);
                LayoutRoot.Children.Add(c);

            }
            catch (Exception e)
            {
                System.Console.WriteLine("Got exception: " + e);
            }

            //LayoutRoot.Children.Add(new TableControl());

           


            
        }

        public  void touchDown(object sender, libSMARTMultiTouch.Input.TouchContactEventArgs e)
        {
            Point p = interactiveBorder.PointFromScreen(e.TouchContact.Position);
            Console.WriteLine("Down at: " + p.X + ", " + p.Y);
        }
    }
}
