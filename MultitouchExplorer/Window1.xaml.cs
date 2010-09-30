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
using libSMARTMultiTouch.Table;
using ExplorerLib;

namespace MultitouchExplorer
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            TableManager.Initialize(this, LayoutRoot);
            LayoutRoot.Children.Add(new TableControl());
            TableManager.IsFullScreen = false;
            Explorer explorer = new Explorer("../../../Sample.xml");

            Console.WriteLine("Getting ALL Known Events:");
            foreach (ExplorerEvent childEvent in explorer.getRootContentMap().getChildEvents())
            {
                System.Console.WriteLine(childEvent.ToString());
            }
        }
    }
}
