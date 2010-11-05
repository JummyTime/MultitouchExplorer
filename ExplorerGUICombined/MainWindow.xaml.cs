using System;
using System.Windows;
using System.Windows.Controls;
using ExplorerGUICombined.ContentCards;
using ExplorerGUICombined.Layers;
using libSMARTMultiTouch.Table;
using ExplorerLib;
using ExplorerLib.ContentTypes;

namespace ExplorerGUICombined
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LayerBackgroundMap backgroundMapLayer;
        private LayerContentCards contentCardsLayer;
        private LayerDocks dockLayer;


        private Canvas rootCanvas;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            TableManager.RunTable(this, LayoutRoot);
            TableManager.IsFullScreen = true;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            rootCanvas = new Canvas();
            rootCanvas.Loaded += rootCanvas_Loaded;
            LayoutRoot.Children.Add(rootCanvas);
        }

        void rootCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            Explorer explorer = new Explorer("../../../Config.xml", "../../../Sample.xml");
            ExplorerContentMap rootMap = explorer.getRootContentMap();

            backgroundMapLayer = new LayerBackgroundMap(rootCanvas, rootMap);
            contentCardsLayer = new LayerContentCards(rootCanvas);

            //Link some events up between the layers. We can enable and disable features this way
            backgroundMapLayer.OnRegionClick += contentCardsLayer.drawRegionWindow;
            backgroundMapLayer.OnRegionClick += backgroundMapLayer.drawRegionOverlay;
            //contentCardsLayer.OnContentCardTouchUp += dockLayer.checkDraggedOn;
            contentCardsLayer.OnContentCardOffscreen += backgroundMapLayer.removeRegionOverlay;
            contentCardsLayer.OnContentCardOffscreen += contentCardsLayer.removeContentCard;
           // dockLayer.OnContentCardDroppedOnDock += backgroundMapLayer.removeRegionOverlay;
           // dockLayer.OnContentCardDroppedOnDock += contentCardsLayer.removeContentCard;
           // contentCardsLayer.OnContentCardsHeldDown += dockLayer.showUnusedDocks;
           // contentCardsLayer.OnContentCardsReleased += dockLayer.hideUnusedDocks;
        }
    }
}
