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

namespace ExplorerGUIJimmy
{
    public class ExplorerClickBehavior : Behavior<InteractiveFrameworkElement>
    {
        private FrameworkElement m_element;
        private Dictionary<int, IDProperties> dictIDProperties = new Dictionary<int, IDProperties>();
        public event TouchContactEventHandler singleclick;
        
        protected override void OnAttached()
        {
            base.OnAttached();
            m_element = base.AssociatedObject.FrameworkElement;
            ((InteractiveBorder)m_element).TouchDown += new TouchContactEventHandler(Point_TouchDown_Confirm);
            ((InteractiveBorder)m_element).TouchUp += new TouchContactEventHandler(Point_TouchUp_Confirm);
            //Console.WriteLine("Normal is the Watchword: Welcome to Click Behavior OnAttach()");
        }

        public void Point_TouchDown_Confirm(object sender, TouchContactEventArgs e)
        {
            Point point = new Point();
            point = ((TouchContactEventArgs)e).TouchContact.GetPosition((InteractiveBorder)sender);
            IDProperties idProperties = this.GetID(((TouchContactEventArgs)e).TouchContact.ID, point.X + point.Y, 12);
            //Console.WriteLine(point.X);
            //Console.WriteLine(point.Y);
            //Console.WriteLine(point.ToString());
            //Console.WriteLine("Normal is the Watchword: Welcome to Click Behavior Click Event");
            //Console.WriteLine(((TouchContactEventArgs)e).TouchContact.ID);

        }
        public void Point_TouchUp_Confirm(object sender, TouchContactEventArgs e)
        {
            Point point = new Point();
            point = ((TouchContactEventArgs)e).TouchContact.GetPosition((InteractiveBorder)sender);
            dictIDProperties[((TouchContactEventArgs)e).TouchContact.ID].distance = dictIDProperties[((TouchContactEventArgs)e).TouchContact.ID].distance - point.X - point.Y;
            if (Math.Abs(dictIDProperties[((TouchContactEventArgs)e).TouchContact.ID].distance) < 10)
            {
                if (singleclick != null)
                {
                    singleclick(sender, e);
                }
                Console.WriteLine("Normal is the Watchword: Single Click Event");
               
                
            }
            //IDProperties idProperties = this.GetID(((TouchContactEventArgs)e).TouchContact.ID,point.X + point.Y,12);
            //Console.WriteLine(point.X);
            //Console.WriteLine(point.Y);
            //Console.WriteLine(point.ToString());
            //Console.WriteLine("Normal is the Watchword: Welcome to Click Behavior Click Event");
            //Console.WriteLine(((TouchContactEventArgs)e).TouchContact.ID);
            Console.WriteLine(dictIDProperties[((TouchContactEventArgs)e).TouchContact.ID].distance);
            //Console.WriteLine(dictIDProperties[((TouchContactEventArgs)e).TouchContact.ID].ID);
        }

        protected override void OnDetaching()
        {

        }
        #region Utilities

        // Given a mouse ID:
        // - create a dictionary entry storing IDProperties by mouse ID if we don't already have one
        // - return the IDProperties object that stores status information associated with that ID
        private IDProperties GetID(int mouseID,double distance,double contactTime)
        {
            // If we have the mouseID, return the corrected one
            if (dictIDProperties.ContainsKey(mouseID))
            {
                dictIDProperties[mouseID].distance = distance;
                return dictIDProperties[mouseID];
            }
            else
            {
                // We don't have it. Add a new ID Property then return it
                IDProperties idProperties = new IDProperties(mouseID,distance,contactTime);
                dictIDProperties.Add(mouseID, idProperties);
                return idProperties;
            }
        }
        #endregion

    }

}