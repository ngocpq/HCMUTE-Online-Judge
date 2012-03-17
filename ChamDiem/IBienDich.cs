using System;
using System.Collections.Generic;
using System.Text;

namespace ChamDiem
{
  public interface IBienDich
  {
    KetQuaBienDich BienDich(String FileSourcePath, String ResultFilePath);    
  }
}
