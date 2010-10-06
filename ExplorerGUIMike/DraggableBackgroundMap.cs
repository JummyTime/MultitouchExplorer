using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ExplorerGUIMike;
using libSMARTMultiTouch.Controls;

namespace ExplorerGUIMike
{
    public class DraggableBackgroundMap : DraggableBorder
    {
        private Image mapImage; 
        public DraggableBackgroundMap(Image map_image)
        {
            mapImage = map_image;
            Child = mapImage;
            IsRSTEnabled = false;
            IsRNTEnabled = true;
            IsRotateEnabled = false;
            IsFlickEnabled = false;
            Attach( new BehaviorBoundMapToScreen(mapImage.Source.Width, mapImage.Source.Height));
        }
    }
}
