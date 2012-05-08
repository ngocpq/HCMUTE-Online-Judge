using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ChamDiem;
using SPKTOnline.Models;
using SPKTOnline.Management;
using ChamDiem.Managers;
using System.Web.Configuration;

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

        
        [WebMethod(EnableSession=true)]
        public KetQuaThiSinh ChamBai(int MaBai, String SourceCode, string NgonNgu)
        {
            Problem problem = db.Problems.FirstOrDefault(m => m.ID == MaBai);
            if (problem == null)
                throw new Exception("Ma bai khong ton tai");            
            List<ITestCase> tescase = new List<ITestCase>();            
            foreach (ITestCase t in problem.TestCases)
            {
                //TODO: config
                t.TimeOut = (int)(problem.TimeLimit <= 0? 1000 : problem.TimeLimit);
                tescase.Add(t);
            }
            IFileComparer ss = FileComparerFactory.GetComparer(problem.Comparer.DllPath, problem.Comparer.ClassName);
            ss.Init(problem.ComparerParameter);

            String executionDir = Server.MapPath(WebConfigurationManager.AppSettings["ExecutionDir"]);
            BienDichCPP.ApplicationFolder = Server.MapPath(WebConfigurationManager.AppSettings["VisualCCompilerDir"]);
            ChamBaiManager chamManager = new ChamBaiManager(NgonNgu,executionDir);
            KetQuaThiSinh ketQua = chamManager.ChamBai(SourceCode, tescase, ss);            
            return ketQua;
            #region Bỏ

            //String exeFilePath = System.IO.Path.Combine(Server.MapPath(ExeDir), fileName);
            //KetQuaBienDich kqBienDich = bienDich.BienDich(SourceCode, exeFilePath);
            //ketQua.KetQuaBienDich = kqBienDich;

            //TODO: Luu ket qua bien dich vao DB
            //if (kqBienDich.BienDichThanhCong == false)
            //{
            //    ketQua.KetQuaCham = null;
            //    return ketQua;
            //}
            //Cham diem de lay ket qua cham

            //List<ITestCase> tescase = new List<ITestCase>();
            //ITestCase tc = problem.TestCases.First();
            //foreach (ITestCase t in problem.TestCases)
            //{
            //    t.TimeOut = (int)(problem.TimeLimit == null ? 1000 : problem.TimeLimit);
            //    tescase.Add(t);
            //}
            //IFileComparer ss = FileComparerFactory.GetComparer(problem.Comparer.DllPath, problem.Comparer.ClassName);
            //ss.Init(problem.ComparerParameter);

            // Cham
            //KetQuaCham kqCham = chamDiem.Cham(exeFilePath, tescase, ss);
            //ketQua.KetQuaCham = kqCham;
            //return ketQua; 
            #endregion
        }
    }
}
