using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ChamDiem.Managers
{
    public class ChamBaiManager
    {
        FileManager _FileManager;
        IBienDich _BienDich;
        IChamDiem _ChamDiem;
        private string NgonNgu;
        
        public ChamBaiManager(string baseDir, IBienDich bienDich, IChamDiem chamDiem)
        {
            _FileManager = new FileManager(baseDir);
            _BienDich = bienDich;
            _ChamDiem = chamDiem;
        }

        public ChamBaiManager(string NgonNgu,string baseDir)
        {
            _FileManager = new FileManager(baseDir);
            _BienDich= (new BienDichFactory()).GetBienDichObjectByNgonNgu(NgonNgu);
            _ChamDiem= new ChamEXE();                                    
        }

        //public KetQuaThiSinh ChamBai(IInputCham input)
        //{
        //    BienDichFactory cpFac = new BienDichFactory();
        //    IBienDich compiler = cpFac.GetBienDichObjectByNgonNgu(input.Language);
        //    KetQuaThiSinh kq = new KetQuaThiSinh();
        //    kq.Input = input;
        //    String outFileName = Path.GetFileNameWithoutExtension(input.SourceCode) + ".exe";
        //    string outFilePath = Path.Combine(Path.GetDirectoryName(input.SourceCode), outFileName);

        //    kq.KetQuaBienDich = compiler.BienDich(input.SourceCode, outFilePath);

        //    if (kq.KetQuaBienDich.BienDichThanhCong)
        //    {
        //        ChamBaiFactory chamFac = new ChamBaiFactory();
        //        IChamDiem chamDiem = chamFac.GetChamDiemObject(outFilePath);
        //        kq.KetQuaCham = chamDiem.Cham(outFilePath, input.TestCases, input.FileComparer);
        //    }
        //    return kq;
        //}

        public KetQuaThiSinh ChamBai(string sourceCode, List<ITestCase> tescase, IFileComparer ss)
        {            
            //_FileManager = new FileManager();
            //TODO: get code ext
            String codeFilePath = _FileManager.GetNextTempFile(".cpp");            
            StreamWriter f = File.CreateText(codeFilePath);
            f.Write(sourceCode);
            f.Close();
            string exeFilePath = Path.ChangeExtension(codeFilePath, ".exe");
            KetQuaThiSinh kq = new KetQuaThiSinh();

            kq.KetQuaBienDich = _BienDich.BienDich(codeFilePath, exeFilePath);
            if (!kq.KetQuaBienDich.BienDichThanhCong)
                return kq;
            kq.KetQuaCham = _ChamDiem.Cham(kq.KetQuaBienDich.FilePath, tescase, ss);
            return kq;
        }
    }
}
