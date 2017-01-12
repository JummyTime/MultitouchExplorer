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
        public Window1()
        {
            InitializeComponent();
            TableManager.RunTable(this, LayoutRoot);
            try
            {
                Explorer explorer = new Explorer("../../../Config.xml", "../../../Sample.xml");
                ExplorerContentMap rootMap = explorer.getRootContentMap();
                Canvas c = new Canvas();
                DraggableBackgroundMap backgroundMap = new DraggableBackgroundMap(rootMap);
                c.Children.Add(backgroundMap);
                LayoutRoot.Children.Add(c);

            }
            catch (Exception e)
            {
                System.Console.WriteLine("Got exception: " + e);
            }

            TableManager.IsFullScreen = true;
           


            
        }
    }
}
