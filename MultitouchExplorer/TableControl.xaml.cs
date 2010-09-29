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
using System.Windows.Shapes;
using libSMARTMultiTouch.Controls;

namespace MultitouchExplorer
{
    /// <summary>
    /// Interaction logic for TableControl.xaml
    /// </summary>
    public partial class TableControl : TableApplicationControl
    {
        private Canvas canvas = new Canvas();

        public TableControl()
        {
            canvas.Background = new SolidColorBrush(Colors.Transparent);
            InitializeComponent();
        }

        private void TableApplicationControl_Loaded(object sender, RoutedEventArgs e)
        {
            TableLayoutRoot.Children.Add(canvas);
        }
    }
}
