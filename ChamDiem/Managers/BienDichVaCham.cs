using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ChamDiem
{
    public delegate void ChamError(object sender, KetQuaThiSinh kq);
    public delegate void ChamComplete(object sender, KetQuaThiSinh kq);

    public class BienDichVaCham
    {
        public event ChamError OnChamError;
        public event ChamComplete OnChamComplete;

        public KetQuaThiSinh ChamBai(IInputCham input)
        {
            BienDichFactory cpFac = new BienDichFactory();
            IBienDich compiler = cpFac.GetBienDichObjectByNgonNgu(input.Language);
            KetQuaThiSinh kq = new KetQuaThiSinh();
            //kq.Input = input;
            String outFileName = Path.GetFileNameWithoutExtension(input.SourceCode) + ".exe";
            string outFilePath = Path.Combine(Path.GetDirectoryName(input.SourceCode), outFileName);

            kq.KetQuaBienDich = compiler.BienDich(input.SourceCode, outFilePath);
            
            if (kq.KetQuaBienDich.BienDichThanhCong)
            {
                ChamBaiFactory chamFac = new ChamBaiFactory();
                IChamDiem chamDiem = chamFac.GetChamDiemObject(outFilePath);
                kq.KetQuaCham = chamDiem.Cham(outFilePath, input.TestCases, input.FileComparer);
            }
            return kq;
        }

        public KetQuaThiSinh ChamBaiAsysn(IInputCham input)
        {
            throw new Exception("Chua viet");
        }
    }
}
