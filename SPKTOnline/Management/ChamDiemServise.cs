using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChamDiem;
using SPKTOnline.Models;
using ChamDiem.Managers;
using System.Web.Configuration;

namespace SPKTOnline.Management
{
    public class ChamDiemServise
    {
        OnlineSPKTEntities1 db = new OnlineSPKTEntities1();

        public KetQuaThiSinh ChamBai(int MaBai, String SourceCode, string NgonNgu)
        {
            Problem problem = db.Problems.FirstOrDefault(m => m.ID == MaBai);
            if (problem == null)
                throw new Exception("Ma bai khong ton tai");
            List<ITestCase> tescase = new List<ITestCase>();
            foreach (ITestCase t in problem.TestCases)
            {
                //TODO: config
                t.TimeOut = (int)(problem.TimeLimit == null ? 1000 : problem.TimeLimit);
                tescase.Add(t);
            }
            IFileComparer ss = FileComparerFactory.GetComparer(problem.Comparer.DllPath, problem.Comparer.ClassName);
            ss.Init(problem.ComparerParameter);

            String executionDir =HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["ExecutionDir"]);
            BienDichCPP.ApplicationFolder = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["VisualCCompilerDir"]);
            ChamBaiManager chamManager = new ChamBaiManager(NgonNgu, executionDir);
            KetQuaThiSinh ketQua = chamManager.ChamBai(SourceCode, tescase, ss);
            return ketQua;
        }
    }
}