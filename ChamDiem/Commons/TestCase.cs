using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ChamDiem
{
  public interface ITestCase//:ISerializable
  {
    string Input { get; set; }
    string Output { get; set; }
    int TimeOut { get; set; }
  }
}
