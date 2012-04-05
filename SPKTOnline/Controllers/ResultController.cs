using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPKTOnline.Models;
using SPKTOnline.Management;

namespace SPKTOnline.Controllers
{
    public class ResultController : Controller
    {
        //
        // GET: /Result/

        public ActionResult Index()
        {
            return View();
        }
        OnlineSPKTEntities1 db = new OnlineSPKTEntities1();
        CheckRoles checkRole = new CheckRoles();
        [ValidateInput(false)]
        public ActionResult TryTestAllResult(string Message)
        {
            string StudentName = User.Identity.Name;
            if (User.Identity.IsAuthenticated)
            {
                if (StudentName != null && checkRole.IsStudent(StudentName))
                {
                    var st = db.Student_Submit.Where(p => p.StudentID == StudentName);
                    List<Student_Submit> list = new List<Student_Submit>();
                    foreach (var s in st)
                    {
                        list.Add(s);
                    }
                    ViewBag.Message = Message;
                    return View(list);
                }
                
            }
            return RedirectToAction("Logon", "Account");//trả ra bạn không được xem trang này
        }
         [ValidateInput(false)]
        public ActionResult TryTestResult(int? ID, string Message)
        {
            string StudentName = User.Identity.Name;
            if (User.Identity.IsAuthenticated)
            {
                if (StudentName != null && checkRole.IsStudent(StudentName))
                {
                    Student_Submit st = db.Student_Submit.Where(p => p.ID == ID).FirstOrDefault();
                    ViewBag.Message = Message;
                    //List<TestCaseResult> list = new List<TestCaseResult>();
                    //var tcResult = db.TestCaseResults.Where(p => p.StudentSubmitID == st.ID);
                    //foreach (var t in tcResult)
                    //{
                    //    list.Add(t);
                    //}
                    return View(st);
                }

            }
            return RedirectToAction("Logon", "Account");//trả ra bạn không được xem trang này
        }
        public ActionResult ContestResult()
        {
            return View();
        }
    }
}
