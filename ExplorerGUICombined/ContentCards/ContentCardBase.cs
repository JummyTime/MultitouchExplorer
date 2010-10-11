using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using ExplorerGUICombined.Behaviors;
using libSMARTMultiTouch.Controls;

namespace ExplorerGUICombined
{
    public abstract class ContentCardBase : DraggableBorder
    {
        public delegate void ContentCardOffscreen(ContentCardBase card);
        public event ContentCardOffscreen OnContentCardOffscreen;

        protected Canvas parentCanvas;
        protected Color highlightColor;
        public ContentCardBase(Canvas parent_canvas, Color highlight_color)
        {
            parentCanvas = parent_canvas;
            highlightColor = highlight_color;
            BorderBrush = new SolidColorBrush(highlightColor);
            BorderThickness = new Thickness(5);

            IsStayInboundsEnabled = false;
            IsTouchBounceEnabled = true;
            Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            DropShadowEffect effect = new DropShadowEffect();
            effect.RenderingBias = RenderingBias.Performance;
            effect.ShadowDepth = 5;
            effect.Opacity = 0.8;
            Effect = effect;

            BehaviorOffscreen behaviorOffscreen = new BehaviorOffscreen(parentCanvas);
            behaviorOffscreen.OnOffscreen += behaviorOffscreen_OnOffscreen;
            Attach(behaviorOffscreen);
        }

        void behaviorOffscreen_OnOffscreen()
        {
            if (OnContentCardOffscreen != null)
            {
                OnContentCardOffscreen(this);
            }
        }

        protected void setContent(UIElement content)
        {
            Child = content;
        }

        public abstract List<ContentCardBase> getCardContents();
        public abstract Image getCardIcon();
        public abstract string getCardIconText();

    }
}
