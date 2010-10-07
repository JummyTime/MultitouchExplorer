using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace TouchApplication
{
    // Associates some basic properties with an ID:
    // -Color
    // -Brush
    // -ID
    // You can easily extend this class to add other properties (e.g., the mouse-owner's name), or to adjust the defaults

    class IDProperties
    {
        private Color myColor;
        private Brush myBrush;
        private int myID;

        // Constructor. Set a few defaults
        public IDProperties (int ID)
        {
            this.ID = ID;
            this.Color = Colors.Orange;
            this.Brush = Brushes.Orange;
        }

        public int ID
        {
            get { return myID; }
            set { myID = this.GetCorrectedID(value); }
        }
        
        public Color Color 
        { 
            get { return myColor; } 
            set { myColor = value; } 
        }

        public Brush Brush
        {
            get { return myBrush; }
            set { myBrush = value; }
        }

        #region Static Utilities
        private static int idCounter = 0;
        private static Dictionary<int, int> dictIDs = new Dictionary<int, int>();        // Given an mouse ID, find and record its equivalent zero-based ID (or return it if we have it)
        // The emulator does not guarantee that mice will be numbered from 0, 1, etc. 
        // This corrects the ID to always make it zero-based, where each new ID gets the next number
        private int GetCorrectedID(int mouseID)
        {
            // If we have the mouseID, return the corrected one
            if (IDProperties.dictIDs.ContainsKey(mouseID))
            {
                return IDProperties.dictIDs[mouseID];
            }
            else
            {
                // We don't have it. Add it, where the corrected ID is the current
                // value of the mouseCounter (which was initially 0) then return it
                IDProperties.dictIDs.Add(mouseID, IDProperties.idCounter);
                IDProperties.idCounter++;
                return IDProperties.dictIDs[mouseID];
            }
        }
        #endregion
    }
}
