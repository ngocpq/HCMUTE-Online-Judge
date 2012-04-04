using System;
using System.Collections.Generic;
using System.Text;
using ChamDiem;

namespace BienDich_Cham
{
    public class InputThiSinh : ChamDiem.IInputCham
    {
        public string SourceCode { get; set; }

        List<ChamDiem.ITestCase> _TestCases;
        public List<ChamDiem.ITestCase> TestCases
        {
            get { return _TestCases; }
            set { _TestCases = value; }
        }
        public IFileComparer FileComparer { get; set; }

        public string MaThiSinh { get; set; }

        public string Language { get; set; }
    }
}
