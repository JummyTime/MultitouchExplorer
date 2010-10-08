using System;
using System.Windows;
using System.Windows.Controls;
using libSMARTMultiTouch.Table;
using ExplorerLib;
using ExplorerLib.ContentTypes;

namespace ExplorerGUICombined
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private BackgroundMapLayer backgroundMapLayer;
        private RegionWindowsLayer regionWindowLayer;

        public Window1()
        {
            InitializeComponent();
            TableManager.RunTable(this, LayoutRoot);
            try
            {
                Explorer explorer = new Explorer("../../../Sample.xml");
                ExplorerContentMap rootMap = explorer.getRootContentMap();

                Canvas c = new Canvas();
                backgroundMapLayer = new BackgroundMapLayer(c, rootMap);
                regionWindowLayer = new RegionWindowsLayer(c, backgroundMapLayer);

                LayoutRoot.Children.Add(c);

            }
            catch (Exception e)
            {
                Console.WriteLine("Got exception: " + e);
            }

            TableManager.IsFullScreen = true;
        }
    }
}
