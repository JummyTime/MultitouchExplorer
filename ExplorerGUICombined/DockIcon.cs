using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using libSMARTMultiTouch.Controls;

namespace ExplorerGUICombined
{
    public class DockIcon : DraggableBorder
    {
        private ContentCardBase childCard;
        private Canvas parentCanvas;
        public DockIcon(Canvas parent_canvas, ContentCardBase card)
        {
            parentCanvas = parent_canvas;
            IsTouchBounceEnabled = true;
            IsStayInboundsEnabled = false;
            IsScaleEnabled = false;
            IsRotateEnabled = false;
            IsFlickEnabled = false;

            childCard = card;
            StackPanel currentIcon = new StackPanel();
            Image icon = childCard.getCardIcon();
            icon.MaxHeight = 50;
            icon.MinHeight = 50;
            icon.MaxWidth = 75;
            icon.Margin = new Thickness(5, 0, 5, 0);
            currentIcon.Children.Add(icon);

            TextBox iconLabel = new TextBox();
            iconLabel.Background = new SolidColorBrush(Colors.Transparent);
            iconLabel.BorderBrush = new SolidColorBrush(Colors.Transparent);
            iconLabel.Foreground = new SolidColorBrush(Colors.White);
            iconLabel.Text = childCard.getCardIconText();
            iconLabel.FontSize = 10;
            iconLabel.MaxWidth = 75;
            iconLabel.MaxLines = 2;
            iconLabel.HorizontalAlignment = HorizontalAlignment.Center;

            currentIcon.Children.Add(iconLabel);
            Child = currentIcon;
        }
    }
}
