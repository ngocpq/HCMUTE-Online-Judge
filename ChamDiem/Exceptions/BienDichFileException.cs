using System;
using System.Collections.Generic;
using System.Text;

namespace ChamDiem.Exceptions
{
    public class BienDichFileException : ChamThiException
    {
        public BienDichFileException()
            : base()
        { }
        public BienDichFileException(string message)
            : base(message)
        { }
        public BienDichFileException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
