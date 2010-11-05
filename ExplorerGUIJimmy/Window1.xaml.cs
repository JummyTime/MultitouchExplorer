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

namespace MultitouchExplorer
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// 
    /// </summary>
    public partial class Window1 : Window
    {
        private TouchCanvas PrimaryCanvas = new TouchCanvas();
        private RSTBehavior rnt = new RSTBehavior();
        private ExplorerGUIJimmy.ExplorerClickBehavior ecb = new ExplorerGUIJimmy.ExplorerClickBehavior();
        private Explorer explorer = new Explorer("../../../Config.xml", "../../../Sample.xml");

        public Window1()
        {
            InitializeComponent();

            TableManager.Initialize(this, LayoutRoot);
            try
            {
                //Explorer explorer = new Explorer("../../../Sample.xml");
                ExplorerContentMap rootMap = explorer.getRootContentMap();
                InteractiveBorder interactiveBorder = new InteractiveBorder();
                interactiveBorder.Attach(rnt);
                interactiveBorder.Attach(ecb);
                ecb.singleclick += new TouchContactEventHandler(singleClick);
                interactiveBorder.Child = rootMap.getImage();
                //LayoutRoot.Children.Add(interactiveBorder);
                LayoutRoot.Children.Add(PrimaryCanvas);
                PrimaryCanvas.Background = new SolidColorBrush(Colors.Transparent);
                PrimaryCanvas.Children.Add(interactiveBorder);
                interactiveBorder.Width = 1440;
                interactiveBorder.Height = 900;
                //Console.WriteLine("Normal is the Watchword");
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Got exception: " + e);
            }

            //LayoutRoot.Children.Add(new TableControl());
            TableManager.IsFullScreen = true;    
        }
        public void singleClick(object sender, TouchContactEventArgs e)
        {
            Console.WriteLine("This only happends when the single click event is satisfied");
            Point point = new Point();
            point = ((TouchContactEventArgs)e).TouchContact.GetPosition((InteractiveBorder)sender);
            Console.WriteLine(point.ToString());
            Get_Region_For_Single_Click(point);
        }
        public void Get_Region_For_Single_Click(Point p)
        {
            TouchCanvas tc = new TouchCanvas();
            DraggableBorder db1 = new DraggableBorder();
            DraggableBorder db2 = new DraggableBorder();
            DraggableBorder db3 = new DraggableBorder();
            InteractiveBorder ib = new InteractiveBorder();
            tc.Background = new SolidColorBrush(Colors.WhiteSmoke);
            db1.Background = new SolidColorBrush(Colors.SlateBlue);
            db2.Background = new SolidColorBrush(Colors.SlateGray);
            db3.Background = new SolidColorBrush(Colors.Tomato);
            ib.Background = new SolidColorBrush(Colors.Red);
            tc.Width = 300;
            tc.Height = 200;
            db1.Width = 50;
            db1.Height = 50;
            db2.Width = 50;
            db2.Height = 50;
            db3.Width = 50;
            db3.Height = 50;
            ib.Width = 10;
            ib.Height = 10;
            tc.Children.Add(db1);
            tc.Children.Add(db2);
            tc.Children.Add(db3);
            tc.Children.Add(ib);
            Canvas.SetLeft(db1, 25);
            Canvas.SetTop(db1, 100);
            Canvas.SetLeft(db2, 100);
            Canvas.SetTop(db2, 100);
            Canvas.SetLeft(db3, 175);
            Canvas.SetTop(db3, 100);
            Canvas.SetTop(ib, 0);
            Canvas.SetRight(ib, 0);
            PrimaryCanvas.Children.Add(tc);
            Canvas.SetLeft(tc, p.X);
            Canvas.SetTop(tc, p.Y);
            TouchInputManager.AddTouchContactDownHandler(ib, new TouchContactEventHandler(Kill_Building_Popup));
        }
        public void Kill_Building_Popup(Object sender, TouchContactEventArgs e)
        {
            object c = ((InteractiveBorder)sender).Parent;
            PrimaryCanvas.Children.Remove((Canvas)c);
        }
    }
}
