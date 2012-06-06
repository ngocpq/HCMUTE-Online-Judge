using System;
using System.Collections.Generic;
using System.Text;
using ChamDiem.BienDich;

namespace ChamDiem
{
  public class BienDichPascal:BienDichBase, IBienDich
  {    

    public override string CompilerFile
    {
        get { throw new NotImplementedException(); }
    }

    public override string GetCompileOption(string sourceFile, string exeFile)
    {
        throw new NotImplementedException();
    }
  }
}
