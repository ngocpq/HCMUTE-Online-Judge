using System;
using System.Collections.Generic;
using System.Text;

namespace ChamDiem
{
    public class RunResult
    {
        public enum ResultState
        {
            Success = 0,            
            Timeout = 1,
            Error = 2
        }
        public int ExitCode { get; set; }
        public ResultState Result{get;set;}
        public string Output { get; set; }
        public string Error { get; set; }
    }
}
