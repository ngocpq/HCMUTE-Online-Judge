using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ChamDiem;
using SPKTOnline.Models;
using SPKTOnline.Management;

namespace SPKTOnline
{
    /// <summary>
    /// Summary description for ServiceChamBai
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ServiceChamBai : System.Web.Services.WebService
    {
        OnlineSPKTEntities1 db = new OnlineSPKTEntities1();

        [WebMethod]
        public KetQuaThiSinh ChamBai(int MaBai, String SourceCode, string NgonNgu)
        {
            Problem problem = db.Problems.FirstOrDefault(m => m.ID == MaBai);
            if (problem == null)
                throw new Exception("Ma bai khong ton tai");
            KetQuaThiSinh ketQua = new KetQuaThiSinh();

            //Bien Dich Source Code:
            IBienDich bienDich = (new BienDichFactory()).GetBienDichObjectByNgonNgu(NgonNgu);
            //TODO: lay ten file
            string ExeDir = "Compile";

            DateTime now = DateTime.Now;
            string fileName = MaBai.ToString() + now.ToString("yyMMddHHmmss") + now.Millisecond.ToString() + ".exe";
            String exeFilePath = System.IO.Path.Combine(Server.MapPath(ExeDir), fileName);
            KetQuaBienDich kqBienDich = bienDich.BienDich(SourceCode, exeFilePath);
            ketQua.KetQuaBienDich = kqBienDich;

            //TODO: Luu ket qua bien dich vao DB
            if (kqBienDich.BienDichThanhCong == false)
            {
                ketQua.KetQuaCham = null;
                return ketQua;
            }
            //Cham diem de lay ket qua cham
            IChamDiem chamDiem = new ChamEXE();
            List<ITestCase> tescase = new List<ITestCase>();
            ITestCase tc = problem.TestCases.First();
            foreach (ITestCase t in problem.TestCases)
            {
                t.TimeOut = (int)(problem.TimeLimit == null ? 1000 : problem.TimeLimit);
                tescase.Add(t);
            }
            //Tinh tong 2 so nguyen
            //tescase.Add(new TestCase("1 1\n", "2", 1000));
            //tescase.Add(new TestCase("2000 300\n", "2300", 1000));
            //tescase.Add(new TestCase("421 234\n", "655", 1000));
            //tescase.Add(new TestCase("4 1\n", "5", 1000));
            //tescase.Add(new TestCase("3000 1\n", "3001", 1000));
            //tescase.Add(new TestCase("5002 1000\n", "6102", 1000));
            //tescase.Add(new TestCase("11 12\n", "23", 1000));

            //IFileComparer ss = new SoSanhSoNguyen(); //SoSanhExternal();
            IFileComparer ss = FileComparerFactory.GetComparer(problem.Comparer.DllPath, problem.Comparer.ClassName);
            ss.Init(problem.ComparerParameter);
            //            
            // Cham
            KetQuaCham kqCham = chamDiem.Cham(exeFilePath, tescase, ss);
            ketQua.KetQuaCham = kqCham;
            return ketQua;
        }
    }
}
