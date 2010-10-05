using System;
using System.Xml;
using ExplorerLib.Exceptions;

namespace ExplorerLib
{
    public class ExplorerPoint
    {
        private double x;
        private double y;

        public ExplorerPoint(XmlNode point_node)
        {
            foreach (XmlAttribute childAttribute in point_node.Attributes)
            {
                if (childAttribute.Name == "x")
                {
                    try
                    {
                        x = Convert.ToDouble(childAttribute.Value);
                    }
                    catch (FormatException ex)
                    {
                        throw new ExplorerParseXMLException("X is invalid for point '" + point_node.OuterXml + "'", ex);
                    }
                    catch (OverflowException ex)
                    {
                        throw new ExplorerParseXMLException("X is overflowed for point '" + point_node.OuterXml + "'",
                                                            ex);
                    }
                }
                else if (childAttribute.Name == "y")
                {
                    try
                    {
                        y = Convert.ToDouble(childAttribute.Value);
                    }
                    catch (FormatException ex)
                    {
                        throw new ExplorerParseXMLException("Y is invalid for point '" + point_node.OuterXml + "'", ex);
                    }
                    catch (OverflowException ex)
                    {
                        throw new ExplorerParseXMLException("Y is overflowed for point '" + point_node.OuterXml + "'",
                                                            ex);
                    }
                }
            }
        }

        public double getX()
        {
            return x;
        }

        public double getY()
        {
            return y;
        }
    }
}