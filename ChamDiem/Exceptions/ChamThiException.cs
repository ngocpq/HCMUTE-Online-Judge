using System;
using System.Collections.Generic;
using System.Text;

namespace ChamDiem.Exceptions
{
    public class ChamThiException:Exception
    {
        public ChamThiException()
            : base()
        { }
        public ChamThiException(string message)
            : base(message)
        { }
        public ChamThiException(string message,Exception innerException)
            : base(message,innerException)
        { }

    }
}
