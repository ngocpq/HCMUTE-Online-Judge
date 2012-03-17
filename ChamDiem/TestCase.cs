using System;
using System.Collections.Generic;
using System.Text;

namespace ChamDiem
{
  public interface ITestCase
  {
    string Input { get; set; }
    string Output { get; set; }
    int TimeOut { get; set; }
  }
}
