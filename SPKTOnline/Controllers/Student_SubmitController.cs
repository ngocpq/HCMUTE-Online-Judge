﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPKTOnline.Models;
using SPKTOnline.Management;
using ChamDiem;

namespace SPKTOnline.Controllers
{
    public class Student_SubmitController : Controller
    {
        //
        // GET: /Student_Submit/
        OnlineSPKTEntities1 db = new OnlineSPKTEntities1();
        CheckRoles checkRole = new CheckRoles();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TryTest(int ID)
        {
            if (User.Identity.IsAuthenticated == true)
            {
                if (checkRole.IsStudent(User.Identity.Name))
                {
                    Problem p = db.Problems.FirstOrDefault(m => m.ID == ID);
                    Student_Summit st = new Student_Summit();
                    st.Problem = p;
                    return View(st);
                }
            }
            return RedirectToAction("Logon", "Home");
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TryTest(Student_Summit st)
        {
            if (User.Identity.IsAuthenticated == true)
            {
                if (checkRole.IsStudent(User.Identity.Name))
                {
                    st.StudentID = User.Identity.Name;
                    st.TrangThaiBienDich = 0;
                    st.TrangThaiCham = (int)TrangThaiCham.ChuaCham;
                    st.LanguageID = 1;
                    st.SubmitTime = DateTime.Now;
                    db.Student_Summit.AddObject(st);
                    
                    db.SaveChanges();
                    
                    ChamDiemServise chamThiService = new ChamDiemServise();                    
                    //Chay va doi
                    //                    
                    //KetQuaThiSinh kq = chamThiService.ChamBai(st.ProblemID, st.SourceCode, st.Language.Name);
                    //
                    //Chay ko doi
                    chamThiService.ChamThiCompleted += new ChamThiServiceEventHandler(chamThiService_ChamThiCompleted);
                    chamThiService.ChamBaiThread(st);
                    st.TrangThaiCham = (int)TrangThaiCham.DangCham;
                    db.SaveChanges();
                    return RedirectToAction("TryTestResult", "Result", new { Message = "Bạn đã gửi bài làm thành công" });//trả ra thông tin ở trang kết quả.
                }
            }
            return RedirectToAction("Logon", "Home");
        }

        void chamThiService_ChamThiCompleted(object sender, KetQuaThiSinh kq)
        {
            Student_Summit st = db.Student_Summit.FirstOrDefault(t => t.ID == kq.SubmitID);
            st.TrangThaiCham = (int)TrangThaiCham.DaCham;
            st.TrangThaiBienDich = kq.KetQuaBienDich.BienDichThanhCong?1:0;
            if(kq.KetQuaBienDich.BienDichThanhCong)
            {
                foreach(var rs in kq.KetQuaCham.KetQuaTestCases)
                {
                    TestCas tc =((TestCas)rs.TestCase);
                    TestCaseResult tcResult = new TestCaseResult();
                    tcResult.TestCaseID = tc.MaTestCase;
                    tcResult.StudentSubmitID = st.ID;
                    tcResult.Score = rs.KetQua == KetQuaTestCase.LoaiKetQua.Dung ? tc.Diem : 0;
                    tcResult.Comment = rs.ThongDiep;
                    //TODO: Them Error
                    //tcResult.Error = rs.Error;
                    db.TestCaseResults.AddObject(tcResult);                    
                }
                db.SaveChanges();
            }            

        }

    }
}
