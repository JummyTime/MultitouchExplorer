using System;

namespace ExplorerLib.Exceptions
{
    public class ExplorerException : Exception
    {
        public ExplorerException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}