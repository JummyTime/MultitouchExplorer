using System;

namespace ExplorerLib.Exceptions
{
    public class ExplorerLoadXMLException : ExplorerException
    {
        public ExplorerLoadXMLException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}