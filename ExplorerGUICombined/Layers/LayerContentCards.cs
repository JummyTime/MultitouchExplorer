using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ExplorerGUICombined.ContentCards;
using ExplorerLib;
using libSMARTMultiTouch.Input;

namespace ExplorerGUICombined.Layers
{
    public class LayerContentCards
    {
        private Canvas parentCanvas;
        private List<ContentCardBase> cardsHeldDown = new List<ContentCardBase>();

        public delegate void ContentCardTouchDown(ContentCardBase card, TouchContact touch_contact);
        public delegate void ContentCardTouchMove(ContentCardBase card, TouchContact touch_contact);
        public delegate void ContentCardTouchUp(ContentCardBase card, TouchContact touch_contact);
        public delegate void ContentCardHeldDown();
        public delegate void ContentCardReleased();
        public delegate void ContentCardOffscreen(ContentCardBase card);

        public event ContentCardTouchDown OnContentCardTouchDown;
        public event ContentCardTouchMove OnContentCardTouchMove;
        public event ContentCardTouchUp OnContentCardTouchUp;
        public event ContentCardHeldDown OnContentCardsHeldDown; //At least one content card held down
        public event ContentCardReleased OnContentCardsReleased; //At least one content card released
        public event ContentCardOffscreen OnContentCardOffscreen; //A content card just went offscreen and is not longer child of the root layer
        
        public LayerContentCards(Canvas parent_canvas)
        {
            parentCanvas = parent_canvas;
        }

        public void drawRegionWindow(ExplorerRegion region, Color highlight_color, Point screen_position)
        {
            ContentCardRegion regionCard = new ContentCardRegion(parentCanvas, highlight_color, region);
            regionCard.OnContentCardOffscreen += card_OnContentCardOffscreen;
            regionCard.TouchDown += card_TouchDown;
            regionCard.TouchUp += card_TouchUp;
            regionCard.TouchMove += card_TouchMove;
            Canvas.SetLeft(regionCard, screen_position.X + 10);
            Canvas.SetTop(regionCard, screen_position.Y + 10);
            parentCanvas.Children.Add(regionCard);
        }

        public void removeContentCard(ContentCardBase card)
        {
            parentCanvas.Children.Remove(card);
        }

        private void card_TouchMove(object sender, TouchContactEventArgs e)
        {
            if (OnContentCardTouchMove != null)
            {
                OnContentCardTouchMove((ContentCardBase)sender, e.TouchContact);
            }
        }

        private void card_TouchUp(object sender, TouchContactEventArgs e)
        {
            if (OnContentCardTouchUp != null)
            {
                OnContentCardTouchUp((ContentCardBase)sender, e.TouchContact);
            }

            if (cardsHeldDown.Contains((ContentCardBase)sender))
            {
                cardsHeldDown.Remove((ContentCardBase)sender);
                if (cardsHeldDown.Count == 0 && OnContentCardsReleased != null)
                {
                    OnContentCardsReleased();
                }
            }
        }

        private void card_TouchDown(object sender, TouchContactEventArgs e)
        {
            if(OnContentCardTouchDown != null)
            {
                OnContentCardTouchDown((ContentCardBase)sender, e.TouchContact);
            }

            if (!cardsHeldDown.Contains((ContentCardBase)sender))
            {
                cardsHeldDown.Add((ContentCardBase)sender);

                if (cardsHeldDown.Count == 1 && OnContentCardsHeldDown != null)
                {
                    OnContentCardsHeldDown();
                }           

            }

        }

        private void card_OnContentCardOffscreen(ContentCardBase card)
        {
            if (OnContentCardOffscreen != null)
            {
                OnContentCardOffscreen(card);
            }
        }

    }


}
