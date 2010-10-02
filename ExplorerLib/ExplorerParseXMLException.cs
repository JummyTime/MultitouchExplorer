using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExplorerLib
{
    public class ExplorerParseXMLException : Exception
    {
        public ExplorerParseXMLException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
    
}
