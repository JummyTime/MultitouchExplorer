using System;

namespace ExplorerLib
{
    public class ExplorerLoadXMLException : Exception
    {
        public ExplorerLoadXMLException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
