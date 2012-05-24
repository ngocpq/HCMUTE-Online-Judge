using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChamDiem;
using SPKTOnline.Models;
using ChamDiem.Managers;
using System.Web.Configuration;
using System.Threading;

namespace SPKTOnline.Management
{    
    public delegate void ChamThiServiceEventHandler(object sender,KetQuaThiSinh kq);

    public class ChamDiemServise
    {
        OnlineSPKTEntities db = new OnlineSPKTEntities();

        public event ChamThiServiceEventHandler ChamThiCompleted;

        //class ThamSo
        //{
        //    public int MaBai { get; set; }
        //    public String SourceCode { get; set; }
        //    public string NgonNgu { get; set; }
        //}

        public void ChamBaiThread(Student_Submit st)
        {                                    
            Thread t = new Thread(new ParameterizedThreadStart(Start));
            t.Start(st);
            
        }

        void Start(object obj)
        {
            Student_Submit st = (Student_Submit)obj;
            KetQuaThiSinh kq = ChamBai(st.ProblemID, st.SourceCode, st.Language.Name);
            kq.SubmitID = st.ID;
            if (ChamThiCompleted != null)
                ChamThiCompleted(this, kq);
        }
        //public KetQuaThiSinh ChamBai(int submitID,int MaBai, String SourceCode, string NgonNgu)
        //{
        //    KetQuaThiSinh kq = ChamBai(MaBai, SourceCode, NgonNgu);
        //    kq.SubmitID = submitID;
        //    return kq;
        //}
        public KetQuaThiSinh ChamBai(int MaBai, String SourceCode, string NgonNgu)
        {            
            Problem problem = db.Problems.FirstOrDefault(m => m.ID == MaBai);
            if (problem == null)
                throw new Exception("Ma bai khong ton tai");
            List<ITestCase> tescase = new List<ITestCase>();
            foreach (ITestCase t in problem.TestCases)
            {
                //TODO: config
                t.TimeOut = (problem.TimeLimit <= 0? 1000 : problem.TimeLimit);                
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