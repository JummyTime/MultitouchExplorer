using System;

namespace ExplorerLib.Exceptions
{
    public class ExplorerParseXMLException : ExplorerException
    {
        public ExplorerParseXMLException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}