using System;
using System.Collections.Generic;
using System.Text;
using ChamDiem;

namespace BienDich_Cham
{
    class TestCase : ITestCase
    {
        public string Input { get; set; }

        public string Output { get; set; }

        public int TimeOut { get; set; }

        public TestCase(string inp, string outp, int time)
        {
            Input = inp;
            Output = outp;
            TimeOut = time;
        }
    }
}
