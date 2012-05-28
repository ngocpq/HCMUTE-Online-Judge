using System;
using System.Collections.Generic;
using System.Web;

namespace ChamDiem.SoSanh
{
    public class SoSanhException:Exception
    {
        public SoSanhException() { }
        public SoSanhException(string message)
            : base(message)
        { }
        public SoSanhException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}