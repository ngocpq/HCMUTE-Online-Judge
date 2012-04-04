using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ChamDiem
{
  public class BienDichFactory
  {
    //public IBienDich GetBienDichObjectByTenFile(String FilePath)
    //{
    //  string ext = Path.GetExtension(FilePath);
    //  return GetBienDichObjectByNgonNgu(ext);
    //}
    public IBienDich GetBienDichObjectByNgonNgu(String Language)
    {
      switch (Language.Trim().ToLower())
      { 
        case "cpp":
        case "c++":
        case "c":
          return new BienDichCPP();
        case "pascal":
        case "pas":
          return new BienDichPascal();
        case "java":
          return new BienDichJava();
        default:
          return null;
      }
    }

  }
}
