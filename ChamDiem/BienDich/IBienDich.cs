using System;
using System.Collections.Generic;
using System.Text;

namespace ChamDiem
{
    public delegate void BienDichHandler(object sender,KetQuaBienDich e);
    public interface IBienDich
    {
        KetQuaBienDich BienDich(String FileSourcePath, String ResultFilePath);
        //event BienDichHandler BienDichCompleted;
    }
}
