using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows.Controls;

using libSMARTMultiTouch;
using libSMARTMultiTouch.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using libSMARTMultiTouch.Input;

using ExplorerLib.ContentTypes;
using ExplorerLib.Exceptions;

namespace ExplorerLib
{
    public class ClickBehavior : Behavior<InteractiveFrameworkElement>
    {
        private FrameworkElement m_element;
        //private Dictionary<int, IDProperties> dictIDProperties = new Dictionary<int, IDProperties>();

        protected override void OnAttached()
        {
            base.OnAttached();
            m_element = base.AssociatedObject.FrameworkElement;
            ((InteractiveBorder)m_element).StylusButtonDown += new StylusButtonEventHandler(Point_Touch_Confirm);
            //((InteractiveBorder)m_element).TouchDown += new TouchContactEventHandler(Point_TouchUp_Confirm);
        }

        public void Point_Touch_Confirm(object sender, EventArgs e)
        {
            /*Point point = new Point();
            bool IsStylusCaptured = ((InteractiveBorder)m_element).IsStylusCaptured;
            IDProperties idProperties = this.GetID(((TouchContactEventArgs)e).TouchContact.ID);
            point = ((TouchContactEventArgs)e).TouchContact.GetPosition((InteractiveBorder)sender);
            Console.Write(point.X);
            Console.Write(point.Y);
            Console.Write(point.ToString());
            System.Console.WriteLine("Normal is the Watchword");*/
        }
       /* public void Point_TouchDown_Confirm(object sender, EventArgs e)
        {

        }*/

        protected override void OnDetaching()
        {

        }
        #region Utilities

        // Given a mouse ID:
        // - create a dictionary entry storing IDProperties by mouse ID if we don't already have one
        // - return the IDProperties object that stores status information associated with that ID
       /* private IDProperties GetID(int mouseID)
        {
            // If we have the mouseID, return the corrected one
            if (dictIDProperties.ContainsKey(mouseID))
            {
                return dictIDProperties[mouseID];
            }
            else
            {
                // We don't have it. Add a new ID Property then return it
                IDProperties idProperties = new IDProperties(mouseID);
                dictIDProperties.Add(mouseID, idProperties);
                return idProperties;
            }
        }*/
        #endregion

    }

}