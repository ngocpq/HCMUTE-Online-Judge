using System;
using System.Collections.Generic;
using System.Text;

namespace ChamDiem.Exceptions
{
    public class RunFileException:ChamThiException
    {
        public RunFileException()
            : base()
        { }
        public RunFileException(string message)
            : base(message)
        { }
        public RunFileException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
