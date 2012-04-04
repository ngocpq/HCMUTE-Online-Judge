using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ChamDiem
{
    public interface IFileComparer
    {
        bool SoSanh(string output, ITestCase testcase, out string message);
        void Init(String parameter);
    }
    
}
