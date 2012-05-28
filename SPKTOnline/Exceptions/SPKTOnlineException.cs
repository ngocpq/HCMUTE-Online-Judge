using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPKTOnline.Exceptions
{
    public class SPKTOnlineException : Exception
    {
        public SPKTOnlineException() { }
        public SPKTOnlineException(string message)
            : base(message)
        { }
        public SPKTOnlineException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}