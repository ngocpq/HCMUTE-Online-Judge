using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ChamDiem
{
  public class ChamBaiFactory
  {
    public IChamDiem GetChamDiemObject(string file)
    {
      switch (Path.GetExtension(file).ToLower())
      { 
        case "exe":
          return new ChamEXE();
        case "class":
          return new ChamJava();
        default:
          return null;
      }
    }
  }
}
