using System;
using System.Collections.Generic;
using System.Text;

namespace ChamDiem
{
    public interface IInputCham
    {
        String Language { get; set; }
        String SourceCodeFile { get; set; }
        List<ITestCase> TestCases { get; set; }
        IFileComparer FileComparer { get; set; } // File dung de so sanh ket qua cham
    }
}
