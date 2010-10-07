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
        public Window1()
        {
            InitializeComponent();
            
            TableManager.Initialize(this, LayoutRoot);
            try
            {
                Explorer explorer = new Explorer("../../../Sample.xml");
                ExplorerContentMap rootMap = explorer.getRootContentMap();
                InteractiveBorder interactiveBorder = new InteractiveBorder();
                ExplorerGUIJimmy.ExplorerClickBehavior ecb = new ExplorerGUIJimmy.ExplorerClickBehavior();
                //ExplorerCl
                RSTBehavior rnt = new RSTBehavior();
                interactiveBorder.Attach(rnt);
                interactiveBorder.Attach(ecb);
                
                ecb.singleclick += new TouchContactEventHandler(singleClick);
                interactiveBorder.Child = rootMap.getImage();
                LayoutRoot.Children.Add(interactiveBorder);
                //Console.WriteLine("Normal is the Watchword");
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Got exception: " + e);
            }

            //LayoutRoot.Children.Add(new TableControl());
            TableManager.IsFullScreen = false;    
        }
        public void singleClick(object sender, TouchContactEventArgs e)
        {
            Console.WriteLine("This would make a nice stopping place");
        }
    }
}
